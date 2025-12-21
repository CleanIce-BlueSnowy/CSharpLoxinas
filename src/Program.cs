using CLI;
using Error;

static class Program {
    private static void Main(string[] args) {
        CommandArgs cmdArgs;
        try {
            cmdArgs = new(args);
        } catch (ErrorList errors) {
            foreach (var error in errors.Errors) {
                ErrorHandler.PrintError(error);
            }
            Environment.Exit(1);
            return;
        }

        if (cmdArgs.InputFile != null) {
            Console.WriteLine($"Input file is: {cmdArgs.InputFile}");
        } else {
            Console.WriteLine("No input file.");
        }
    }
}
