using System.Diagnostics;

namespace Information;

/// <summary>
/// Loxinas 类型。
/// </summary>
public enum LoxinasType {
    Int32,
    Float64,
}

/// <summary>
/// 为 Loxinas 类型添加扩展方法。
/// </summary>
public static class LoxinasTypeExtensions {
    /// <summary>
    /// 原始代码表示。
    /// </summary>
    /// <param name="type">Loxinas 类型。</param>
    /// <returns>表示字符串。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string RawRepr(this LoxinasType type) => type switch {
        LoxinasType.Int32 => "int",
        LoxinasType.Float64 => "double",
        _ => throw new UnreachableException(),
    };
}

/// <summary>
/// 为 Loxinas 值添加类型相关方法。
/// </summary>
public static class ValueExtendLoxinasType {
    /// <summary>
    /// 获取值的类型。
    /// </summary>
    /// <param name="value">值。</param>
    /// <returns>类型。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static LoxinasType GetLoxinasType(this IValue value) => value switch {
        ValueInt32 => LoxinasType.Int32,
        ValueFloat64 => LoxinasType.Float64,
        _ => throw new UnreachableException(),
    };
}

#if DEBUG

/// <summary>
/// 为 Loxinas 类型添加调试信息。
/// </summary>
public static class LoxinasTypeExtensionsDebug {
    /// <summary>
    /// 调试信息。
    /// </summary>
    /// <param name="type">类型。</param>
    /// <returns>类型在调试模式下的字符串表示。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string DebugInfo(this LoxinasType type) => type switch {
        LoxinasType.Int32 => "Int32 (int)",
        LoxinasType.Float64 => "Float64 (double)",
        _ => throw new UnreachableException(),
    };
}

#endif
