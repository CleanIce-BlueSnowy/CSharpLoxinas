using Error;
using Information;

namespace Compiler;

/// <summary>
/// 词法扫描器。
/// </summary>
public partial class Lexer {
    /// <summary>
    /// 正在扫描的词素类型。
    /// </summary>
    private enum ScanningType {
        /// <summary>
        /// 最普通的类型。
        /// </summary>
        None,
        /// <summary>
        /// 名称类型，标识符或关键字。
        /// </summary>
        Identifier,
    }

    /// <summary>
    /// 源代码。
    /// </summary>
    private readonly string source;
    /// <summary>
    /// 源代码超尾位置。
    /// </summary>
    private readonly Location endLocation;
    /// <summary>
    /// 统一生成的 EOF 词素。
    /// </summary>
    private readonly TokenEOF tokenEOF;
    /// <summary>
    /// 词素扫描起始点，相对于整个源代码字符串。
    /// </summary>
    private int startIdx = 0;
    /// <summary>
    /// 即将扫描的字符位置，相对于整个源代码字符串。
    /// </summary>
    private int currentIdx = 0;
    /// <summary>
    /// 词素起始位置。
    /// </summary>
    private Position startPos = new(1, 0);
    /// <summary>
    /// 词素终止位置。
    /// </summary>
    private Position endPos = new(1, 0);
    /// <summary>
    /// 前一个已经扫描过的词素。
    /// </summary>
    private Token previous;
    /// <summary>
    /// 即将扫描的词素。
    /// </summary>
    private Token current;
    /// <summary>
    /// 正在扫描的词素类型。
    /// </summary>
    private ScanningType scanningType = ScanningType.None;

    /// <summary>
    /// 关键字列表。
    /// </summary>
    private static readonly Dictionary<string, Keyword> keywords = new() {
        ["else"] = Keyword.Else,
        ["func"] = Keyword.Func,
        ["if"] = Keyword.If,
    };

    /// <summary>
    /// 通过源代码初始化词法扫描器。
    /// </summary>
    /// <param name="source">源代码</param>
    public Lexer(string source) {
        this.source = source;
        var lines = source.Split('\n');
        var endIdx = lines[^1].Length;
        endLocation = new(new(lines.Length, endIdx), new(lines.Length, endIdx + 1));  // 超尾位置。
        tokenEOF = new(endLocation);
        previous = tokenEOF;
        current = tokenEOF;
    }

    /// <summary>
    /// 前一个已经扫描过的词素。
    /// </summary>
    public Token Previous => previous;

    /// <summary>
    /// 消耗词素。
    /// </summary>
    /// <returns>下一个被扫描的词素。若已经到达源代码结尾，则返回 EOF 词素。</returns>
    public Token Advance() {
        if (AtEnd()) {  // 结尾。
            UpdateToken();  // 移动 previous 和 current。
            return previous;
        } else if (current is TokenEOF) {  // 未开始扫描，需要初始化，进行两次扫描。
            UpdateToken();  // 初始化。
            UpdateToken();  // 消耗扫描。
            return previous;
        } else {  // 扫描并更新词素。
            UpdateToken();
            return previous;
        }
    }

    /// <summary>
    /// 获取下一个即将被扫描的词素。
    /// </summary>
    /// <returns>下一个即将被扫描的词素。若已经到达源代码结尾，则返回 EOF 词素。</returns>
    public Token Peek() {
        if (AtEnd()) {
            return tokenEOF;
        } else if (current is TokenEOF) {  // 未开始扫描，需要初始化。
            UpdateToken();  // 初始化。
            return current;
        } else {
            return current;
        }
    }

    /// <summary>
    /// 遇到错误时，同步扫描器。此方法将扫描器的扫描点从当前出错的词素中移动出来。
    /// </summary>
    public void Synchronize() {
        switch (scanningType) {
            case ScanningType.None:
                break;
            case ScanningType.Identifier:  // 移动到名称末尾。
                while (IsIdentifierChar(PeekChar())) {
                    AdvanceChar();
                }
                break;
        }
    }

    /// <summary>
    /// 更新词素。扫描新词素并移动词素。
    /// </summary>
    private void UpdateToken() {
        previous = current;
        current = ScanToken();
    }

    /// <summary>
    /// 扫描新词素。
    /// </summary>
    /// <returns>新被扫描的词素。</returns>
    /// <exception cref="CompileError"></exception>
    private Token ScanToken() {
        /// 先过滤空白再刷新位置。
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

    /// <summary>
    /// 过滤空白符。
    /// </summary>
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

    /// <summary>
    /// 扫描名称词素。
    /// </summary>
    /// <returns>一个标识符词素或关键字词素。</returns>
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
