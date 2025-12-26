using Error;
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

    public Lexer(string source) {
        this.source = source;
        var lines = source.Split();
        var endIdx = lines[^1].Length;
        endLocation = new(new(lines.Length, endIdx), new(lines.Length, endIdx + 1));
        tokenEOF = new(endLocation);
        previous = tokenEOF;
        current = tokenEOF;
    }

    public Token Previous {
        get => previous;
    }

    public Token Advance() {
        if (AtEnd()) {
            return tokenEOF;
        } else if (current is TokenEOF) {
            UpdateToken();
            UpdateToken();
            return previous;
        } else {
            UpdateToken();
            return previous;
        }
    }

    public Token Peek() {
        if (AtEnd()) {
            return tokenEOF;
        } else if (current is TokenEOF) {
            UpdateToken();
            return current;
        } else {
            return current;
        }
    }

    private void UpdateToken() {
        previous = current;
        current = ScanToken();
    }

    private Token ScanToken() {
        SkipWhiteSpace();
        FreshIndex();

        if (AtEnd()) {
            return tokenEOF;
        }
        
        switch (AdvanceChar()) {
            case '+':
                return new TokenOperator(CurrentLocation(), Operator.Add);
            case '-':
                return new TokenOperator(CurrentLocation(), Operator.Sub);
            case '*':
                return new TokenOperator(CurrentLocation(), Operator.Star);
            case '/':
                return new TokenOperator(CurrentLocation(), Operator.Slash);
            case '=':
                return new TokenOperator(CurrentLocation(), Operator.Equal);
            case char ch when IsIdentifierBeginChar(ch):
                return ScanIdentifier();
            default:
                throw new CompileError(CurrentLocation(), $"Unknown character `{PreviousChar()}`");
        }
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

    private TokenIdentifier ScanIdentifier() {
        while (IsIdentifierChar(PeekChar())) {
            AdvanceChar();
        }

        return new(CurrentLocation(), source.Substring(startIdx, currentIdx - startIdx));
    }
}
