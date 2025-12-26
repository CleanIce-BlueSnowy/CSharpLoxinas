using CLI;
using Compiler;
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

        if (cmdArgs.InputFile is string inputFilePath) {
            string source;
            try {
                source = ReadFile(inputFilePath);
            } catch (ProgramError error) {
                ErrorHandler.PrintError(error);
                Environment.Exit(1);
                return;
            }

            var lexer = new Lexer(source);
            try {
                while (true) {
                    Token token = lexer.Advance();
                    Console.WriteLine(token);
                    if (token is TokenEOF) {
                        break;
                    }
                }
            } catch (CompileError error) {
                ErrorHandler.PrintError(error);
            }
        } else {
            Console.WriteLine(LoxinasInfo.Version);
        }
    }

    private static string ReadFile(string path) {
        try {
            return File.ReadAllText(path);
        } catch (Exception exc) {
            throw new ProgramError($"Cannot read file: {exc.Message}");
        }
    }
}
