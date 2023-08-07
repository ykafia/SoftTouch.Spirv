using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Buffers;

public class SpirvBuffer : ExpandableBuffer<int>, ISpirvBuffer
{
    public Span<int> InstructionSpan => _owner.Span[5..Length];
    public Memory<int> InstructionMemory => _owner.Memory[5..Length];
    public RefHeader Header => new(_owner.Span[..5]);
    public RefInstructions Instructions => new(InstructionMemory);


    public SpirvBuffer()
    {
        _owner = MemoryOwner<int>.Allocate(32,AllocationMode.Clear);
        var header = Header;
        header.MagicNumber = Spv.Specification.MagicNumber;
        header.VersionNumber = new(1,3);
        header.GeneratorMagicNumber = 42;
        Length = 5;
    }

    public RefInstructions.Enumerator GetEnumerator() => Instructions.GetEnumerator();

    public void Add(SortedWordBuffer buffer) => Add(buffer.Span);

    public override string ToString()
    {
        return new Disassembler().Disassemble(this);
    }
}
