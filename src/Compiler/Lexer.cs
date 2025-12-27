using System.Text.RegularExpressions;
using Error;
using Information;

namespace Compiler;

public partial class Lexer {
    private enum ScanningType {
        None,
        Identifier,
    }

    private readonly string source;
    private readonly Location endLocation;
    private readonly TokenEOF tokenEOF;
    private int startIdx = 0;
    private int currentIdx = 0;
    private Position startPos = new(1, 0);
    private Position endPos = new(1, 0);
    private Token previous;
    private Token current;
    private ScanningType scanningType = ScanningType.None;

    private static readonly Dictionary<string, Keyword> keywords = new() {
        ["else"] = Keyword.Else,
        ["func"] = Keyword.Func,
        ["if"] = Keyword.If,
    };

    public Lexer(string source) {
        this.source = source;
        var lines = source.Split('\n');
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
            UpdateToken();
            return previous;
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

    public void Synchronize() {
        switch (scanningType) {
            case ScanningType.None:
                break;
            case ScanningType.Identifier:
                while (IsIdentifierChar(PeekChar())) {
                    AdvanceChar();
                }
                break;
        }
    }

    private void UpdateToken() {
        previous = current;
        current = ScanToken();
    }

    private Token ScanToken() {
        SkipWhiteSpace();
        Fresh();

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
                Operator ope;
                if (MatchChar('=')) {
                    ope = Operator.EqualEqual;
                } else {
                    ope = Operator.Equal;
                }
                return new TokenOperator(CurrentLocation(), ope);
            case char ch when IsIdentifierBeginChar(ch):
                return ScanName();
            default:
                throw new CompileError(CurrentLocation(), $"Unknown character `{PreviousChar()}`.");
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

    private Token ScanName() {
        scanningType = ScanningType.Identifier;

        while (IsIdentifierChar(PeekChar())) {
            AdvanceChar();
        }

        string name = source[startIdx..currentIdx];

        if (keywords.TryGetValue(name, out Keyword keyword)) {
            return new TokenKeyword(CurrentLocation(), keyword);
        } else {
            return new TokenIdentifier(CurrentLocation(), name);
        }
    }
}
