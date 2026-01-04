using Error;
using Information;

namespace Compiler;

/// <summary>
/// 语法解析器。
/// </summary>
public partial class Parser {
    /// <summary>
    /// 等式表达式（解析操作符 <c>==</c> 与 <c>!=</c>）。
    /// </summary>
    /// <returns>表达式。</returns>
    private Expr ExprEquality() {
        Expr lhs = ExprTerm();

        while (lexer.Peek() is TokenOperator { Operator: Operator.EqualEqual } ope) {
            lexer.Advance();
            Expr rhs = ExprTerm();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 加减表达式（解析操作符 <c>+</c> 与 <c>-</c>）。
    /// </summary>
    /// <returns>表达式。</returns>
    private Expr ExprTerm() {
        Expr lhs = ExprFactor();

        while (lexer.Peek() is TokenOperator { Operator: Operator.Add | Operator.Sub } ope) {
            lexer.Advance();
            Expr rhs = ExprFactor();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 乘除表达式（解析操作符 <c>*</c> 与 <c>/</c>）。
    /// </summary>
    /// <returns>表达式。</returns>
    private Expr ExprFactor() {
        Expr lhs = ExprUnary();

        while (lexer.Peek() is TokenOperator { Operator: Operator.Star | Operator.Slash } ope) {
            lexer.Advance();
            Expr rhs = ExprUnary();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 一元表达式（解析操作符 <c>-</c>）。
    /// </summary>
    /// <returns></returns>
    private Expr ExprUnary() {
        if (lexer.Peek() is TokenOperator { Operator: Operator.Sub } ope) {
            lexer.Advance();
            Expr rhs = ExprUnary();
            return new ExprUnary(ope, rhs);
        } else {
            return ExprPrimary();
        }
    }

    /// <summary>
    /// 基本表达式（字面量、标识符）。
    /// </summary>
    /// <returns>表达式。</returns>
    private Expr ExprPrimary() {
        switch (lexer.Peek()) {
            case TokenIdentifier tokenIdentifier:
                lexer.Advance();
                return new ExprVariable(tokenIdentifier);
            default:
                throw new CompileError(lexer.Advance().Location, "Expected an expression.");
        }
    }
}
