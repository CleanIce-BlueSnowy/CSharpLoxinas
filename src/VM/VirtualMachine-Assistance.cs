using Bytes;
using Error;
using IR;

namespace VM;

/// <summary>
/// Loxinas 虚拟机。
/// </summary>
public partial class VirtualMachine {
    /// <summary>
    /// 获取 IR 指令。
    /// </summary>
    /// <returns>IR 指令。</returns>
    /// <exception cref="ProgramError"></exception>
    private IrCode GetIrCode() {
        ushort code = ReadInt16();
        if (code >= (ushort)IrCode.MAX_VALID) {
            throw new ProgramError($"[At {InstCntToString()}] Unknown IR COde: {code:X4} ()");
        }
        return (IrCode)code;
    }

    /// <summary>
    /// 读取 16 位整数值。
    /// </summary>
    /// <returns>16 位整数值。</returns>
    private ushort ReadInt16() {
        CodeSizeCheck(2);
        byte[] bytes = irCodes[irCnt..(irCnt + 2)];
        irCnt += 2;
        return bytes.ToUShort();
    }

    /// <summary>
    /// 读取 32 位整数值。
    /// </summary>
    /// <returns>32 位整数值。</returns>
    private uint ReadInt32() {
        CodeSizeCheck(4);
        byte[] bytes = irCodes[irCnt..(irCnt + 4)];
        irCnt += 4;
        return bytes.ToUInt();
    }

    /// <summary>
    /// 读取 64 位整数值。
    /// </summary>
    /// <returns>64 位整数值。</returns>
    private ulong ReadInt64() {
        CodeSizeCheck(8);
        byte[] bytes = irCodes[irCnt..(irCnt + 8)];
        irCnt += 8;
        return bytes.ToULong();
    }

    /// <summary>
    /// 是否已经到了 IR 代码结尾。
    /// </summary>
    /// <returns></returns>
    private bool IsAtEnd() => irCnt >= irCodes.Length;

    /// <summary>
    /// 将指令计数进行格式化。
    /// </summary>
    /// <returns>格式化后的字符串。</returns>
    private string InstCntToString() => $"{irCnt:X8}";

    /// <summary>
    /// 检查代码字节序列是否有足够的字节读取。
    /// </summary>
    /// <param name="need">需要的字节数量。</param>
    /// <exception cref="ProgramError"></exception>
    private void CodeSizeCheck(int need) {
        if (irCnt + need > irCodes.Length) {
            throw new ProgramError($"Not enough bytes to read: Need {need} bytes.");
        }
    }
}
