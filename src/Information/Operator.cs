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
    public static string DebugInfo(this Operator ope) {
        return ope switch {
            Operator.Add => "Add (+)",
            Operator.Sub => "Sub (-)",
            Operator.Star => "Star (*)",
            Operator.Slash => "Slash (/)",
            Operator.Equal => "Equal (=)",
            Operator.EqualEqual => "EqualEqual (==)",
            _ => "## Unknown Operator ##",
        };
    }
}

#endif
