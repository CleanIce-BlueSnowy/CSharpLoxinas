namespace CLI;

/// <summary>
/// 获取 Loxinas 信息。
/// </summary>
public static partial class LoxinasInfo {
    /// <summary>
    /// 版本号。
    /// </summary>
    private const string version = "0.0.1-alpha";

    /// <summary>
    /// 版本信息。
    /// </summary>
    public static string Version {
        get {
            string osInfo = GetOSInfo();
            string architecture = GetArchitecture();

            #if DEBUG
            return $"Loxinas {version} (Debug Mode) [{osInfo} on {architecture}]";  // 调试模式。
            #else
            return $"Loxinas {version} [{osInfo} on {architecture}]";
            #endif
        }
    }
}
