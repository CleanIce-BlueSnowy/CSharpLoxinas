using Information;

namespace Error;

public static class ErrorHandler {
    public static void PrintError(LoxinasError error) {
        if (error is CompileError compileError) {
            PrintErrorRaw(compileError, compileError.Location);
        } else {
            PrintErrorRaw(error);
        }
    }

    private static void PrintErrorRaw(LoxinasError error) {
        Console.WriteLine($"{error.Type}: {error.Message}");
    }

    private static void PrintErrorRaw(LoxinasError error, Location location) {
        Console.WriteLine(location.ToString());
        PrintErrorRaw(error);
    }
}
