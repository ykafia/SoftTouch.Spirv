namespace SoftTouch.Spirv.Core.Buffers;



public class ComposableBuffer
{
    public int Bound { get; set; }

    public WordBuffer Declarations { get; init; }
    public FunctionBufferCollection Functions { get; init; }

    public ComposableBuffer()
    {
        Declarations = new();
        Functions = new();
    }

    public void Add(MutRefInstruction instruction)
    {
        if(InstructionInfo.GetGroupOrder(instruction) == 13)
            Functions.Insert(instruction);
        else
            Declarations.Add(instruction);

    }
}