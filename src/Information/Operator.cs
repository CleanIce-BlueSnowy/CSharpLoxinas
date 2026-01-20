using System.Diagnostics;

namespace Information;

/// <summary>
/// 操作符。
/// </summary>
public enum Operator {
    Add,
    Sub,
    Star,
    Slash,
    Equal,
    EqualEqual,
    LeftParen,
    RightParen,
}

/// <summary>
/// 为操作符添加扩展方法。
/// </summary>
public static class OperatorExtensions {
    /// <summary>
    /// 原始代码表示。
    /// </summary>
    /// <param name="ope">操作符。</param>
    /// <returns>表示字符串。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string RawRepr(this Operator ope) => ope switch {
        Operator.Add => "+",
        Operator.Sub => "-",
        Operator.Star => "*",
        Operator.Slash => "/",
        Operator.Equal => "=",
        Operator.EqualEqual => "==",
        Operator.LeftParen => "(",
        Operator.RightParen => ")",
        _ => throw new UnreachableException(),
    };
}

#if DEBUG

/// <summary>
/// 为操作符添加调试信息。
/// </summary>
public static class OperatorExtensionsDebug {
    /// <summary>
    /// 调试信息。
    /// </summary>
    /// <param name="ope">操作符。</param>
    /// <returns>操作符在调试模式下的字符串表示。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string DebugInfo(this Operator ope) => ope switch {
        Operator.Add => "Add `+`",
        Operator.Sub => "Sub `-`",
        Operator.Star => "Star `*`",
        Operator.Slash => "Slash `/`",
        Operator.Equal => "Equal `=`",
        Operator.EqualEqual => "EqualEqual `==`",
        Operator.LeftParen => "LeftParen `(`",
        Operator.RightParen => "RightParen `)`",
        _ => throw new UnreachableException(),
    };
}

#endif
