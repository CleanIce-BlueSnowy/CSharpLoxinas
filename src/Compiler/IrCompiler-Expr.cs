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
    private static List<IInstruction> CompileExprLiteral(ExprLiteral expr) => [new InstConstant(expr.Value)];
}
