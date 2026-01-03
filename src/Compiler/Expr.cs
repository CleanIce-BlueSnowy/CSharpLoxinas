using Information;

namespace Compiler;

/// <summary>
/// AST 表达式。
/// </summary>
/// <param name="location"位置信息。</param>
public abstract class Expr(Location location) {
    /// <summary>
    /// 位置信息
    /// </summary>
    public Location Location {
        get => location;
    }
}

/// <summary>
/// AST 表达式：一元操作符。
/// </summary>
/// <param name="location">位置信息。</param>
/// <param name="ope">操作符。</param>
/// <param name="rhs">右操作数。</param>
public class ExprUnary(Location location, Operator ope, Expr rhs): Expr(location) {
    /// <summary>
    /// 操作符。
    /// </summary>
    public Operator Operator {
        get => ope;
    }

    /// <summary>
    /// 右操作数。
    /// </summary>
    public Expr Rhs {
        get => rhs;
    }
}
