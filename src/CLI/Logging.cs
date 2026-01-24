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
        string output = $"[Info] {msg}";
        if (Program.LogFile is null) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(output);
            Console.ResetColor();
        } else {
            Program.LogFile.WriteLine(output);
        }
    }

    /// <summary>
    /// 成功日志。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogSuccess(string msg) {
        string output = $"[Success] {msg}";
        if (Program.LogFile is null) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(output);
            Console.ResetColor();
        } else {
            Program.LogFile.WriteLine(output);
        }
    }

    /// <summary>
    /// 错误日志。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogError(string msg) {
        string output = $"[Error] {msg}";
        if (Program.LogFile is null) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(output);
            Console.ResetColor();
        } else {
            Program.LogFile.WriteLine(output);
        }
    }

    #if DEBUG
    /// <summary>
    /// 调试日志（仅调试模式下编译）。
    /// </summary>
    /// <param name="msg">信息。</param>
    public static void LogDebug(string msg) {
        string output = $"[Debug] {msg}";
        if (Program.LogFile is null) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(output);
            Console.ResetColor();
        } else {
            Program.LogFile.WriteLine(output);
        }
    }
    #endif
}
