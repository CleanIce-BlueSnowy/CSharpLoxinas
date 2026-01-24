using CLI;
using IR;

namespace VM;

/// <summary>
/// Loxinas 虚拟机。
/// </summary>
public partial class VirtualMachine {
    /// <summary>
    /// IR 代码字节序列。
    /// </summary>
    private byte[] irCodes;

    /// <summary>
    /// IR 代码字节序列的指令计数。
    /// </summary>
    private int irCnt = 0;

    /// <summary>
    /// 虚拟机值栈。
    /// </summary>
    private readonly List<byte> valueStack = [];

    /// <summary>
    /// Loxinas 虚拟机。
    /// </summary>
    /// <param name="bytes">IR 字节序列。</param>
    public VirtualMachine(byte[] bytes) {
        irCodes = bytes;
    }

    /// <summary>
    /// 运行。
    /// </summary>
    public void Run() {
        while (!IsAtEnd()) {
            IrCode irCode = GetIrCode();

            #if DEBUG
            if (Program.CommandArgs!.DebugLogRunning) {
                PrintCurrentCode(irCode);
                PrintValueStack();
            }
            #endif

            switch (irCode) {
                case IrCode.Constant32: {
                    uint value = ReadInt32();
                    PushInt32(value);
                    break;
                }
                case IrCode.Constant64: {
                    ulong value = ReadInt64();
                    PushInt64(value);
                    break;
                }
                case IrCode.IAdd32: {
                    uint b = PopInt32();
                    uint a = PopInt32();
                    uint res = a + b;
                    PushInt32(res);
                    break;
                }
                case IrCode.ISub32: {
                    uint b = PopInt32();
                    uint a = PopInt32();
                    uint res = a - b;
                    PushInt32(res);
                    break;
                }
            }
        }

        #if DEBUG
        if (Program.CommandArgs!.DebugLogRunning) {
            Logging.LogDebug("Final Stack:");
            PrintValueStack();
        }
        #endif
    }
}
