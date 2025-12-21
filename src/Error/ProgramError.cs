namespace Error;

public class ProgramError(string message) : Exception, ILoxinasError {
    public override string Message {
        get => message;
    }

    public string Type {
        get => "Program Error";
    }
}
