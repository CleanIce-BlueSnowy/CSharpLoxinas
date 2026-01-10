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
        Console.ForegroundColor = ConsoleColor.Red;
        switch (error) {
            case ProgramError programError:
                PrintProgramError(programError);
                break;
            case CompileError compileError:
                PrintCompileError(compileError);
                break;
        }
        Console.ResetColor();
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
        Console.WriteLine(error.Location);
        PrintLocationSource(Program.sourceLines, error.Location);
        PrintNamedError("Compile Error", error);
    }

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
        Console.WriteLine($"|> {firstLine}");
        if (location.End.Line == location.Start.Line) {
            string spaces = new(' ', location.Start.Idx);
            string arrows = new('^', location.End.Idx - location.Start.Idx);
            Console.WriteLine($"|> {spaces}{arrows}");
        } else {
            string firstLineSpaces = new(' ', location.Start.Idx);
            string firstLineArrows = new('^', firstLine.Length - location.Start.Idx);
            Console.WriteLine($"|> {firstLineSpaces}{firstLineArrows}");
            if (location.End.Line - location.Start.Line > 1) {  // 注意超尾位置（左闭右开）。
                Console.WriteLine($"|> ...");
            }
            string lastLine = sourceLines[location.End.Line - 1];
            Console.WriteLine($"|> {lastLine}");
            string lastLineArrows = new('^', location.End.Idx - 1);
            Console.WriteLine($"|> {lastLineArrows}");
        }
    }
}
