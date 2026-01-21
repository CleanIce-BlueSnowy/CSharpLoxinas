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
    private IExpr ExprEquality() {
        IExpr lhs = ExprTerm();

        while (lexer.Peek() is TokenOperator(_, Operator: Operator.EqualEqual) ope) {
            #if DEBUG
            if (Program.CommandArgs!.DebugPrintToken) {
                Console.WriteLine(ope.DebugInfo());
            }
            #endif

            lexer.Advance();
            IExpr rhs = ExprTerm();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 加减表达式（解析操作符 <c>+</c> 与 <c>-</c>）。
    /// </summary>
    /// <returns>表达式。</returns>
    private IExpr ExprTerm() {
        IExpr lhs = ExprFactor();

        while (lexer.Peek() is TokenOperator(_, Operator: Operator.Add or Operator.Sub) ope) {
            #if DEBUG
            if (Program.CommandArgs!.DebugPrintToken) {
                Console.WriteLine(ope.DebugInfo());
            }
            #endif

            lexer.Advance();
            IExpr rhs = ExprFactor();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 乘除表达式（解析操作符 <c>*</c> 与 <c>/</c>）。
    /// </summary>
    /// <returns>表达式。</returns>
    private IExpr ExprFactor() {
        IExpr lhs = ExprUnary();

        while (lexer.Peek() is TokenOperator(_, Operator: Operator.Star or Operator.Slash) ope) {
            #if DEBUG
            if (Program.CommandArgs!.DebugPrintToken) {
                Console.WriteLine(ope.DebugInfo());
            }
            #endif

            lexer.Advance();
            IExpr rhs = ExprUnary();
            lhs = new ExprBinary(ope, lhs, rhs);
        }

        return lhs;
    }

    /// <summary>
    /// 一元表达式（解析操作符 <c>-</c>）。
    /// </summary>
    /// <returns></returns>
    private IExpr ExprUnary() {
        if (lexer.Peek() is TokenOperator(_, Operator: Operator.Sub) ope) {
            #if DEBUG
            if (Program.CommandArgs!.DebugPrintToken) {
                Console.WriteLine(ope.DebugInfo());
            }
            #endif

            lexer.Advance();
            IExpr rhs = ExprUnary();
            return new ExprUnary(ope, rhs);
        } else {
            return ExprPrimary();
        }
    }

    /// <summary>
    /// 基本表达式（字面量、标识符）。
    /// </summary>
    /// <returns>表达式。</returns>
    private IExpr ExprPrimary() {
        switch (lexer.Peek()) {
            case TokenIdentifier tokenIdentifier:
                #if DEBUG
                if (Program.CommandArgs!.DebugPrintToken) {
                    Console.WriteLine(lexer.Peek().DebugInfo());
                }
                #endif

                lexer.Advance();
                return new ExprVariable(tokenIdentifier);

            case TokenNumber(_, Value: IValue value) tokenNumber:
                #if DEBUG
                if (Program.CommandArgs!.DebugPrintToken) {
                    Console.WriteLine(lexer.Peek().DebugInfo());
                }
                #endif

                lexer.Advance();
                return new ExprLiteral(tokenNumber, value);

            case TokenOperator(_, Operator: Operator.LeftParen):
                #if DEBUG
                if (Program.CommandArgs!.DebugPrintToken) {
                    Console.WriteLine(lexer.Peek().DebugInfo());
                }
                #endif

                lexer.Advance();
                IExpr expr = ParseExpression();
                if (lexer.Peek() is not TokenOperator { Operator: Operator.RightParen }) {
                    throw new CompileError(lexer.Advance().Location, "Expected `)` after the expression.");
                }
                lexer.Advance();
                return expr;

            default:
                throw new CompileError(lexer.Advance().Location, "Expected an expression.");
        }
    }
}
