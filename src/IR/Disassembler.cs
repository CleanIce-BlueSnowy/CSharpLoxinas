using System.Diagnostics;
using System.Text;
using Assistance;
using Bytes;
using Error;

namespace IR;

public class Disassembler(byte[] bytes) {
    private int idx = 0;

    private string DisasmCode() {
        IdxCheck(2);

        ushort code = bytes[idx..(idx + 2)].ToUShort();
        if (code > (ushort)IrCode.MAX_VALID) {
            throw new DisassemblerError($"[At {IdxToString()}] Unknown IR Code: {code:04X} ({code}).");
        }
        idx += 2;
        IrCode irCode = (IrCode)code;

        return irCode switch {
            IrCode.Constant32 => PackCode("Constant", "4 Bytes", [DisasmValue32()]),
            _ => throw new UnreachableException(),
        };
    }

    private string DisasmValue32() {
        IdxCheck(4);

        uint value = bytes[idx..(idx + 4)].ToUInt();
        idx += 4;

        return $"{value:08X} ({value})";
    }

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

    private string IdxToString() => $"{idx:08X}";

    private void IdxCheck(int consume) {
        if (idx + consume > bytes.Length) {
            throw new DisassemblerError($"[At {IdxToString()}] Need {consume} bytes but not enough.");
        }
    }
}
