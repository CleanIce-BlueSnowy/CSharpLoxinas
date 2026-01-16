using Error;

namespace CLI;

/// <summary>
/// 解析之后格式化的命令行参数。
/// </summary>
public class CommandArgs {
    /// <summary>
    /// 输入文件。
    /// </summary>
    public readonly string? InputFile;

    /// <summary>
    /// 输出文件。
    /// </summary>
    public string? OutputFile;

    /// <summary>
    /// 是否设置了编译器模式。未设置则需要自己设置。
    /// </summary>
    public readonly bool SetMode;

    /// <summary>
    /// 反汇编。
    /// </summary>
    public bool Disassemble;

    /// <summary>
    /// 编译。
    /// </summary>
    public bool Compile;

    #if DEBUG // ====== DEBUG BEGIN ======
    /// <summary>
    /// 是否在调试模式下启用词素打印。若不在调试模式下，则无法设置此参数。
    /// </summary>
    public readonly bool DebugPrintToken;

    /// <summary>
    /// 是否在调试模式下启用语法树打印。
    /// </summary>
    public readonly bool DebugPrintAst;

    /// <summary>
    /// 是否在调试模式下启用抽象指令打印。
    /// </summary>
    public readonly bool DebugPrintInst;

    #endif  // ====== DEBUG END ======

    /// <summary>
    /// 解析命令行参数列表并构建命令行参数类。
    /// </summary>
    /// <param name="args">命令行参数列表。</param>
    /// <exception cref="ErrorList"></exception>
    public CommandArgs(string[] args) {
        List<ProgramError> exceptions = [];
        for (int i = 0; i < args.Length; i++) {
            string arg = args[i];
            switch (arg) {
                case "--debug-print-token" or "--debug-pt":
                    #if DEBUG
                    DebugPrintToken = true;
                    #else
                    exceptions.Add(new($"Argument `{arg}` must be used in loxinas debug mode."));
                    #endif
                    break;
                case "--debug-print-ast" or "--debug-pa":
                    #if DEBUG
                    DebugPrintAst = true;
                    #else
                    exceptions.Add(new($"Argument `{arg}` must be used in loxinas debug mode."));
                    #endif
                    break;
                case "--debug-print-instruction" or "--debug-print-inst" or "--debug-pi":
                    #if DEBUG
                    DebugPrintInst = true;
                    #else
                    exceptions.Add(new($"Argument `{arg}` must be used in loxinas debug mode."));
                    #endif
                    break;
                case "--disassemble" or "--disasm":
                    if (SetMode) {
                        exceptions.Add(new($"Cannot set disassemble mode with other modes in the same time."));
                    } else {
                        Disassemble = true;
                        SetMode = true;
                    }
                    break;
                case "-c" or "--compile":
                    if (SetMode) {
                        exceptions.Add(new($"Cannot set compile mode with other modes in the same time."));
                    } else {
                        Compile = true;
                        SetMode = true;
                    }
                    break;
                case "-o" or "--output":
                    if (OutputFile is null) {
                        OutputFile = args[++i];
                    } else {
                        exceptions.Add(new($"Multiplied output file `{args[++i]}`."));
                    }
                    break;
                default:
                    if (!arg.StartsWith('-') && InputFile is null) {
                        InputFile = arg;
                    } else {
                        exceptions.Add(new($"Unknown argument `{arg}`."));
                    }
                    break;
            }
        }
        if (exceptions.Count != 0) {
            throw new ErrorList(exceptions);
        }
    }
}
