namespace Bytes;

/// <summary>
/// 添加转化为字节数组的支持。
/// </summary>
public static class ByteConvert {
    /// <summary>
    /// 转换为小端法字节序列。
    /// </summary>
    /// <param name="num">数值。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this ushort num) {
        byte first = (byte)(num & 0xff);
        byte second = (byte)((num >> 8) & 0xff);
        return [first, second];
    }

    /// <summary>
    /// 转换为小端法字节序列。
    /// </summary>
    /// <param name="num">数值。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this uint num) {
        byte first = (byte)(num & 0xff);
        byte second = (byte)((num >> 8) & 0xff);
        byte third = (byte)((num >> 16) & 0xff);
        byte fourth = (byte)((num >> 24) & 0xff);
        return [first, second, third, fourth];
    }

    /// <summary>
    /// 转换为小端法字节序列。
    /// </summary>
    /// <param name="num">数值。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this int num) => ((uint)num).ToBytes();

    /// <summary>
    /// 转换为小端法字节序列。
    /// </summary>
    /// <param name="num">数值。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this ulong num) {
        byte first = (byte)(num & 0xff);
        byte second = (byte)((num >> 8) & 0xff);
        byte third = (byte)((num >> 16) & 0xff);
        byte fourth = (byte)((num >> 24) & 0xff);
        byte fifth = (byte)((num >> 32) & 0xff);
        byte sixth = (byte)((num >> 40) & 0xff);
        byte seventh = (byte)((num >> 48) & 0xff);
        byte eighth = (byte)((num >> 56) & 0xff);
        return [first, second, third, fourth, fifth, sixth, seventh, eighth];
    }

    /// <summary>
    /// 转换为小端法字节序列。
    /// </summary>
    /// <param name="num">数值。</param>
    /// <returns>字节序列。</returns>
    public static byte[] ToBytes(this double num) => BitConverter.DoubleToUInt64Bits(num).ToBytes();
}
