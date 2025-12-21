namespace Error;

static class ErrorHandler {
    public static void PrintError(ILoxinasError error) {
        Console.WriteLine($"{error.Type}: {error.Message}");
    }
}
