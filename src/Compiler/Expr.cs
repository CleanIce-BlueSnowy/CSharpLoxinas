using Information;

namespace Compiler;

/// <summary>
/// AST 表达式。
/// </summary>
/// <param name="Location"位置信息。</param>
public abstract record Expr(Location Location);

/// <summary>
/// AST 表达式：一元操作符。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Ope">操作符。</param>
/// <param name="Rhs">右操作数。</param>
public record ExprUnary(Location Location, Operator Ope, Expr Rhs): Expr(Location);
