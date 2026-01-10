using Bytes;

namespace IR;

/// <summary>
/// 中间代码——底层二进制指令。
/// </summary>
public enum IrCode : ushort {
    Constant32,
    Constant64,
    MAX_VALID = Constant64,
}

/// <summary>
/// 为 <c>OpCode</c> 添加额外方法。
/// </summary>
public static class IrCodeExtend {
    public static byte[] ToBytes(this IrCode irCode) => ((ushort)irCode).ToBytes();
}
