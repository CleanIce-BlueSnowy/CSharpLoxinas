using System.Diagnostics;
using System.Text;
using Compiler;

namespace Debug;

#if DEBUG

/// <summary>
/// AST 打印器（调试使用）。
/// </summary>
public static partial class AstPrinter {
    /// <summary>
    /// 打印表达式。
    /// </summary>
    /// <param name="expr">表达式。</param>
    /// <returns>打印结果。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string Print(IExpr expr) => expr switch {
        ExprUnary exprUnary => PackInfo("Expr", "Unary", GetInfo(exprUnary)),
        ExprBinary exprBinary => PackInfo("Expr", "Binary", GetInfo(exprBinary)),
        ExprVariable exprVariable => PackInfo("Expr", "Variable", GetInfo(exprVariable)),
        ExprLiteral exprLiteral => PackInfo("Expr", "Literal", GetInfo(exprLiteral)),
        _ => throw new UnreachableException(),
    };

    /// <summary>
    /// 打包 AST 节点信息（格式化）。
    /// </summary>
    /// <param name="type">节点类型。</param>
    /// <param name="name">名称。</param>
    /// <param name="infoList">信息列表。</param>
    /// <returns>打包之后的字符串。</returns>
    private static string PackInfo(string type, string name, List<string> infoList) {
        var builder = new StringBuilder($"{type} [{name}] => {{");
        foreach (var info in infoList) {
            builder.Append($"\n    {info}");
        }
        builder.Append("\n}");
        return builder.ToString();
    }

    /// <summary>
    /// 将源字符串进行缩进（除第一行外）。
    /// </summary>
    /// <param name="source">源字符串。</param>
    /// <returns>缩进之后的字符串。</returns>
    private static string Indent(string source) {
        string[] lines = source.Split('\n');
        var builder = new StringBuilder(lines[0]);
        foreach (string line in lines[1..]) {
            if (builder.Length != 0) {
                builder.Append('\n');
            }
            builder.Append($"    {line}");
        }
        return builder.ToString();
    }
}

#endif
