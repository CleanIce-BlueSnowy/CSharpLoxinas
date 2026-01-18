using System.Diagnostics;
using System.Text;
using Bytes;
using Information;

namespace IR;

/// <summary>
/// Loxinas 抽象指令。
/// </summary>
public interface IInstruction {
    /// <summary>
    /// 转换为字节序列。
    /// </summary>
    /// <returns>字节序列。</returns>
    public byte[] ToBytes();
}

/// <summary>
/// Loxinas 抽象常量指令。
/// </summary>
/// <param name="value">常量值。</param>
public struct InstConstant(IValue value) : IInstruction {
    /// <summary>
    /// 常量值。
    /// </summary>
    public readonly IValue Value => value;

    /// <summary>
    /// 转换为字节序列。
    /// </summary>
    /// <returns>字节序列。</returns>
    /// <exception cref="UnreachableException"></exception>
    public readonly byte[] ToBytes() {
        List<byte> bytes = [];

        IrCode irCode = value switch {
            ValueInt32 => IrCode.Constant32,
            ValueFloat64 => IrCode.Constant64,
            _ => throw new UnreachableException(),
        };
        bytes.AddRange(irCode.ToBytes());

        bytes.AddRange(value switch {
            ValueInt32(int val) => val.ToBytes(),
            ValueFloat64(double val) => val.ToBytes(),
            _ => throw new UnreachableException(),
        });

        return [..bytes];
    }
}

/// <summary>
/// Loxinas 抽象操作指令。
/// </summary>
/// <param name="opeType">操作数类型。</param>
/// <param name="ope">操作符。</param>
public struct InstOperation(LoxinasType opeType, Operator ope) : IInstruction {
    /// <summary>
    /// 操作数类型。
    /// </summary>
    public readonly LoxinasType OpeType => opeType;

    /// <summary>
    /// 操作符。
    /// </summary>
    public readonly Operator Ope => ope;

    /// <summary>
    /// 转换为字节序列。
    /// </summary>
    /// <returns>字节序列。</returns>
    /// <exception cref="UnreachableException"></exception>
    public readonly byte[] ToBytes() => (ope switch {
        Operator.Add => opeType switch {
            LoxinasType.Int32 => IrCode.IAdd32,
            _ => throw new UnreachableException(),
        },
        _ => throw new UnreachableException(),
    }).ToBytes();
}

#if DEBUG

/// <summary>
/// 为指令添加调试信息。
/// </summary>
public static class InstructionExtendDebug {
    /// <summary>
    /// 获取指令的调试信息。
    /// </summary>
    /// <param name="inst">指令。</param>
    /// <returns>调试信息的字符串表示。</returns>
    /// <exception cref="UnreachableException"></exception>
    public static string DebugInfo(this IInstruction inst) => inst switch {
        InstConstant instConstant => PackInfo("Constant", GetInfo(instConstant)),
        InstOperation instOperation => PackInfo("Operation", GetInfo(instOperation)),
        _ => throw new UnreachableException(),
    };

    /// <summary>
    /// 包装指令信息（格式化）。
    /// </summary>
    /// <param name="name">指令名称。</param>
    /// <param name="infoList">指令信息。</param>
    /// <returns>包装后的字符串。</returns>
    private static string PackInfo(string name, List<string> infoList) {
        var builder = new StringBuilder($"Instruction [{name}] => {{");
        foreach (string info in infoList) {
            builder.Append($"\n    {info}");
        }
        builder.Append("\n}");
        return builder.ToString();
    }

    /// <summary>
    /// 获取常量指令信息。
    /// </summary>
    /// <param name="inst">常量指令。</param>
    /// <returns>指令信息。</returns>
    private static List<string> GetInfo(InstConstant inst) {
        List<string> info = [];
        info.Add($"Value: {inst.Value.DebugInfo()}");
        return info;
    }

    /// <summary>
    /// 获取常量指令信息。
    /// </summary>
    /// <param name="inst">常量指令。</param>
    /// <returns>指令信息。</returns>
    private static List<string> GetInfo(InstOperation inst) {
        List<string> info = [];
        info.Add($"Operation Type: {inst.OpeType.DebugInfo()}");
        info.Add($"Operator: {inst.Ope.DebugInfo()}");
        return info;
    }
}

#endif
