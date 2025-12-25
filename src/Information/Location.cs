namespace Information;

public record struct Location(Position Start, Position End);

public record struct Position(int Line, int Idx);
