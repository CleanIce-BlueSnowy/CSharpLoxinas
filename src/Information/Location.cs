using System.Text;

namespace Information;

/// <summary>
/// 一段位置。
/// </summary>
/// <param name="Start">起始点。</param>
/// <param name="End">终止点，超尾位置。</param>
public record struct Location(Position Start, Position End) {
    public override readonly string ToString() {
        var builder = new StringBuilder($"[line {Start.Line}");
        if (End.Line != Start.Line) {
            builder.Append($"-{End.Line}");
        }
        builder.Append($" at {Start.Idx + 1}");
        if (End.Idx - 1 != Start.Idx) {
            builder.Append($"-{End.Idx}");
        }
        builder.Append(']');
        return builder.ToString();
    }
}

/// <summary>
/// 单点位置。
/// </summary>
/// <param name="Line">行号，1-based。</param>
/// <param name="Idx">于当前行的位置，0-based。</param>
public record struct Position(int Line, int Idx) {
    public override readonly string ToString() {
        return $"[line {Line} at {Idx + 1}]";
    }
}
