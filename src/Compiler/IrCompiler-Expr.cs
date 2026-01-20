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
    private static ExprResult CompileExprLiteral(ExprLiteral expr) => new(expr.Value.GetLoxinasType(), [new InstConstant(expr.Value)]);

    /// <summary>
    /// 编译二元表达式。
    /// </summary>
    /// <param name="expr">二元表达式。</param>
    /// <returns>指令序列。</returns>
    private ExprResult CompileExprBinary(ExprBinary expr) {
        LoxinasType lhsType = CompileExpression(expr.Lhs);
        LoxinasType rhsType = CompileExpression(expr.Rhs);
        if (lhsType != rhsType) {
            throw new CompileError(expr.Operator.Location, $"Cannot use `{expr.Operator.Operator.RawRepr()}` to operate on different types of values: Type `{lhsType.RawRepr()}` and `{rhsType.RawRepr()}`.");
        }
        return new(lhsType, [new InstOperation(LoxinasType.Int32, expr.Operator.Operator)]);
    }
}
