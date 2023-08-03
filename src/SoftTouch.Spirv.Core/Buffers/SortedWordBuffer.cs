using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public sealed class SortedWordBuffer : BufferBase<int>, ISpirvBuffer
{
    public static SortedWordBuffer Empty { get; } = new();
    public int InstructionCount => new SpirvReader(Memory).Count;
    public bool IsEmpty => Span.IsEmpty;


    public RefInstruction this[int index]
    {
        get
        {
            var enumerator = GetEnumerator();
            int tmp = 0;
            while (enumerator.MoveNext() && tmp < index)
                tmp += 1;
            return enumerator.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => new(Span, Memory);

    public SortedWordBuffer()
    {
        _owner = MemoryOwner<int>.Empty;
    }

    public SortedWordBuffer(WordBuffer buffer)
    {
        _owner = MemoryOwner<int>.Allocate(buffer.BufferLength, AllocationMode.Clear);
        var tmpLength = 0;
        foreach (var item in buffer)
        {
            item.Words.CopyTo(Span[tmpLength..(tmpLength + item.CountOfWords)]);
            tmpLength += item.CountOfWords;
        }
        buffer.Dispose();
    }
}
