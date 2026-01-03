using Information;

namespace Error;

/// <summary>
/// Loxinas 编译错误。
/// </summary>
/// <param name="location">位置信息。</param>
/// <param name="message">错误信息。</param>
public class CompileError(Location location, string message) : LoxinasError(message) {
    /// <summary>
    /// 位置信息。
    /// </summary>
    public Location Location => location;
}
