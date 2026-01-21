using System.Diagnostics;
using System.Text;
using Information;

namespace Compiler;

/// <summary>
/// 词素。
/// </summary>
/// <param name="Location">位置信息。</param>
public record Token(Location Location);

/// <summary>
/// EOF 词素，表示文件尾。
/// </summary>
/// <param name="Location">位置信息（超尾位置）。</param>
public record TokenEOF(Location Location) : Token(Location);

/// <summary>
/// 操作符词素。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Operator">操作符。</param>
public record TokenOperator(Location Location, Operator Operator) : Token(Location);

/// <summary>
/// 标识符词素。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Name">标识符名称。</param>
public record TokenIdentifier(Location Location, string Name) : Token(Location);

/// <summary>
/// 关键字词素。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Keyword">关键字。</param>
public record TokenKeyword(Location Location, Keyword Keyword) : Token(Location);

/// <summary>
/// 数字词素。
/// </summary>
/// <param name="Location">位置信息。</param>
/// <param name="Value"数值</param>
public record TokenNumber(Location Location, IValue Value) : Token(Location);

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
    /// <exception cref="UnreachableException"></exception>
    public static string DebugInfo(this Token token) => token switch {
        TokenEOF tokenEOF => PackInfo("EOF", GetInfo(tokenEOF)),
        TokenOperator tokenOperator => PackInfo("Operator", GetInfo(tokenOperator)),
        TokenIdentifier tokenIdentifier => PackInfo("Identifier", GetInfo(tokenIdentifier)),
        TokenKeyword tokenKeyword => PackInfo("Keyword", GetInfo(tokenKeyword)),
        TokenNumber tokenNumber => PackInfo("Number", GetInfo(tokenNumber)),
        _ => throw new UnreachableException(),
    };

    /// <summary>
    /// 包装词素信息（格式化）。
    /// </summary>
    /// <param name="name">词素名称。</param>
    /// <param name="infoList">词素信息。</param>
    /// <returns>包装后的字符串。</returns>
    private static string PackInfo(string name, List<string> infoList) {
        var builder = new StringBuilder($"Token [{name}] => {{");
        foreach (string info in infoList) {
            builder.Append($"\n    {info}");
        }
        builder.Append("\n}");
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

    /// <summary>
    /// 获取数字词素信息。
    /// </summary>
    /// <param name="token">数字词素。</param>
    /// <returns>词素信息。</returns>
    private static List<string> GetInfo(TokenNumber token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Value: {token.Value.DebugInfo()}");
        return info;
    }
}

#endif
