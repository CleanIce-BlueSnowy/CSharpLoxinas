namespace Error;

/// <summary>
/// 错误列表。
/// </summary>
/// <param name="errors">所有错误。</param>
public class ErrorList(IEnumerable<LoxinasError> errors) : Exception {
    /// <summary>
    /// 错误列表。
    /// </summary>
    public IEnumerable<LoxinasError> Errors => errors;
}
