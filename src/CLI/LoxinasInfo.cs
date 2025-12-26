namespace CLI;

public static partial class LoxinasInfo {
    private static string version = "0.0.1-alpha";

    public static string Version {
        get {
            string osInfo = GetOSInfo();
            string architecture = GetArchitecture();

            #if DEBUG
            return $"Loxinas {version} (Debug Mode) [{osInfo} on {architecture}]";
            #else
            return $"Loxinas {version} [{osInfo} on {architecture}]";
            #endif
        }
    }
}
