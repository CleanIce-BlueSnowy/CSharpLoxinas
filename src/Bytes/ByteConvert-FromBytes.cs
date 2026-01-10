namespace Bytes;

/// <summary>
/// 添加字节转换支持。
/// </summary>
public static partial class ByteConvert {
    /// <summary>
    /// 由小端法字节序列转换为值。
    /// </summary>
    /// <param name="bytes">字节序列。</param>
    /// <returns>值。</returns>
    public static ushort ToUShort(this byte[] bytes) => (ushort)(bytes[0] + (bytes[1] << 8));

    /// <summary>
    /// 由小端法字节序列转换为值。
    /// </summary>
    /// <param name="bytes">字节序列。</param>
    /// <returns>值。</returns>
    public static uint ToUInt(this byte[] bytes) => (uint)(bytes[0] + (bytes[1] << 8) + (bytes[2] << 16) + (bytes[3] << 24));
}
