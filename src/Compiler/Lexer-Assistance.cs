using Information;

namespace Compiler;

public partial class Lexer {
    private static bool IsIdentifierBeginChar(char ch) {
        return char.IsLetter(ch) || ch == '_';
    }

    private static bool IsIdentifierChar(char ch) {
        return char.IsLetterOrDigit(ch) || ch == '_';
    }

    private Location CurrentLocation() {
        return new(startPos, endPos);
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
        if (AtEnd()) {
            return '\0';
        }
        currentIdx++;
        endPos.Idx++;
        return source[currentIdx - 1];
    }

    private char PeekChar() {
        if (AtEnd()) {
            return '\0';
        }
        return source[currentIdx];
    }

    private char PreviousChar() {
        if (currentIdx == 0) {
            return '\0';
        }
        return source[currentIdx - 1];
    }

    private bool MatchChar(char target) {
        if (PeekChar() == target) {
            AdvanceChar();
            return true;
        } else {
            return false;
        }
    }

    private bool AtEnd() {
        return currentIdx == source.Length;
    }
}
