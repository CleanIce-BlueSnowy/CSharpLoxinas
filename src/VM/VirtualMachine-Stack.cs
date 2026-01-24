using Bytes;
using Error;

namespace VM;

/// <summary>
/// Loxinas 虚拟机。
/// </summary>
public partial class VirtualMachine {
    /// <summary>
    /// 往值栈中压入 32 位整数值。
    /// </summary>
    /// <param name="value">值。</param>
    private void PushInt32(uint value) => valueStack.AddRange(value.ToBytes());

    /// <summary>
    /// 往值栈中压入 64 位整数值。
    /// </summary>
    /// <param name="value">值。</param>
    private void PushInt64(ulong value) => valueStack.AddRange(value.ToBytes());

    /// <summary>
    /// 往值栈中压入 64 位双精度浮点数值。
    /// </summary>
    /// <param name="value">值。</param>
    private void PushFloat64(double value) => valueStack.AddRange(value.ToBytes());

    /// <summary>
    /// 获取栈顶的 32 位整数值。
    /// </summary>
    /// <returns>值。</returns>
    private uint PeekInt32() {
        StackSizeCheck(4);
        byte[] bytes = [.. valueStack[^4..]];
        return bytes.ToUInt();
    }

    /// <summary>
    /// 获取栈顶的 64 位双精度浮点数值。
    /// </summary>
    /// <returns>值。</returns>
    private double PeekFloat64() {
        StackSizeCheck(8);
        byte[] bytes = [.. valueStack[^8..]];
        return bytes.ToDouble();
    }

    /// <summary>
    /// 弹出栈顶的 32 位整数值。
    /// </summary>
    /// <returns>值。</returns>
    private uint PopInt32() {
        uint value = PeekInt32();
        valueStack.RemoveRange(valueStack.Count - 4, 4);
        return value;
    }

    /// <summary>
    /// 弹出栈顶的 32 位整数值。
    /// </summary>
    /// <returns>值。</returns>
    private double PopFloat64() {
        double value = PeekFloat64();
        valueStack.RemoveRange(valueStack.Count - 8, 8);
        return value;
    }

    /// <summary>
    /// 检查栈是否足够使用。
    /// </summary>
    /// <param name="need">需要的大小。</param>
    private void StackSizeCheck(int need) {
        if (valueStack.Count < need) {
            throw new ProgramError($"Stack now has {valueStack.Count} bytes but {need} bytes are needed.");
        }
    }
}
