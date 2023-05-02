using CommunityToolkit.HighPerformance.Buffers;
using System.Net.Sockets;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public ref struct RefInstruction
{

    public Op OpCode { get; init; }
    public int? ResultId { get; set; }
    public int? ResultType { get; set; }
    public Span<int> Operands { get; init; }
    public Memory<int>? Slice { get; init; }
    public int OwnerIndex { get; set; }

    /// <summary>
    /// Word Count is the high-order 16 bits of word 0 of the instruction, holding its total WordCount. 
    /// <br/> If the instruction takes a variable number of operands, Word Count also says "+ variable", after stating the minimum size of the instruction.
    /// </summary>
    public int WordCount => 
        Operands.Length
        + (ResultId.HasValue ? 1 : 0)
        + (ResultType.HasValue ? 1 : 0);


    public static RefInstruction Parse(Memory<int> owner, int ownerIndex)
    {
        var words = owner.Span.Slice(ownerIndex, owner.Span[ownerIndex] >> 16);
        var index = 0;
        var op = (Op)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);
        if (info.HasResult)
            result = words[++index];
        if (info.HasResultType)
            resultType = words[++index];

        return new RefInstruction()
        {
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = words[index..],
            OwnerIndex = ownerIndex,
            Slice = owner
        };
    }
    public static RefInstruction ParseRef(Span<int> words)
    {
        var index = 0;
        var op = (Op)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);
        if (info.HasResult)
            result = words[++index];
        if (info.HasResultType)
            resultType = words[++index];

        return new RefInstruction()
        {
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = words[index..]
        };
    }

    public bool ToOwned(out OwnedInstruction? instruction)
    {
        if (Slice == null)
        {
            instruction = null;
            return false;
        }
        else
        {
            instruction = new()
            {
                OpCode = OpCode,
                ResultId = ResultId,
                ResultType = ResultType,
                Operands = Slice.Value
            };
            return true;
        }
    }

    public override string ToString()
    {
        return OpCode.ToString();
    }
}