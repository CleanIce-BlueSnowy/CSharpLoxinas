namespace Information;

/// <summary>
/// Loxinas 值（编译期使用）。
/// </summary>
public abstract record Value;

/// <summary>
/// Loxinas 32 位有符号整数值（编译期使用）。
/// </summary>
/// <param name="Value">数值。</param>
public record ValueInt32(int Value) : Value;

#if DEBUG

/// <summary>
/// 为值添加调试信息。
/// </summary>
public static class ValueExtendDebug {
    /// <summary>
    /// 获取值的调试信息。
    /// </summary>
    /// <param name="value">值。</param>
    /// <returns>调试信息。</returns>
    public static string DebugInfo(this Value value) => value switch {
        ValueInt32 { Value: int valueInt32 } => PackInfo("Int32", valueInt32.ToString()),
        _ => "## Unknown Value ##",
    };

    /// <summary>
    /// 包装值信息（格式化）。
    /// </summary>
    /// <param name="name">值名称。</param>
    /// <param name="value">原始值的字符串表示。</param>
    /// <returns>包装后的字符串。</returns>
    private static string PackInfo(string name, string value) => $"Value [{name}]: {value}";
}

#endif
