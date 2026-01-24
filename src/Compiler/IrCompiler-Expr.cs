using System.Diagnostics;
using Error;
using Information;
using IR;

namespace Compiler;

/// <summary>
/// Loxinas 编译器。
/// </summary>
public partial class IrCompiler {
    /// <summary>
    /// 编译字面量表达式。
    /// </summary>
    /// <param name="expr">字面量表达式。</param>
    /// <returns>表达式编译结果。</returns>
    private static ExprResult CompileExprLiteral(ref IExpr expression) {
        ExprLiteral expr = (ExprLiteral)expression;

        return new(expr.Value.GetLoxinasType(), [new InstConstant(expr.Value)]);
    }

    /// <summary>
    /// 编译二元表达式。
    /// </summary>
    /// <param name="expr">二元表达式。</param>
    /// <returns>指令序列。</returns>
    /// <exception cref="UnreachableException"></exception>
    private ExprResult CompileExprBinary(ref IExpr expression) {
        ExprBinary expr = (ExprBinary)expression;

        ExprResult lhsRes = CompileExpression(ref expr.Lhs);
        ExprResult rhsRes = CompileExpression(ref expr.Rhs);

        if (lhsRes.Type != rhsRes.Type) {
            throw new CompileError(expr.Operator.Location, $"Cannot use `{expr.Operator.Operator.RawRepr()}` to operate on different types of values: Type `{lhsRes.Type.RawRepr()}` and `{rhsRes.Type.RawRepr()}`.");
        }

        if (Program.CommandArgs!.Optimize && expr.Lhs is ExprLiteral lhs && expr.Rhs is ExprLiteral rhs) {
            IValue newValue = expr.Operator.Operator switch {
                Operator.Add => lhsRes.Type switch {
                    LoxinasType.Int32 => new ValueInt32(lhs.Value.GetInt32Value() + rhs.Value.GetInt32Value()),
                    _ => throw new UnreachableException(),
                },
                Operator.Sub => lhsRes.Type switch {
                    LoxinasType.Int32 => new ValueInt32(lhs.Value.GetInt32Value() - rhs.Value.GetInt32Value()),
                    _ => throw new UnreachableException(),
                },
                _ => throw new UnreachableException(),
            };

            expression = new ExprLiteral(new(expr.Location), newValue);

            return new(lhsRes.Type, [new InstConstant(newValue)]);
        } else {
            instructions.AddRange(lhsRes.Inst);
            instructions.AddRange(rhsRes.Inst);
            return new(lhsRes.Type, [new InstOperation(LoxinasType.Int32, expr.Operator.Operator)]);
        }
    }
}
