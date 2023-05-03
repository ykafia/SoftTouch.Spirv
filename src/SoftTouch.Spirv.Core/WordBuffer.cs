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
    public int BufferLength { get; private set; }
    public int Count => new SpirvReader(buffer[..BufferLength]).Count;

    public RefInstruction this[int index]
    {
        get
        {
            int id = 0;
            int wid = 0;
            while(id < index)
            {
                wid += buffer.Span[wid] >> 16;
                id++;
            }
            return RefInstruction.ParseRef(buffer.Span.Slice(wid,buffer.Span[wid] >> 16));
        }
    }

    public WordBuffer(int initialCapacity = 32)
    {
        BufferLength = 0;
        buffer = MemoryOwner<int>.Allocate(initialCapacity);
    }
    public WordBuffer(MemoryOwner<int> data)
    {
        buffer = data;
        BufferLength = Count;
    }

    public InstructionEnumerator GetEnumerator() => new(buffer.Span);


    public void Expand(int size)
    {
        if (buffer.Length < BufferLength + size)
        {
            var tmp = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(BufferLength + size)));
            buffer.Span.CopyTo(tmp.Span);
            buffer = tmp;
        }
        else
            BufferLength += size;
    }

    internal void Add(Span<int> words)
    {
        Expand(words.Length);
        words.CopyTo(buffer.Span[(BufferLength-words.Length)..BufferLength]);
    }

    internal void AddString(LiteralString value)
    {
        var chars = MemoryMarshal.Cast<char,int>(value.Value.AsSpan());
        var wordLength = value.WordLength;
        Expand(wordLength);
        var span = buffer.Span[(BufferLength - wordLength)..BufferLength];
        span.Clear();
        chars.CopyTo(span);
    }
    internal void AddInt(int value)
    {
        var start = BufferLength - 1;
        Expand(1);
        buffer.Span[start] = value;
    }
    internal void AddArray(int[] value)
    {
        Expand(value.Length);
        value.AsSpan().CopyTo(buffer.Span[(BufferLength - value.Length)..BufferLength]);
    }
    internal void Add(int? value)
    {
        if (value.HasValue)
        {
            var start = BufferLength - 1;
            Expand(1);
            buffer.Span[start] = value.Value;
        }
    }

    internal void Add<T>(T? value)
    {
        if (value != null)
        {
            var start = BufferLength - 1;
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

    internal static int GetWordLength<T>(T? value)
    {
        if (value is null) return 0;

        return value switch
        {
            int _ => 1,
            string v => new LiteralString(v).WordLength,
            int[] a => a.Length,
            Enum _ => 1,
            _ => 0
        };
    }
}
