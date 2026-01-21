using Information;

namespace Compiler;

/// <summary>
/// AST 表达式。
/// </summary>
/// <param name="Location"位置信息。</param>
public interface IExpr {
    /// <summary>
    /// 位置信息。
    /// </summary>
    public Location Location { get; }
}

/// <summary>
/// AST 表达式：一元操作符。
/// </summary>
/// <param name="ope">操作符。</param>
/// <param name="rhs">右操作数。</param>
public class ExprUnary(TokenOperator ope, IExpr rhs): IExpr {
    /// <summary>
    /// 操作符。
    /// </summary>
    public TokenOperator Operator => ope;

    /// <summary>
    /// 右操作数。
    /// </summary>
    public IExpr Rhs = rhs;

    public Location Location => Location.Combine(Operator.Location, Rhs.Location);
}

/// <summary>
/// AST 表达式：二元操作符。
/// </summary>
/// <param name="ope">操作符。</param>
/// <param name="lhs">左操作数。</param>
/// <param name="rhs">右操作数。</param>
public class ExprBinary(TokenOperator ope, IExpr lhs, IExpr rhs): IExpr {
    /// <summary>
    /// 操作符。
    /// </summary>
    public TokenOperator Operator => ope;

    /// <summary>
    /// 左操作数。
    /// </summary>
    public IExpr Lhs = lhs;

    /// <summary>
    /// 右操作数。
    /// </summary>
    public IExpr Rhs = rhs;

    public Location Location => Location.Combine(Lhs.Location, Rhs.Location);
}

/// <summary>
/// AST 表达式：字面量。
/// </summary>
/// <param name="token">词素。</param>
/// <param name="value">值。</param>
public class ExprLiteral(Token token, IValue value): IExpr {
    /// <summary>
    /// 词素。
    /// </summary>
    public Token Token => token;

    /// <summary>
    /// 值。
    /// </summary>
    public IValue Value => value;

    public Location Location => Token.Location;
}

/// <summary>
/// AST 表达式：变量表达式。
/// </summary>
/// <param name="name">标识符。</param>
public class ExprVariable(TokenIdentifier name): IExpr {
    /// <summary>
    /// 标识符。
    /// </summary>
    public TokenIdentifier Name => name;

    public Location Location => Name.Location;
}
