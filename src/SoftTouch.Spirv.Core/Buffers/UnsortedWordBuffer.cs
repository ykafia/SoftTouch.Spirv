using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public readonly struct UnsortedWordBuffer : ISpirvBuffer
{
    public static readonly SortedWordBuffer Empty = new ();

    readonly ExpandableBuffer<int> words;
    public Span<int> Span => words.Span;
    public Memory<int> Memory => words.Memory;
    public int Count => new SpirvReader(words.Memory).Count;
    public bool IsEmpty => words.Span.IsEmpty;
    
    
    public RefInstruction this[int index]
    {
        get
        {
            var enumerator = GetEnumerator();
            int tmp = 0;
            while(enumerator.MoveNext() && tmp < index)
                tmp += 1;
            return enumerator.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => new(words.Span, words.Memory);

    public UnsortedWordBuffer()
    {
        words = new();
    }

    public UnsortedWordBuffer(WordBuffer buffer)
    {
        words = new(buffer.BufferLength);
        buffer.Span.CopyTo(words.Span);
        buffer.Dispose();
    }
}
