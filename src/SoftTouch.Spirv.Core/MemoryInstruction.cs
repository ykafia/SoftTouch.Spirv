using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;


namespace SoftTouch.Spirv.Core;

public record struct Instruction(ISpirvBuffer Buffer, Memory<int> Words, int Index)
{
    public static Instruction Empty { get; } = new(null!, Memory<int>.Empty, 0);

    public static implicit operator IdRef(Instruction i) => new(i.ResultId ?? throw new Exception("Instruction has no result id"));
    public static implicit operator IdResultType(Instruction i) => new(i.ResultId ?? throw new Exception("Instruction has no result id"));


    public Instruction(ISpirvBuffer buffer, int index) : this(buffer, Memory<int>.Empty, index)
    {
        Buffer = buffer;
        Index = index;
        var wid = 0;
        for(int i = 0; i < index; i+=1)
            wid += buffer.InstructionSpan[wid] >> 16;
        Words = buffer.InstructionMemory.Slice(wid, buffer.Span[wid] >> 16);
    }

    public readonly SDSLOp OpCode => AsRef().OpCode;
    public readonly int? ResultId => AsRef().ResultId;
    public readonly int? ResultType => AsRef().ResultType;
    public readonly int WordCount => Words.Length;
    public readonly Memory<int> Operands => Words[1..];

    public bool IsEmpty => Words.IsEmpty;

    public readonly RefInstruction AsRef() => RefInstruction.ParseRef(Words.Span);

    public T GetOperand<T>(string name) where T : struct, IFromSpirv<T> 
        => AsRef().GetOperand<T>(name);

    public readonly OperandEnumerator GetEnumerator() => AsRef().GetEnumerator();
}
