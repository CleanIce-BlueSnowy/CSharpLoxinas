using Error;

namespace CLI;

/// <summary>
/// 解析之后格式化的命令行参数。
/// </summary>
public class CommandArgs {
    /// <summary>
    /// 输入文件。
    /// </summary>
    public readonly string? InputFile;

    /// <summary>
    /// 解析命令行参数列表。
    /// </summary>
    /// <param name="args">命令行参数列表。</param>
    /// <exception cref="ErrorList"></exception>
    public CommandArgs(string[] args) {
        List<ProgramError> exceptions = [];
        foreach (string arg in args) {
            if (InputFile is null) {
                InputFile = arg;
            } else {
                exceptions.Add(new($"Unknown argument `{arg}`."));
            }
        }
        if (exceptions.Count != 0) {
            throw new ErrorList(exceptions);
        }
    }
}
