using CLI;
using Compiler;
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
    /// 入口点。
    /// </summary>
    /// <param name="args">命令行参数。</param>
    private static void Main(string[] args) {
        CommandArgs cmdArgs;
        try {
            cmdArgs = new(args);
        } catch (ErrorList errors) {  // 处理错误列表。
            foreach (var error in errors.Errors) {
                ErrorHandler.PrintError(error);
            }
            Environment.Exit(1);
            return;  // 阻止“未初始化”的警告。
        }

        if (cmdArgs.InputFile is not null) {
            string source;
            try {
                source = ReadFile(cmdArgs.InputFile);
            } catch (ProgramError error) {
                ErrorHandler.PrintError(error);
                Environment.Exit(1);
                return;
            }

            sourceLines = source.Split('\n');

            var lexer = new Lexer(source);
            while (true) {
                Token token;
                try {
                    token = lexer.Advance();
                } catch (CompileError error) {
                    ErrorHandler.PrintError(error);
                    lexer.Synchronize();  // 同步，避免连环错误。
                    continue;
                }
                #if DEBUG
                Console.WriteLine(token.DebugInfo());
                #endif
                if (token is TokenEOF) {
                    break;
                }
            }
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
