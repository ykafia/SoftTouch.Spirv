using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;

using static Spv.Specification;


public readonly struct LogicalOperand
{
    public OperandKind OperandKind { get; init; }
    public OperandQuantifier Quantifier { get; init; }
}

public readonly struct InstructionClass
{
    public string Name { get; init; }
    public Op OpCode { get; init; }
    public Capability[] Capability { get; init; }
    public string[] Extensions { get; init; }

}

public partial struct Instruction : IDisposable, ISpirvElement
{
    InstructionClass Info { get; init; }
    public int? ResultId { get; init; }
    public int? ResultType { get; init; }
    OperandArray? Operands { get; init; }



    public void Dispose()
    {
        Operands?.Dispose();
    }

    public void Write(scoped ref SpirvWriter writer)
    {
        writer.Write((int)Info.OpCode);
        if(ResultType is not null)
            writer.Write(ResultType.Value);
        if(ResultId is not null)
            writer.Write(ResultId.Value);
        if(Operands is not null)
            writer.Write(Operands.Span);
    }
}