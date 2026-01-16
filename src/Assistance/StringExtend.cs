namespace Assistance;

/// <summary>
/// 为 <c>string</c> 类型添加扩展方法。
/// </summary>
public static class StringExtend {
    /// <summary>
    /// 字符串居中对齐。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="width">对齐宽度。</param>
    /// <returns>对齐后的字符串。</returns>
    public static string CenterAlign(this string str, int width) => str.PadLeft(((width - str.Length) / 2) + str.Length).PadRight(width);
}
