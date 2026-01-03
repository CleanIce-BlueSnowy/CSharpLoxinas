using Information;

namespace Compiler;

/// <summary>
/// 词法扫描器。
/// </summary>
public partial class Lexer {
    /// <summary>
    /// 判断是否为标识符起始字符。
    /// </summary>
    /// <param name="ch">字符</param>
    /// <returns>是/否。</returns>
    private static bool IsIdentifierBeginChar(char ch) {
        return char.IsLetter(ch) || ch == '_';
    }

    /// <summary>
    /// 判断是否为标识符字符。
    /// </summary>
    /// <param name="ch">字符</param>
    /// <returns>是/否。</returns>
    private static bool IsIdentifierChar(char ch) {
        return char.IsLetterOrDigit(ch) || ch == '_';
    }

    /// <summary>
    /// 获取当前词素的位置信息。
    /// </summary>
    /// <returns>当前词素的位置信息。</returns>
    private Location CurrentLocation() {
        return new(startPos, endPos);
    }

    /// <summary>
    /// 刷新词素。此方法会重置词素的起始位置信息，对齐到当前扫描位置。
    /// </summary>
    private void Fresh() {
        scanningType = ScanningType.None;
        startIdx = currentIdx;
        startPos = endPos;
    }

    /// <summary>
    /// 遇到换行符的位置信息更新。
    /// </summary>
    private void NewLine() {
        endPos.Line++;
        endPos.Idx = 0;
    }

    /// <summary>
    /// 消耗字符。
    /// </summary>
    /// <returns>消耗的字符。若已到结尾则返回空字符。</returns>
    private char AdvanceChar() {
        if (AtEnd()) {
            return '\0';
        }
        currentIdx++;
        endPos.Idx++;
        return source[currentIdx - 1];
    }

    /// <summary>
    /// 获取下一个即将扫描的字符。
    /// </summary>
    /// <returns>即将扫描的字符。若已到结尾则返回空字符。</returns>
    private char PeekChar() {
        if (AtEnd()) {
            return '\0';
        }
        return source[currentIdx];
    }

    /// <summary>
    /// 获取前一个已经被扫描过的字符。
    /// </summary>
    /// <returns>前一个字符。若没有扫描过字符，则返回空字符。</returns>
    private char PreviousChar() {
        if (currentIdx == 0) {
            return '\0';
        }
        return source[currentIdx - 1];
    }

    /// <summary>
    /// 匹配消耗字符。若下一个字符与目标匹配，则消耗；若不匹配，则不做任何事情。
    /// </summary>
    /// <param name="target">目标字符。</param>
    /// <returns>是否匹配。</returns>
    private bool MatchChar(char target) {
        if (PeekChar() == target) {
            AdvanceChar();
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 判断是否扫描到源代码结尾。
    /// </summary>
    /// <returns>是/否。</returns>
    private bool AtEnd() {
        return currentIdx == source.Length;
    }
}
