using CLI;
using Compiler;
using Debug;
using Error;
using IR;

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
            try {
                CommandArgs = new(args);
            } catch (ErrorList errors) {  // 处理错误列表。
                foreach (var error in errors.Errors) {
                    ErrorHandler.PrintError(error);
                }
                Environment.Exit(1);
            }

            if (CommandArgs.InputFile is not null) {
                string extName = Path.GetExtension(CommandArgs.InputFile);
                string otherName = Path.Combine(Path.GetDirectoryName(CommandArgs.InputFile) ?? "", Path.GetFileNameWithoutExtension(CommandArgs.InputFile));
                if (!CommandArgs.SetMode) {
                    switch (extName.ToLower()) {
                        case LoxinasInfo.SourceCodeExt or "":
                            CommandArgs.Compile = true;
                            break;
                        case LoxinasInfo.IrCodeExt:
                            CommandArgs.Disassemble = true;
                            break;
                        default:
                            throw new ProgramError($"Could not infer the compiler mode from input file: Unknown extension `{extName}`.");
                    }
                }

                if (CommandArgs.Compile) {
                    CommandArgs.OutputFile ??= otherName + LoxinasInfo.IrCodeExt;
                    if (!CommandArgs.OutputFile.EndsWith(LoxinasInfo.IrCodeExt, StringComparison.OrdinalIgnoreCase)) {
                        CommandArgs.OutputFile += LoxinasInfo.IrCodeExt;
                    }
                } else if (CommandArgs.Disassemble) {
                    if (CommandArgs.OutputFile is not null && !CommandArgs.OutputFile.EndsWith(LoxinasInfo.DisasmCodeExt, StringComparison.OrdinalIgnoreCase)) {
                        CommandArgs.OutputFile += LoxinasInfo.DisasmCodeExt;
                    }
                }

                if (CommandArgs.Compile) {
                    Compile();
                } else if (CommandArgs.Disassemble) {
                    Disassemble();
                }
            } else {  // 无给出文件。
                Console.WriteLine(LoxinasInfo.Version);
            }
        } catch (ProgramError error) {
            ErrorHandler.PrintError(error);
            Environment.Exit(2);
        }
    }

    /// <summary>
    /// 编译源代码。
    /// </summary>
    /// <exception cref="ProgramError"></exception>
    private static void Compile() {
        string source;
        try {
            source = ReadFile(CommandArgs!.InputFile!);
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
        #endif

        try {
            File.WriteAllBytes(CommandArgs!.OutputFile!, compiler.IrCodeBytes);
        } catch (Exception exc) {
            throw new ProgramError($"Could not write file: {exc.Message}");
        }

        Logging.LogSuccess("Compiling finished.");
    }

    /// <summary>
    /// 反汇编中间代码。
    /// </summary>
    private static void Disassemble() {
        byte[] bytes;
        try {
            bytes = File.ReadAllBytes(CommandArgs!.InputFile!);
        } catch (Exception exc) {
            throw new ProgramError($"Could not read file: {exc.Message}");
        }

        Logging.LogInfo("Start diassembling.");

        var disasm = new Disassembler(bytes);

        string asm;
        try {
            asm = disasm.Disasm();
        } catch (DisassembleError error) {
            ErrorHandler.PrintError(error);
            Environment.Exit(1);
            return;
        }

        if (CommandArgs!.OutputFile is null) {
            Console.WriteLine(asm);
        } else {
            try {
                File.WriteAllText(CommandArgs!.OutputFile, asm);
            } catch (Exception exc) {
                throw new ProgramError($"Could not write file: {exc.Message}");
            }
        }

        Logging.LogSuccess("Disassembling finished.");
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
            throw new ProgramError($"Could not read file: {exc.Message}");
        }
    }
}
