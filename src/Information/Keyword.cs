namespace Information;

/// <summary>
/// Loxinas 关键字。
/// </summary>
public enum Keyword {
    Else,
    Func,
    If,
}

#if DEBUG

/// <summary>
/// 为关键字添加调试信息。
/// </summary>
public static class KeywordExtensionsDebug {
    /// <summary>
    /// 调试信息。
    /// </summary>
    /// <param name="keyword">关键字。</param>
    /// <returns>关键字在调试模式下的字符串表示。</returns>
    public static string DebugInfo(this Keyword keyword) => keyword switch {
        Keyword.Else => "`Else`",
        Keyword.Func => "`Func`",
        Keyword.If => "`If`",
        _ => "## Unknown Keyword ##",
    };
}

#endif
