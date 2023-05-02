using CommunityToolkit.HighPerformance.Buffers;
using System.Net.Sockets;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public struct OwnedInstruction
{

    public Op OpCode { get; init; }
    public int? ResultId { get; set; }
    public int? ResultType { get; set; }
    public Memory<int> Operands { get; init; }

    /// <summary>
    /// Word Count is the high-order 16 bits of word 0 of the instruction, holding its total WordCount. 
    /// <br/> If the instruction takes a variable number of operands, Word Count also says "+ variable", after stating the minimum size of the instruction.
    /// </summary>
    public int WordCount => 
        Operands.Length
        + (ResultId.HasValue ? 1 : 0)
        + (ResultType.HasValue ? 1 : 0);


    public static void Parse(in Memory<int> words, out OwnedInstruction instruction)
    {
        var index = 0;
        var op = (Op)(words.Span[0] & 0xFFFF);
        var wordLength = words.Span[0] >> 16;
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);
        if (info.HasResult)
            result = words.Span[++index];
        if (info.HasResultType)
            resultType = words.Span[++index];

        instruction = new OwnedInstruction()
        {
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = words.Slice(index, wordLength)
        };
    }


    public override string ToString()
    {
        return OpCode.ToString();
    }
}