using System.Text;

namespace Information;

/// <summary>
/// 一段位置（<c>End.Idx</c> 是超尾位置）。
/// </summary>
/// <param name="Start">起始点。</param>
/// <param name="End">终止点，为超尾位置。</param>
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

    /// <summary>
    /// 合并两段位置。取左右两端点合并。
    /// </summary>
    /// <param name="start">起始位置</param>
    /// <param name="end">终止位置。</param>
    /// <returns>合并后的位置。</returns>
    public static Location Combine(Location start, Location end) => new(start.Start, end.End);
}

/// <summary>
/// 单点位置。
/// </summary>
/// <param name="Line">行号，1-based。</param>
/// <param name="Idx">于当前行的位置，0-based。</param>
public record struct Position(int Line, int Idx) {
    public override readonly string ToString() => $"[line {Line} at {Idx + 1}]";
}
