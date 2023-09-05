using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;



public ref struct MixinInstruction
{
    public string MixinName { get; init; }
    public Instruction Instruction { get; init; }

    public SDSLOp OpCode => Instruction.OpCode;
    public int? ResultId => Instruction.ResultId;
    public int? ResultType => Instruction.ResultType;
    public Span<int> Words => Instruction.Words.Span;
    public bool IsEmpty => Instruction.IsEmpty;

    public static implicit operator MixinInstruction(Instruction instruction) => new("",instruction);    

    public MixinInstruction(string mixinName, Instruction instruction)
    {
        MixinName = mixinName;
        Instruction = instruction;
    }
}
