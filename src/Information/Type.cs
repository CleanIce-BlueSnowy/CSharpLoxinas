using System.Diagnostics;

namespace Information;

/// <summary>
/// Loxinas 类型。
/// </summary>
public enum Type {
    Int32,
    Float64,
}

/// <summary>
/// 为 Loxinas 值添加类型相关方法。
/// </summary>
public static class ValueExtendType {
    /// <summary>
    /// 获取值的类型。
    /// </summary>
    /// <param name="value">值。</param>
    /// <returns>类型。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static Type GetType(this IValue value) => value switch {
        ValueInt32 => Type.Int32,
        ValueFloat64 => Type.Float64,
        _ => throw new UnreachableException(),
    };
}
