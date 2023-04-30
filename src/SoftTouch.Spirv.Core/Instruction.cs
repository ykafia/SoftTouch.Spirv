using CommunityToolkit.HighPerformance.Buffers;
using System.Net.Sockets;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public partial struct Instruction : IDisposable, ISpirvElement
{
    public Op OpCode { get; init; }
    public int? ResultId { get; set; }
    public int? ResultType { get; set; }
    public OperandArray? Operands { get; init; }

    /// <summary>
    /// Word Count is the high-order 16 bits of word 0 of the instruction, holding its total WordCount. 
    /// <br/> If the instruction takes a variable number of operands, Word Count also says "+ variable", after stating the minimum size of the instruction.
    /// </summary>
    public int WordCount => 
        (Operands?.Length ?? 0)
        + (ResultId.HasValue ? 1 : 0)
        + (ResultType.HasValue ? 1 : 0);


    public void Dispose()
    {
        Operands?.Dispose();
    }

    public void Write(scoped ref SpirvWriter writer)
    {
        writer.Write(WordCount << 16 | (int)OpCode);
        if(ResultType is not null)
            writer.Write(ResultType.Value);
        if(ResultId is not null)
            writer.Write(ResultId.Value);
        if(Operands is not null)
            writer.Write(Operands.Value.Span);
    }


    public static Instruction Parse(Span<int> words)
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

        var operands = new OperandArray(words[index..]);

        return new Instruction()
        {
            OpCode = op,
            ResultId = result,
            ResultType = resultType,
            Operands = operands
        };
    }
    public override string ToString()
    {
        return OpCode.ToString();
    }
}