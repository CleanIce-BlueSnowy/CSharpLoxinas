namespace Bytes;

/// <summary>
/// 添加字节转换支持。
/// </summary>
public static partial class ByteConvert {
    /// <summary>
    /// 由小端法字节序列转换为 16 位数值。
    /// </summary>
    /// <param name="bytes">字节序列。</param>
    /// <returns>16 位数值。</returns>
    public static ushort ToUShort(this byte[] bytes) => (ushort)(bytes[0] + (bytes[1] << 8));

    /// <summary>
    /// 由小端法字节序列转换为 32 位数值。
    /// </summary>
    /// <param name="bytes">字节序列。</param>
    /// <returns>32 位数值。</returns>
    public static uint ToUInt(this byte[] bytes) => (uint)(bytes[0] + (bytes[1] << 8) + (bytes[2] << 16) + (bytes[3] << 24));

    /// <summary>
    /// 由小端法字节序列转换为 64 位数值。
    /// </summary>
    /// <param name="bytes">字节序列。</param>
    /// <returns>64 位数值。</returns>
    public static ulong ToULong(this byte[] bytes) => (ulong)(bytes[0] + (bytes[1] << 8) + (bytes[2] << 16) + (bytes[3] << 24) + (bytes[4] << 32) + (bytes[5] << 40) + (bytes[6] << 48) + (bytes[7] << 56));
}
