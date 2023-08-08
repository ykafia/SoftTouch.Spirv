using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using SoftTouch.Spirv.Core.Buffers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public partial struct Instruction
{
    ISpirvBuffer Buffer { get; init; }
    public int Index { get; init; }

    int offset;

    public int CountOfWords => Buffer[Index].WordCount;
    public SDSLOp OpCode => Buffer[Index].OpCode;
    public int? ResultId => Buffer[Index].ResultId;
    public int? ResultType => Buffer[Index].ResultType;
    public Span<int> Operands => Buffer[Index].Operands;

    public Span<int> Words => Buffer[Index].Words;

    public Instruction(ISpirvBuffer buffer, int index, int offset = 0)
    {
        Buffer = buffer;
        Index = index;
        this.offset = offset;
    }

    public OperandEnumerator GetEnumerator() => new(RefInstruction.ParseRef(Words));

    public RefInstruction AsRef(int offset = 0) => RefInstruction.ParseRef(Words);

    public static implicit operator IdRef(Instruction i) => new (i.ResultId ?? throw new Exception("Instruction has no result id"));
    public static implicit operator IdResultType(Instruction i) => new(i.ResultId ?? throw new Exception("Instruction has no result id"));

    public override string ToString()
    {
        return OpCode.ToString();
    }
}