namespace CLI;

/// <summary>
/// 日志系统。
/// </summary>
public static class Logging {
    /// <summary>
    /// 信息日志。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogInfo(string msg) {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[Info] {msg}");
        Console.ResetColor();
    }

    /// <summary>
    /// 成功日志。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogSuccess(string msg) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Success] {msg}");
        Console.ResetColor();
    }

    /// <summary>
    /// 错误日志。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogError(string msg) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error] {msg}");
        Console.ResetColor();
    }

    #if DEBUG
    /// <summary>
    /// 调试日志（仅调试模式下编译）。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogDebug(string msg) {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[Debug] {msg}");
        Console.ResetColor();
    }
    #endif
}
