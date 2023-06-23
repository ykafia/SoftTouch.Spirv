using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;


namespace SoftTouch.Spirv.Core;


public ref struct RefInstruction
{
    public int CountOfWords { get; init; }
    public SDSLOp OpCode { get; init; }
    public int? ResultId { get; set; }
    public int? ResultType { get; set; }
    public Span<int> Operands { get; init; }
    public Memory<int>? Slice { get; init; }
    public int OwnerIndex { get; set; }
    public int IdRefOffset { get; set; }
    public Span<int> Words { get; init; }

    /// <summary>
    /// Word Count is the high-order 16 bits of word 0 of the instruction, holding its total WordCount. 
    /// <br/> If the instruction takes a variable number of operands, Word Count also says "+ variable", after stating the minimum size of the instruction.
    /// </summary>
    public int WordCount =>
        Operands.Length
        + (ResultId.HasValue ? 1 : 0)
        + (ResultType.HasValue ? 1 : 0);


    public OperandEnumerator GetEnumerator() => new(this);

    public static RefInstruction Parse(Memory<int> owner, int ownerIndex)
    {
        var words = owner.Span.Slice(ownerIndex, owner.Span[ownerIndex] >> 16);
        var index = 0;
        var op = (SDSLOp)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);
        if (info.HasResultType)
            resultType = words[++index];
        if (info.HasResult)
            result = words[++index];


        return new RefInstruction()
        {
            CountOfWords = words[0] >> 16,
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = words[index..],
            OwnerIndex = ownerIndex,
            Slice = owner,
            Words = words
        };
    }
    public static RefInstruction ParseRef(Span<int> words, int offset = 0)
    {
        var index = 0;
        var op = (SDSLOp)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);

        if (info.HasResultType)
            resultType = words[++index];
        if (info.HasResult)
            result = words[++index];

        return new RefInstruction()
        {
            CountOfWords = words[0] >> 16,
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = words[index..],
            Words = words,
            IdRefOffset = offset
        };
    }
    

    public bool ToOwned(out OwnedInstruction instruction)
    {
        if (Slice == null)
        {
            instruction = new();
            return false;
        }
        else
        {
            instruction = new()
            {
                OpCode = OpCode,
                ResultId = ResultId,
                ResultType = ResultType,
                Operands = Slice.Value[OwnerIndex..(OwnerIndex + CountOfWords)]
            };
            return true;
        }
    }


    public void SetResultId(int id)
    {
        var info = InstructionInfo.GetInfo(OpCode);

        if (info.HasResult)
        {
            int i = 0;
            while (info[i].Kind != OperandKind.IdResult) { i += 1; }
            Operands[id] = id;
        }
    }
    public void SetResultType(int id)
    {
        var info = InstructionInfo.GetInfo(OpCode);

        if (info.HasResultType)
        {
            int i = 0;
            while (info[i].Kind != OperandKind.IdResultType) { i += 1; }
            Operands[id] = id;
        }
    }
    public void ReplaceIdRef(int toReplace, int value)
    {
        var info = InstructionInfo.GetInfo(OpCode);
        for (int i = 0; i < info.Count; i++)
        {
            if (info[i].Kind == OperandKind.IdRef && Operands[i] == toReplace)
                Operands[i] = value;
        }
    }
    public void Offset(int value)
    {
        foreach (var o in this)
            o.OffsetIdRef(value);
    }

    public override string ToString()
    {
        return OpCode.ToString();
    }
}