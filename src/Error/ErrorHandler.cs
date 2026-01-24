using CLI;
using Information;

namespace Error;

/// <summary>
/// 错误处理器。
/// </summary>
public static class ErrorHandler {
    /// <summary>
    /// 打印错误。
    /// </summary>
    /// <param name="error">Loxinas 错误。</param>
    public static void PrintError(LoxinasError error) {
        if (Program.LogFile is null) {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        switch (error) {
            case ProgramError programError:
                PrintProgramError(programError);
                break;
            case CompileError compileError:
                PrintCompileError(compileError);
                break;
            case DisassembleError disassembleError:
                PrintDisassembleError(disassembleError);
                break;
        }
        if (Program.LogFile is null) {
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 打印 Loxinas 编译器程序错误。
    /// </summary>
    /// <param name="error">程序错误。</param>
    private static void PrintProgramError(ProgramError error) => PrintNamedError("Program Error", error);

    /// <summary>
    /// 打印 Loxinas 编译错误。
    /// </summary>
    /// <param name="error">编译错误。</param>
    private static void PrintCompileError(CompileError error) {
        if (Program.LogFile is null) {
            Console.WriteLine(error.Location);
        } else {
            Program.LogFile.WriteLine(error.Location);
        }
        PrintLocationSource(Program.sourceLines, error.Location);
        PrintNamedError("Compile Error", error);
    }

    /// <summary>
    /// 打印 Loxinas 反汇编错误。
    /// </summary>
    /// <param name="error">反汇编错误。</param>
    private static void PrintDisassembleError(DisassembleError error) => PrintNamedError("Disassemble Error", error);

    /// <summary>
    /// 打印带名称的 Loxinas 错误。
    /// </summary>
    /// <param name="name">名称。</param>
    /// <param name="error">Loxinas 错误。</param>
    private static void PrintNamedError(string name, LoxinasError error) => Logging.LogError($"{name}: {error.Message}");

    /// <summary>
    /// 打印位置信息的源代码提示。
    /// </summary>
    /// <param name="sourceLines">源代码行。</param>
    /// <param name="location">位置信息。</param>
    private static void PrintLocationSource(string[] sourceLines, Location location) {
        string firstLine = sourceLines[location.Start.Line - 1];
        string output = $"|> {firstLine}";
        if (Program.LogFile is null) {
            Console.WriteLine(output);
        } else {
            Program.LogFile.WriteLine(output);
        }
        if (location.End.Line == location.Start.Line) {
            string spaces = new(' ', location.Start.Idx);
            string arrows = new('^', location.End.Idx - location.Start.Idx);
            output = $"|> {spaces}{arrows}";
            if (Program.LogFile is null) {
                Console.WriteLine(output);
            } else {
                Program.LogFile.WriteLine(output);
            }
        } else {
            string firstLineSpaces = new(' ', location.Start.Idx);
            string firstLineArrows = new('^', firstLine.Length - location.Start.Idx);
            output = $"|> {firstLineSpaces}{firstLineArrows}";
            if (Program.LogFile is null) {
                Console.WriteLine(output);
            } else {
                Program.LogFile.WriteLine(output);
            }
            if (location.End.Line - location.Start.Line > 1) {  // 注意超尾位置（左闭右开）。
                output = $"|> ...";
                if (Program.LogFile is null) {
                    Console.WriteLine(output);
                } else {
                    Program.LogFile.WriteLine(output);
                }
            }
            string lastLine = sourceLines[location.End.Line - 1];
            output = $"|> {lastLine}";
            if (Program.LogFile is null) {
                Console.WriteLine(output);
            } else {
                Program.LogFile.WriteLine(output);
            }
            string lastLineArrows = new('^', location.End.Idx - 1);
            output = $"|> {lastLineArrows}";
            if (Program.LogFile is null) {
                Console.WriteLine(output);
            } else {
                Program.LogFile.WriteLine(output);
            }
        }
    }
}
