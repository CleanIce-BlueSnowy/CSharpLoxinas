using CLI;
using Compiler;
using Debug;
using Error;

/// <summary>
/// 主程序。
/// </summary>
static class Program {
    /// <summary>
    /// 源代码按行分割的数组。
    /// </summary>
    public static string[] sourceLines = [];

    /// <summary>
    /// 命令行参数。
    /// </summary>
    public static CommandArgs? CommandArgs = null;

    /// <summary>
    /// 入口点。
    /// </summary>
    /// <param name="args">命令行参数。</param>
    private static void Main(string[] args) {
        try {
            CommandArgs = new(args);
        } catch (ErrorList errors) {  // 处理错误列表。
            foreach (var error in errors.Errors) {
                ErrorHandler.PrintError(error);
            }
            Environment.Exit(1);
        }

        if (CommandArgs.InputFile is not null) {
            string source;
            try {
                source = ReadFile(CommandArgs.InputFile);
            } catch (ProgramError error) {
                ErrorHandler.PrintError(error);
                Environment.Exit(1);
                return;  // 阻止“未初始化”警告。
            }

            sourceLines = source.Split('\n');

            Logging.LogInfo("Start compiling.");

            var parser = new Parser(new(source));

            Expr expr;

            try {
                expr = parser.ParseExpression();
            } catch (CompileError error) {
                ErrorHandler.PrintError(error);
                return;
            }

            #if DEBUG
            if (CommandArgs.DebugPrintAst) {
                Logging.LogDebug("====== The AST ======");
                Console.WriteLine(AstPrinter.Print(expr));
                Logging.LogDebug("====== End ======");
            }
            #endif

            Logging.LogSuccess("Parsing finished.");

            var compiler = new IrCompiler();
            compiler.CompileExpression(expr);

            #if DEBUG
            if (CommandArgs.DebugPrintInst) {
                Logging.LogDebug("====== Instructions ======");
                compiler.PrintInstructions();
                Logging.LogDebug("====== End ======");
            }
            if (CommandArgs.DebugPrintIrCode) {
                Logging.LogDebug("====== IR Code ======");
                foreach (byte code in compiler.IrCodeBytes) {
                    Console.WriteLine($"{code:X2}");
                }
                Logging.LogDebug("====== End ======");
            }
            #endif

            Logging.LogSuccess("Compiling finished.");
        } else {  // 无给出文件。
            Console.WriteLine(LoxinasInfo.Version);
        }
    }

    /// <summary>
    /// 阅读源代码文件的所有内容。
    /// </summary>
    /// <param name="path">源代码文件路径。</param>
    /// <returns>源代码。</returns>
    /// <exception cref="ProgramError"></exception>
    private static string ReadFile(string path) {
        try {
            return File.ReadAllText(path);
        } catch (Exception exc) {
            throw new ProgramError($"Cannot read file: {exc.Message}");
        }
    }
}
