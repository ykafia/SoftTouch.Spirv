using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public readonly struct SortedWordBuffer : ISpirvBuffer
{
    public static SortedWordBuffer Empty { get; } = new();

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
            while (enumerator.MoveNext() && tmp < index)
                tmp += 1;
            return enumerator.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => new(words.Span, words.Memory);

    public SortedWordBuffer()
    {
        words = new();
    }

    public SortedWordBuffer(WordBuffer buffer)
    {
        words = new(buffer.BufferLength);
        var tmpLength = 0;
        foreach (var item in buffer)
        {
            item.Words.CopyTo(words.Span[tmpLength..(tmpLength + item.CountOfWords)]);
            tmpLength += item.CountOfWords;
        }
        buffer.Dispose();
    }
}
