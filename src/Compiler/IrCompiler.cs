using Information;
using IR;

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

    public void Compile(IExpr expr) {
        instructions.AddRange(CompileExpression(ref expr).Inst);
    }

    /// <summary>
    /// 编译表达式。
    /// </summary>
    /// <param name="expr"></param>
    /// <returns>表达式类型。</returns>
    /// <exception cref="NotImplementedException"></exception>
    private ExprResult CompileExpression(ref IExpr expr) => expr switch {
        ExprLiteral => CompileExprLiteral(ref expr),
        ExprBinary => CompileExprBinary(ref expr),
        _ => throw new NotImplementedException(),
    };

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
