namespace Error;

public class ProgramError(string message) : LoxinasError(message) {
    public override string Type {
        get => "Program Error";
    }
}
