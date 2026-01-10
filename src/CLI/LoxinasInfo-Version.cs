using System.Runtime.InteropServices;

namespace CLI;

/// <summary>
/// 获取 Loxinas 信息。
/// </summary>
public static partial class LoxinasInfo {
    /// <summary>
    /// 获取操作系统信息。
    /// </summary>
    /// <returns>操作系统信息。</returns>
    private static string GetOSInfo() {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            return GetWindowsInfo();
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            return GetLinuxInfo();
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            return GetMacOSInfo();
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)) {
            return "FreeBSD";
        } else {
            return Environment.OSVersion.ToString();
        }
    }

    /// <summary>
    /// 获取 Windows 操作系统信息。
    /// </summary>
    /// <returns>Windows 操作系统信息。</returns>
    private static string GetWindowsInfo() {
        var osVersion = Environment.OSVersion;
        string osName = "Windows";

        if (osVersion.Version.Major == 10) {
            if (osVersion.Version.Build >= 22000) {
                osName = "Windows 11";
            } else {
                osName = "Windows 10";
            }
        } else if (osVersion.Version.Major == 6) {
            osName = osVersion.Version.Minor switch {
                1 => "Windows 7",
                2 => "Windows 8",
                3 => "Windows 8.1",
                _ => osName,
            };
        }

        return $"{osName} {osVersion.Version.Major}.{osVersion.Version.Minor}";
    }

    /// <summary>
    /// 获取 Linux 操作系统信息。
    /// </summary>
    /// <returns>Linux 操作系统信息。</returns>
    private static string GetLinuxInfo() {
        try {
            if (File.Exists("/etc/os-release")) {
                string[] lines = File.ReadAllLines("/etc/os-release");
                bool foundPrettyName = false;
                string? prettyName = null;
                string versionId = "";

                foreach (string line in lines) {
                    if (line.StartsWith("PRETTY_NAME=")) {
                        foundPrettyName = true;
                        prettyName = line["PRETTY_NAME=".Length..].Trim('"');
                    } else if (line.StartsWith("NAME=")) {
                        prettyName ??= line["NAME=".Length..].Trim('"');
                    } else if (line.StartsWith("VESION_ID=")) {
                        versionId = line["VERSION_ID=".Length..].Trim('"');
                    }
                }

                prettyName ??= "Linux";
                if (foundPrettyName) {
                    return prettyName;
                } else {
                    return $"{prettyName} {versionId}";
                }
            } else {
                return "Linux";
            }
        } catch {
            return "Linux";
        }
    }

    /// <summary>
    /// 获取 macOS 操作系统信息。
    /// </summary>
    /// <returns>macOS 操作系统信息。</returns>
    private static string GetMacOSInfo() {
        try {
            var startInfo = new System.Diagnostics.ProcessStartInfo {
                FileName = "sw_vers",
                Arguments = "-productVersion",
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            using var process = System.Diagnostics.Process.Start(startInfo);
            string? version = process?.StandardOutput.ReadToEnd().Trim();
            return $"macOS {version ?? ""}";
        } catch {
            var osVersion = Environment.OSVersion;
            return $"macOS {osVersion.Version.Major}.{osVersion.Version.Minor}";
        }
    }

    /// <summary>
    /// 获取架构信息。
    /// </summary>
    /// <returns>架构信息。</returns>
    private static string GetArchitecture() => RuntimeInformation.OSArchitecture.ToString().ToLower();
}
