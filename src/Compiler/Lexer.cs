using Information;

namespace Compiler;

public partial class Lexer {
    private readonly string source;
    private readonly Location endLocation;
    private readonly TokenEOF tokenEOF;
    private int startIdx = 0;
    private int currentIdx = 0;
    private Position startPos = new(1, 0);
    private Position endPos = new(1, 0);
    private Token previous;
    private Token current;

    public Token Previous {
        get => previous;
    }

    public Lexer(string source) {
        this.source = source;
        var lines = source.Split();
        var endIdx = lines[^1].Length;
        endLocation = new(new Position(lines.Length, endIdx), new Position(lines.Length, endIdx + 1));
        tokenEOF = new(endLocation);
        previous = tokenEOF;
        current = tokenEOF;
    }

    private Token ScanToken() {
        throw new NotImplementedException();
    }

    private void SkipWhiteSpace() {
        while (true) {
            switch (PeekChar()) {
                case '\n':
                    AdvanceChar();
                    NewLine();
                    break;

                case char ch when char.IsWhiteSpace(ch):
                    AdvanceChar();
                    break;

                default:
                    return;
            }
        }
    }

    private void FreshIndex() {
        startIdx = currentIdx;
        startPos = endPos;
    }

    private void NewLine() {
        endPos.Line++;
        endPos.Idx = 0;
    }

    private char AdvanceChar() {
        if (currentIdx == source.Length) {
            return '\0';
        }
        currentIdx++;
        return source[currentIdx - 1];
    }

    private char PeekChar() {
        if (currentIdx == source.Length) {
            return '\0';
        }
        return source[currentIdx];
    }

    private bool MatchChar(char target) {
        if (PeekChar() == target) {
            AdvanceChar();
            return true;
        } else {
            return false;
        }
    }
}
