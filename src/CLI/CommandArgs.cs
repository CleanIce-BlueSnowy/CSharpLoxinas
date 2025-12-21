using Error;

namespace CLI;

public class CommandArgs {
    public readonly string? InputFile;

    public CommandArgs(string[] args) {
        List<ProgramError> exceptions = [];
        foreach (string arg in args) {
            if (InputFile == null) {
                InputFile = arg;
            } else {
                exceptions.Add(new($"Unknown argument `{arg}`."));
            }
        }
        if (exceptions.Count != 0) {
            throw new ErrorList(exceptions);
        }
    }
}
