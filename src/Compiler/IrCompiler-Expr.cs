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
    /// <returns>指令序列。</returns>
    private List<IInstruction> CompileExprLiteral(ExprLiteral expr) => [new InstConstant(expr.Value)];

    /// <summary>
    /// 编译二元表达式。
    /// </summary>
    /// <param name="expr">二元表达式。</param>
    /// <returns>指令序列。</returns>
    private List<IInstruction> CompileExprBinary(ExprBinary expr) {
        CompileExpression(expr.Lhs);
        CompileExpression(expr.Rhs);
        return [new InstOperation(LoxinasType.Int32, expr.Operator.Operator)];
    }
}
