using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core;

public partial class WordBuffer
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
            var tmp = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Length + size)));
            buffer.Span.CopyTo(tmp.Span);
            buffer = tmp;
        }
        else
            Length += size;
    }

    public void Add(Span<int> words)
    {
        Expand(words.Length);
        words.CopyTo(buffer.Span[(Length-words.Length)..Length]);
    }

    public void AddString(LiteralString value)
    {
        var chars = MemoryMarshal.Cast<char,int>(value.Value.AsSpan());
        var wordLength = value.WordLength;
        Expand(wordLength);
        var span = buffer.Span[(Length - wordLength)..Length];
        span.Clear();
        chars.CopyTo(span);
    }
    public void AddInt(int value)
    {
        var start = Length - 1;
        Expand(1);
        buffer.Span[start] = value;
    }
    public void AddArray(int[] value)
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

    public void Add<T>(T? value)
    {
        if (value != null)
        {
            var start = Length - 1;
            Expand(GetWordLength(value));
            if (value is int i)
                AddInt(i);
            else if (value is int[] array)
                AddArray(array);
            else if (value is string s)
                AddString(s);
            else if (value is Enum e)
                Add(Convert.ToInt32(e));
        }
    }

    public static int GetWordLength<T>(T? value)
    {
        if (value is null) return 0;

        return value switch
        {
            int _ => 1,
            string v => new LiteralString(v).WordLength,
            Enum _ => 1,
            _ => 0
        };
    }
}
