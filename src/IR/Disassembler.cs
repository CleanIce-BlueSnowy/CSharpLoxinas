using System.Diagnostics;
using System.Text;
using Assistance;
using Bytes;
using Error;

namespace IR;

/// <summary>
/// 中间代码反汇编器。
/// </summary>
/// <param name="bytes">中间代码字节序列</param>
public class Disassembler(byte[] bytes) {
    /// <summary>
    /// 偏移量。
    /// </summary>
    private int offset = 0;

    /// <summary>
    /// 反汇编中间代码。
    /// </summary>
    /// <returns>反汇编结果。</returns>
    public string Disasm() => DisasmChunk();

    /// <summary>
    /// 反汇编中间代码块。
    /// </summary>
    /// <returns>反汇编结果。</returns>
    private string DisasmChunk() {
        var result = new StringBuilder();

        while (!IsAtEnd()) {
            result.Append(DisasmCode());
        }

        return result.ToString();
    }

    /// <summary>
    /// 反汇编单个指令
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DisassemblerError"></exception>
    /// <exception cref="UnreachableException"></exception>
    private string DisasmCode() {
        OffsetCheck(2);

        ushort code = bytes[offset..(offset + 2)].ToUShort();
        if (code >= (ushort)IrCode.MAX_VALID) {
            throw new DisassemblerError($"[At {OffsetToString()}] Unknown IR Code: {code:04X} ({code}).");
        }
        offset += 2;
        IrCode irCode = (IrCode)code;

        return irCode switch {
            IrCode.Constant32 => PackCode("Constant", "4 Bytes", [DisasmValue32()]),
            IrCode.Constant64 => PackCode("Constant", "8 Bytes", [DisasmValue64()]),
            _ => throw new UnreachableException(),
        };
    }

    /// <summary>
    /// 反汇编 32 位数值。
    /// </summary>
    /// <returns>字符串表示。</returns>
    private string DisasmValue32() {
        OffsetCheck(4);

        uint value = bytes[offset..(offset + 4)].ToUInt();
        offset += 4;

        return $"{value:08X} ({value})";
    }

    /// <summary>
    /// 反汇编 64 位数值。
    /// </summary>
    /// <returns>字符串表示。</returns>
    private string DisasmValue64() {
        OffsetCheck(8);

        ulong value = bytes[offset..(offset + 8)].ToULong();
        offset += 8;

        return $"{value:016X} ({value})";
    }

    /// <summary>
    /// 打包一个指令的信息（格式化）。
    /// </summary>
    /// <param name="name">指令名称。</param>
    /// <param name="extra">指令额外信息。</param>
    /// <param name="args">指令参数。</param>
    /// <returns>打包后的字符串。</returns>
    private static string PackCode(string name, string? extra, List<string> args) {
        var builder = new StringBuilder($"{name:15}");
        if (extra is not null) {
            builder.Append($" [{extra.CenterAlign(13)}]  ");
        } else {
            builder.Append(new string(' ', 18));
        }
        foreach (string arg in args) {
            builder.Append(arg);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 将偏移量转换为字符串表示。
    /// </summary>
    /// <returns>字符串表示。</returns>
    private string OffsetToString() => $"{offset:08X}";

    /// <summary>
    /// 检查目前偏移量位置之后是否有足够的字节可以读取。如果没有，则报错。
    /// </summary>
    /// <param name="consume">需要读取的字节数量。</param>
    /// <exception cref="DisassemblerError"></exception>
    private void OffsetCheck(int consume) {
        if (offset + consume > bytes.Length) {
            throw new DisassemblerError($"[At {OffsetToString()}] Need {consume} bytes but not enough.");
        }
    }

    /// <summary>
    /// 检查是否到字节序列末尾。
    /// </summary>
    /// <returns>是/否。</returns>
    private bool IsAtEnd() {
        return offset == bytes.Length;
    }
}
