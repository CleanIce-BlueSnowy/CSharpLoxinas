using System.Text;

namespace Information;

public record struct Location(Position Start, Position End) {
    public override readonly string ToString() {
        var builder = new StringBuilder($"[line {Start.Line}");
        if (End.Line != Start.Line) {
            builder.Append($"-{End.Line}");
        }
        builder.Append($" at {Start.Idx}");
        if (End.Idx - 1 != Start.Idx) {
            builder.Append($"-{End.Idx}");
        }
        builder.Append(']');
        return builder.ToString();
    }
}

public record struct Position(int Line, int Idx) {
    public override readonly string ToString() {
        return $"[line {Line} at {Idx}]";
    }
}
