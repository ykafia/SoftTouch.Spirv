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
    public override Span<int> Span => _owner.Span[5..];
    public override Memory<int> Memory => _owner.Memory[5..];
    public RefHeader Header => new(_owner.Span[..5]);
    public RefInstructions Instructions => new(Memory);


    public SpirvBuffer()
    {
        _owner = MemoryOwner<int>.Allocate(32,AllocationMode.Clear);
        var header = Header;
        header.MagicNumber = Spv.Specification.MagicNumber;
        header.VersionNumber = "1.3";
        header.GeneratorMagicNumber = 42;
    }

    public RefInstructions.Enumerator GetEnumerator() => Instructions.GetEnumerator();

    public void Add(SortedWordBuffer buffer) => Add(buffer.Span);
}
