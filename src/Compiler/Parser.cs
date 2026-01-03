namespace Compiler;

/// <summary>
/// 语法解析器。
/// </summary>
/// <param name="lexer">词法扫描器。</param>
public partial class Parser(Lexer lexer) {
    private readonly Lexer lexer = lexer;

    public Expr ParseExpression() => ExprEquality();
}
