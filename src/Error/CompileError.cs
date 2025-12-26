using Information;

namespace Error;

public class CompileError(Location location, string message) : LoxinasError(message) {
    public Location Location {
        get => location;
    }

    public override string Type {
        get => "Compile Error";
    }
}
