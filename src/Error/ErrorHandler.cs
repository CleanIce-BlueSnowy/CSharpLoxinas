using Information;

namespace Error;

public static class ErrorHandler {
    public static void PrintError(LoxinasError error) {
        switch (error) {
            case ProgramError programError:
                PrintProgramError(programError);
                break;
            case CompileError compileError:
                PrintCompileError(compileError);
                break;
        }
    }

    private static void PrintProgramError(ProgramError error) {
        PrintNamedError("Program Error", error);
    }

    private static void PrintCompileError(CompileError error) {
        Console.WriteLine(error.Location);
        PrintLocationSource(Program.sourceLines, error.Location);
        PrintNamedError("Compile Error", error);
    }

    private static void PrintNamedError(string name, LoxinasError error) {
        Console.WriteLine($"{name}: {error.Message}");
    }

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
            if (location.End.Line - location.Start.Line > 1) {
                Console.WriteLine($"|> ...");
            }
            string lastLine = sourceLines[location.End.Line - 1];
            Console.WriteLine($"|> {lastLine}");
            string lastLineArrows = new('^', location.End.Idx - 1);
            Console.WriteLine($"|> {lastLineArrows}");
        }
    }
}
