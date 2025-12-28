using CLI;
using Compiler;
using Error;

static class Program {
    public static string[] sourceLines = [];

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

        if (cmdArgs.InputFile is not null) {
            string source;
            try {
                source = ReadFile(cmdArgs.InputFile);
            } catch (ProgramError error) {
                ErrorHandler.PrintError(error);
                Environment.Exit(1);
                return;
            }

            sourceLines = source.Split('\n');

            var lexer = new Lexer(source);
            while (true) {
                Token token;
                try {
                    token = lexer.Advance();
                } catch (CompileError error) {
                    ErrorHandler.PrintError(error);
                    lexer.Synchronize();
                    continue;
                }
                #if DEBUG
                Console.WriteLine(token.DebugInfo());
                #endif
                if (token is TokenEOF) {
                    break;
                }
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
