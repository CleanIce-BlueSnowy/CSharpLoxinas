using System.Text;
using Information;

namespace Compiler;

public abstract class Token(Location location) {
    public Location Location {
        get => location;
    }
}

public class TokenEOF(Location location) : Token(location);

public class TokenOperator(Location location, Operator ope) : Token(location) {
    public Operator Operator {
        get => ope;
    }
}

public class TokenIdentifier(Location location, string name) : Token(location) {
    public string Name {
        get => name;
    }
}

public class TokenKeyword(Location location, Keyword keyword) : Token(location) {
    public Keyword Keyword {
        get => keyword;
    }
}

#if DEBUG

public static class TokenExtensionsDebug {
    public static string DebugInfo(this Token token) {
        return token switch {
            TokenEOF tokenEOF => PackTokenInfo("EOF", GetInfo(tokenEOF)),
            TokenOperator tokenOperator => PackTokenInfo("Operator", GetInfo(tokenOperator)),
            TokenIdentifier tokenIdentifier => PackTokenInfo("Identifier", GetInfo(tokenIdentifier)),
            TokenKeyword tokenKeyword => PackTokenInfo("Keyword", GetInfo(tokenKeyword)),
            _ => "## Unknown Token ##",
        };
    }

    private static string PackTokenInfo(string name, List<string>? info) {
        var builder = new StringBuilder($"Token [{name}] => {{");
        if (info is not null) {
            builder.Append('\n');
            foreach (string line in info) {
                builder.Append($"    {line}\n");
            }
        }
        builder.Append('}');
        return builder.ToString();
    }

    private static List<string> GetInfo(TokenEOF token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        return info;
    }

    private static List<string> GetInfo(TokenOperator token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Operator: {token.Operator.DebugInfo()}");
        return info;
    }

    private static List<string> GetInfo(TokenIdentifier token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Name: {token.Name}");
        return info;
    }

    private static List<string> GetInfo(TokenKeyword token) {
        List<string> info = [];
        info.Add($"Location: {token.Location}");
        info.Add($"Keyword: {token.Keyword.DebugInfo()}");
        return info;
    }
}

#endif
