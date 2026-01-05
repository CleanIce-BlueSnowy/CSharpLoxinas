using Compiler;
using Information;

namespace Debug;

#if DEBUG

public static partial class AstPrinter {
    /// <summary>
    /// 获取一元操作符表达式信息。
    /// </summary>
    /// <param name="expr">一元操作符表达式。</param>
    /// <returns>表达式信息。</returns>
    private static List<string> GetInfo(ExprUnary expr) {
        List<string> info = [];
        info.Add($"Location: {expr.Location}");
        info.Add($"Operator: {Indent(expr.Operator.DebugInfo())}");
        info.Add($"Rhs: {Indent(Print(expr.Rhs))}");
        return info;
    }

    /// <summary>
    /// 获取二元操作符表达式信息。
    /// </summary>
    /// <param name="expr">二元操作符表达式。</param>
    /// <returns>表达式信息。</returns>
    private static List<string> GetInfo(ExprBinary expr) {
        List<string> info = [];
        info.Add($"Location: {expr.Location}");
        info.Add($"Operator: {Indent(expr.Operator.DebugInfo())}");
        info.Add($"Lhs: {Indent(Print(expr.Lhs))}");
        info.Add($"Rhs: {Indent(Print(expr.Rhs))}");
        return info;
    }

    /// <summary>
    /// 获取变量表达式信息。
    /// </summary>
    /// <param name="expr">变量表达式。</param>
    /// <returns>表达式信息。</returns>
    private static List<string> GetInfo(ExprVariable expr) {
        List<string> info = [];
        info.Add($"Location: {expr.Location}");
        info.Add($"Name: {Indent(expr.Name.DebugInfo())}");
        return info;
    }

    /// <summary>
    /// 获取字面量表达式信息。
    /// </summary>
    /// <param name="expr">字面量表达式。</param>
    /// <returns>表达式信息。</returns>
    private static List<string> GetInfo(ExprLiteral expr) {
        List<string> info = [];
        info.Add($"Location: {expr.Location}");
        info.Add($"Value: {expr.Value.DebugInfo()}");
        return info;
    }
}

#endif
