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
