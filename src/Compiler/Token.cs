using Information;

namespace Compiler;

public abstract class Token(Location location) {
    public Location Location {
        get => location;
    }
}

public class TokenEOF(Location location) : Token(location);

public class TokenOperator(Location location, Operator ope) : Token(location) {
    public Operator Operator {
        get => ope;
    }
}

public class TokenIdentifier(Location location, string name) : Token(location) {
    public string Name {
        get => name;
    }
}
