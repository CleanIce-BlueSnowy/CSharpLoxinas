using System.Text;
using Information;

namespace Compiler;

/// <summary>
/// 词素。
/// </summary>
/// <param name="location">位置信息。</param>
public abstract class Token(Location location) {
    /// <summary>
    /// 位置信息。
    /// </summary>
    public Location Location {
        get => location;
    }
}

/// <summary>
/// EOF 词素，表示文件尾。
/// </summary>
/// <param name="location">位置信息（超尾位置）。</param>
public class TokenEOF(Location location) : Token(location);

/// <summary>
/// 操作符词素。
/// </summary>
/// <param name="location">位置信息。</param>
/// <param name="ope">操作符。</param>
public class TokenOperator(Location location, Operator ope) : Token(location) {
    /// <summary>
    /// 操作符。
    /// </summary>
    public Operator Operator {
        get => ope;
    }
}

/// <summary>
/// 标识符词素。
/// </summary>
/// <param name="location">位置信息。</param>
/// <param name="name">标识符名称。</param>
public class TokenIdentifier(Location location, string name) : Token(location) {
    /// <summary>
    /// 标识符名称。
    /// </summary>
    public string Name {
        get => name;
    }
}

/// <summary>
/// 关键字词素。
/// </summary>
/// <param name="location">位置信息。</param>
/// <param name="keyword">关键字。</param>
public class TokenKeyword(Location location, Keyword keyword) : Token(location) {
    /// <summary>
    /// 关键字。
    /// </summary>
    public Keyword Keyword {
        get => keyword;
    }
}

#if DEBUG

/// <summary>
/// 为词素添加调试信息。
/// </summary>
public static class TokenExtensionsDebug {
    /// <summary>
    /// 获取词素的调试信息。
    /// </summary>
    /// <param name="token">词素。</param>
    /// <returns>调试信息的字符串表示。</returns>
    public static string DebugInfo(this Token token) {
        return token switch {
            TokenEOF tokenEOF => PackTokenInfo("EOF", GetInfo(tokenEOF)),
            TokenOperator tokenOperator => PackTokenInfo("Operator", GetInfo(tokenOperator)),
            TokenIdentifier tokenIdentifier => PackTokenInfo("Identifier", GetInfo(tokenIdentifier)),
            TokenKeyword tokenKeyword => PackTokenInfo("Keyword", GetInfo(tokenKeyword)),
            _ => "## Unknown Token ##",
        };
    }

    /// <summary>
    /// 包装词素信息，进行格式化。
    /// </summary>
    /// <param name="name">词素名称。</param>
    /// <param name="info">词素信息。</param>
    /// <returns>格式化后的字符串。</returns>
    private static string PackTokenInfo(string name, List<string> info) {
        var builder = new StringBuilder($"Token [{name}] => {{");
        builder.Append('\n');
        foreach (string line in info) {
            builder.Append($"    {line}\n");
        }
        builder.Append('}');
        return builder.ToString();
    }

    /// <summary>
    /// 获取 EOF 词素信息。
    /// </summary>
    /// <param name="token">EOF 词素。</param>
    /// <returns>词素信息。</returns>
    private static List<string> GetInfo(TokenEOF token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        return info;
    }

    /// <summary>
    /// 获取操作符词素信息。
    /// </summary>
    /// <param name="token">操作符词素。</param>
    /// <returns>词素信息。</returns>
    private static List<string> GetInfo(TokenOperator token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Operator: {token.Operator.DebugInfo()}");
        return info;
    }

    /// <summary>
    /// 获取标识符词素信息。
    /// </summary>
    /// <param name="token">标识符词素。</param>
    /// <returns>词素信息。</returns>
    private static List<string> GetInfo(TokenIdentifier token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Name: {token.Name}");
        return info;
    }

    /// <summary>
    /// 获取关键字词素信息。
    /// </summary>
    /// <param name="token">关键字词素。</param>
    /// <returns>词素信息。</returns>
    private static List<string> GetInfo(TokenKeyword token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Keyword: {token.Keyword.DebugInfo()}");
        return info;
    }
}

#endif
