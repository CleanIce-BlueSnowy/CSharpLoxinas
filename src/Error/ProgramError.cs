namespace Error;

/// <summary>
/// Loxinas 编译器程序错误。
/// </summary>
/// <param name="message">错误信息。</param>
public class ProgramError(string message) : LoxinasError(message) {}
