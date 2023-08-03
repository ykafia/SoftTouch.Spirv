using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;



public ref struct MixinRefInstruction
{
    public string MixinName { get; init; }
    public RefInstruction Instruction { get; init; }

    public SDSLOp OpCode => Instruction.OpCode;
    public int? ResultId => Instruction.ResultId;
    public int? ResultType => Instruction.ResultType;
    public Span<int> Words => Instruction.Words;
    public bool IsEmpty => Instruction.IsEmpty;

    public static implicit operator MixinRefInstruction(RefInstruction instruction) => new("",instruction);    

    public MixinRefInstruction(string mixinName, RefInstruction instruction)
    {
        MixinName = mixinName;
        Instruction = instruction;
    }
}
