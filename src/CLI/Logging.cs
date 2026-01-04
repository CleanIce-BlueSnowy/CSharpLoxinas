namespace CLI;

public static class Logging {
    public static void LogInfo(string msg) {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[Info] {msg}");
        Console.ResetColor();
    }

    public static void LogSuccess(string msg) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Success] {msg}");
        Console.ResetColor();
    }

    public static void LogError(string msg) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error] {msg}");
        Console.ResetColor();
    }

    #if DEBUG
    public static void LogDebug(string msg) {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[Debug] {msg}");
        Console.ResetColor();
    }
    #endif
}
