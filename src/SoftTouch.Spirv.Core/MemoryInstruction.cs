using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;


namespace SoftTouch.Spirv.Core;

/// <summary>
/// Representation of an instruction from a memory slice.
/// </summary>
/// <param name="Buffer"></param>
/// <param name="Words"></param>
/// <param name="Index"></param>
/// <param name="WordIndex"></param>
public record struct Instruction(ISpirvBuffer Buffer, Memory<int> Words, int Index, int WordIndex)
{
    public static Instruction Empty { get; } = new(null!, Memory<int>.Empty, 0, 0);

    public static implicit operator IdRef(Instruction i) => new(i.ResultId ?? throw new Exception("Instruction has no result id"));
    public static implicit operator IdResultType(Instruction i) => new(i.ResultId ?? throw new Exception("Instruction has no result id"));


    public Instruction(ISpirvBuffer buffer, int index) : this(buffer, Memory<int>.Empty, index, 0)
    {
        Buffer = buffer;
        Index = index;
        var wid = 0;
        for (int i = 0; i < index; i += 1)
            wid += buffer.InstructionSpan[wid] >> 16;
        Words = buffer.InstructionMemory.Slice(wid, buffer.InstructionSpan[wid] >> 16);
        WordIndex = wid;
    }

    public readonly SDSLOp OpCode => AsRef().OpCode;
    public readonly int? ResultId => AsRef().ResultId;
    public readonly int? ResultType => AsRef().ResultType;
    public readonly int WordCount => Words.Length;
    public readonly Memory<int> Operands => Words[1..];

    public bool IsEmpty => Words.IsEmpty;

    public readonly RefInstruction AsRef() => RefInstruction.ParseRef(Words.Span);

    public T? GetOperand<T>(string name) where T : struct, IFromSpirv<T> 
        => AsRef().GetOperand<T>(name);

    public readonly OperandEnumerator GetEnumerator() => AsRef().GetEnumerator();

    public override string ToString()
    {
        return (ResultId == null ? "" : $"%{ResultId} = ") + $"{OpCode} {string.Join(" ", Operands.ToArray().Select(x => x.ToString()))}";
    }
}
