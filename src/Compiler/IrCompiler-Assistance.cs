using Information;
using IR;

namespace Compiler;

/// <summary>
/// Loxinas 编译器。
/// </summary>
public partial class IrCompiler {
    /// <summary>
    /// 表达式编译结果。
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Inst"></param>
    private record struct ExprResult(LoxinasType Type, List<IInstruction> Inst);
}
