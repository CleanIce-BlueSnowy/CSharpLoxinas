using Bytes;

namespace IR;

/// <summary>
/// 中间代码——底层二进制指令。
/// </summary>
public enum IrCode : ushort {
    Constant32,
    Constant64,
    IAdd32,

    /// <summary>
    /// 最大有效值（不包括）。
    /// </summary>
    MAX_VALID,
}

/// <summary>
/// 为 <c>OpCode</c> 添加额外方法。
/// </summary>
public static class IrCodeExtend {
    /// <summary>
    /// 将中间代码指令转换为字节序列。
    /// </summary>
    /// <param name="irCode">中间代码。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this IrCode irCode) => ((ushort)irCode).ToBytes();
}
