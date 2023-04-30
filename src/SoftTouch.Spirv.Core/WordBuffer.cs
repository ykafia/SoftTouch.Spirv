using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core;

public class WordBuffer
{
    MemoryOwner<int> buffer;
    public int Length { get; private set; }

    public WordBuffer(int initialCapacity = 32)
    {
        Length = 0;
        buffer = MemoryOwner<int>.Allocate(initialCapacity);
    }

    public InstructionEnumerator GetEnumerator() => new(buffer.Span);


    public void Expand(int size)
    {
        if (buffer.Length < Length + size)
        {
            buffer = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Length + size)));
        }
        else
            Length += size;
    }
    public void Add(LiteralString value)
    {
        var chars = MemoryMarshal.Cast<char,int>(value.Value.AsSpan());
        var wordLength = value.WordLength;
        Expand(wordLength);
        var span = buffer.Span[(Length - wordLength)..Length];
        span.Clear();
        chars.CopyTo(span);
    }
    public void Add(int value)
    {
        var start = Length - 1;
        Expand(1);
        buffer.Span[start] = value;
    }
    public void Add(int[] value)
    {
        Expand(value.Length);
        value.AsSpan().CopyTo(buffer.Span[(Length - value.Length)..Length]);
    }
    public void Add(int? value)
    {
        if (value.HasValue)
        {
            var start = Length - 1;
            Expand(1);
            buffer.Span[start] = value.Value;
        }
    }
    public void Add<T>(T value)
        where T : Enum
    {
        Expand(1);
        Add(Convert.ToInt32(value));
    }

}
