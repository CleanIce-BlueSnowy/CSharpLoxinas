namespace Information;

public enum Keyword {
    Else,
    Func,
    If,
}

#if DEBUG

public static class KeywordExtensionsDebug {
    public static string DebugInfo(this Keyword keyword) {
        return keyword switch {
            Keyword.Else => "`Else`",
            Keyword.Func => "`Func`",
            Keyword.If => "`If`",
            _ => "## Unknown Keyword ##",
        };
    }
}

#endif
