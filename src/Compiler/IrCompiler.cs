namespace Compiler;

/// <summary>
/// Loxinas 编译器。
/// </summary>
public partial class IrCompiler {
    /// <summary>
    /// 抽象指令序列。
    /// </summary>
    private readonly List<IInstruction> instructions = [];

    /// <summary>
    /// 中间代码字节序列。
    /// </summary>
    public byte[] IrCodeBytes {
        get {
            List<byte> bytes = [];
            foreach (IInstruction inst in instructions) {
                bytes.AddRange(inst.ToBytes());
            }
            return [..bytes];
        }
    }

    /// <summary>
    /// 编译表达式。
    /// </summary>
    /// <param name="expr"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void CompileExpression(Expr expr) => instructions.AddRange(expr switch {
        ExprLiteral exprLiteral => CompileExprLiteral(exprLiteral),
        _ => throw new NotImplementedException(),
    });

    #if DEBUG
    /// <summary>
    /// 调试模式下打印抽象指令序列。
    /// </summary>
    public void PrintInstructions() {
        foreach (IInstruction inst in instructions) {
            Console.WriteLine(inst.DebugInfo());
        }
    }
    #endif
}
