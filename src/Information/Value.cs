using System.Diagnostics;

namespace Information;

/// <summary>
/// Loxinas 值（编译期使用）。
/// </summary>
public interface IValue;

/// <summary>
/// Loxinas 32 位有符号整数值（编译期使用）。
/// </summary>
/// <param name="Value">数值。</param>
public record struct ValueInt32(int Value) : IValue;

/// <summary>
/// Loxinas 64 位双精度浮点值（编译期使用）。
/// </summary>
/// <param name="Value">数值。</param>
public record struct ValueFloat64(double Value): IValue;

/// <summary>
/// 为值添加扩展方法。
/// </summary>
public static class ValueExtensions {
    /// <summary>
    /// 获取 32 位有符号整数值。
    /// </summary>
    /// <param name="value">Loxinas 值。</param>
    /// <returns>32 位有符号整数。</returns>
    public static int GetInt32Value(this IValue value) => ((ValueInt32)value).Value;

    /// <summary>
    /// 获取 64 位双精度浮点数值。
    /// </summary>
    /// <param name="value">Loxinas 值。</param>
    /// <returns>64 位双精度浮点数值。</returns>
    public static double GetFloat64Value(this IValue value) => ((ValueFloat64)value).Value;
}

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
    /// <exception cref="UnreachableException"></exception>
    public static string DebugInfo(this IValue value) => value switch {
        ValueInt32(int val) => PackInfo("Int32", val.ToString()),
        ValueFloat64(double val) => PackInfo("Float64", val.ToString()),
        _ => throw new UnreachableException(),
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
