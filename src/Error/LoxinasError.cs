namespace Error;

/// <summary>
/// Loxinas 错误。
/// </summary>
/// <param name="message">错误信息。</param>
public abstract class LoxinasError(string message) : Exception(message) {}
