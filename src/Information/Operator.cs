namespace Information;

public enum Operator {
    Add,
    Sub,
    Star,
    Slash,
    Equal,
    EqualEqual,
}

#if DEBUG

public static class OperatorExtensionsDebug {
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
