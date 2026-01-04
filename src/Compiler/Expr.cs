using Information;

namespace Compiler;

/// <summary>
/// AST 表达式。
/// </summary>
/// <param name="Location"位置信息。</param>
public abstract record Expr {
    public virtual Location Location { get; }
}

/// <summary>
/// AST 表达式：一元操作符。
/// </summary>
/// <param name="Operator">操作符。</param>
/// <param name="Rhs">右操作数。</param>
public record ExprUnary(TokenOperator Operator, Expr Rhs): Expr {
    public override Location Location => Location.Combine(Operator.Location, Rhs.Location);
}

/// <summary>
/// AST 表达式：二元操作符。
/// </summary>
/// <param name="Operator">操作符。</param>
/// <param name="Lhs">左操作数。</param>
/// <param name="Rhs">右操作数。</param>
public record ExprBinary(TokenOperator Operator, Expr Lhs, Expr Rhs): Expr {
    public override Location Location => Location.Combine(Lhs.Location, Rhs.Location);
}

/// <summary>
/// AST 表达式：变量表达式。
/// </summary>
/// <param name="Name">标识符。</param>
public record ExprVariable(TokenIdentifier Name): Expr {
    public override Location Location => Name.Location;
}
