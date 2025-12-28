using Information;

namespace Compiler;

public abstract class Expr(Location location) {
    public Location Location {
        get => location;
    }
}

public class ExprUnary(Location location, Operator ope, Expr rhs): Expr(location) {
    public Operator Operator {
        get => ope;
    }

    public Expr Rhs {
        get => rhs;
    }
}
