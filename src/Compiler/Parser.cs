namespace Compiler;

/// <summary>
/// 语法解析器。
/// </summary>
/// <param name="lexer">词法扫描器。</param>
public partial class Parser(Lexer lexer) {
    /// <summary>
    /// 此法扫描器。
    /// </summary>
    private readonly Lexer lexer = lexer;

    /// <summary>
    /// 解析表达式。
    /// </summary>
    /// <returns>表达式。</returns>
    public IExpr ParseExpression() => ExprEquality();
}
