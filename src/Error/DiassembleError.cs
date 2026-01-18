namespace Error;

/// <summary>
/// Loxinas 反汇编器错误。
/// </summary>
/// <param name="message">错误信息。</param>
public class DisassembleError(string message): LoxinasError(message);
