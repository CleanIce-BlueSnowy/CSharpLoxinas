using Information;

namespace Error;

public class CompileError(Location location, string message) : ILoxinasError {
    public string Message {
        get => message;
    }

    public Location Location {
        get => location;
    }

    public string Type {
        get => "Compile Error";
    }
}
