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
    /// 日志文件。
    /// </summary>
    public string? LogFile;

    /// <summary>
    /// 是否设置了编译器模式。未设置则需要自己设置。
    /// </summary>
    public readonly bool SetMode;

    /// <summary>
    /// 运行。
    /// </summary>
    public bool Run;

    /// <summary>
    /// 反汇编。
    /// </summary>
    public bool Disassemble;

    /// <summary>
    /// 编译。
    /// </summary>
    public bool Compile;

    /// <summary>
    /// 优化。
    /// </summary>
    public bool Optimize;

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

    /// <summary>
    /// 是否在调试模式下启用运行信息打印。
    /// </summary>
    public readonly bool DebugLogRunning;

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
                    exceptions.Add(new($"Unknown argument `{arg}`."));
                    #endif
                    break;
                case "--debug-print-ast" or "--debug-pa":
                    #if DEBUG
                    DebugPrintAst = true;
                    #else
                    exceptions.Add(new($"Unknown argument `{arg}`."));
                    #endif
                    break;
                case "--debug-print-instruction" or "--debug-print-inst" or "--debug-pi":
                    #if DEBUG
                    DebugPrintInst = true;
                    #else
                    exceptions.Add(new($"Unknown argument `{arg}`."));
                    #endif
                    break;
                case "--debug-log-running" or "--debug-lr":
                    #if DEBUG
                    DebugLogRunning = true;
                    #else
                    exceptions.Add(new($"Unknown argument `{arg}`."));
                    #endif
                    break;
                case "--run" or "-r":
                    if (SetMode) {
                        exceptions.Add(new("Cannot set disassemble mode with other modes in the same time."));
                    } else {
                        Run = true;
                        SetMode = true;
                    }
                    break;
                case "--disassemble" or "--disasm":
                    if (SetMode) {
                        exceptions.Add(new("Cannot set disassemble mode with other modes in the same time."));
                    } else {
                        Disassemble = true;
                        SetMode = true;
                    }
                    break;
                case "-c" or "--compile":
                    if (SetMode) {
                        exceptions.Add(new("Cannot set compile mode with other modes in the same time."));
                    } else {
                        Compile = true;
                        SetMode = true;
                    }
                    break;
                case "-O" or "--optimize":
                    Optimize = true;
                    break;
                case "-o" or "--output":
                    if (i + 1 >= args.Length) {
                        exceptions.Add(new("Output file excepted."));
                    }
                    if (OutputFile is null) {
                        OutputFile = args[++i];
                    } else {
                        exceptions.Add(new($"Multiplied output file `{args[++i]}`."));
                    }
                    break;
                case "--log":
                    if (i + 1 >= args.Length) {
                        exceptions.Add(new("Log file excepted."));
                    }
                    if (LogFile is null) {
                        LogFile = args[++i];
                    } else {
                        exceptions.Add(new($"Multiplied log file `{args[++i]}`."));
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
