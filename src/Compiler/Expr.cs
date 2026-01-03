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
/// <param name="Operator">操作符。</param>
/// <param name="Rhs">右操作数。</param>
public record ExprUnary(Location Location, TokenOperator Operator, Expr Rhs): Expr(Location);

/// <summary>
/// AST 表达式：二元操作符。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Operator">操作符。</param>
/// <param name="Lhs">左操作数。</param>
/// <param name="Rhs">右操作数。</param>
public record ExprBinary(Location Location, TokenOperator Operator, Expr Lhs, Expr Rhs): Expr(Location);

/// <summary>
/// AST 表达式：变量表达式。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Name">标识符。</param>
public record ExprVariable(Location Location, TokenIdentifier Name): Expr(Location);
