using System.Diagnostics;
using System.Text;
using CLI;
using IR;

namespace VM;

#if DEBUG

/// <summary>
/// Loxinas 虚拟机。
/// </summary>
public partial class VirtualMachine {
    /// <summary>
    /// 打印值栈。
    /// </summary>
    private void PrintValueStack() {
        var builder = new StringBuilder("Stack: [ ");
        foreach (byte ele in valueStack) {
            builder.Append($"{ele:X2} ");
        }
        builder.Append(']');
        Logging.LogDebug(builder.ToString());
    }

    /// <summary>
    /// 打印当前指令。
    /// </summary>
    /// <param name="irCode">指令。</param>
    private void PrintCurrentCode(IrCode irCode) => Logging.LogDebug($"IR Code: {GetIrCodeName(irCode)}");

    /// <summary>
    /// 获取 IR 指令名称。
    /// </summary>
    /// <param name="irCode">IR 指令。</param>
    /// <returns>名称</returns>
    /// <exception cref="UnreachableException"></exception>
    private static string GetIrCodeName(IrCode irCode) => irCode switch {
        IrCode.Constant32 => "Constant32",
        IrCode.Constant64 => "Constant64",
        IrCode.IAdd32 => "IAdd32",
        IrCode.ISub32 => "ISub32",
        _ => throw new UnreachableException(),
    };
}

#endif
