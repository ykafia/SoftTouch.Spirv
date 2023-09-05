namespace SoftTouch.Spirv.Core.Buffers;


public interface ISpirvBuffer
{
    Span<int> Span { get; }
    Memory<int> Memory { get; }
    Span<int> InstructionSpan { get; }
    Memory<int> InstructionMemory { get; }

    bool HasHeader { get; }

    public Instruction this[int index] {get;}
}