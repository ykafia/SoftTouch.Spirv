using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace SoftTouch.Spirv.Core;

public struct ReplaceBuffer
{
    MemoryOwner<int> buffer;
    public int Length { get; private set; }

    public RefInstruction this[int index]
    {
        get
        {
            int id = 0;
            int wid = 0;
            while (id < index)
            {
                wid += buffer.Span[wid] >> 16;
                id++;
            }
            return RefInstruction.ParseRef(buffer.Span.Slice(wid, buffer.Span[wid] >> 16));
        }
    }

    public ReplaceBuffer()
    {
        buffer = MemoryOwner<int>.Allocate(32);
        Length = 0;
    }

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
        words.CopyTo(buffer.Span.Slice(Length - words.Length, words.Length));
    }
}

public partial class WordBuffer
{

    // TODO : Generate overloads so that instructions are added with automatic bound
    Bound bound = new(0);


    ReplaceBuffer replacedInstructions;

    MemoryOwner<int> buffer;
    public int BufferLength { get; private set; }
    public List<SpirvUpdate> Updates { get; private set; }

    public Span<int> Span => buffer.Span[..BufferLength];
    public int Count => new SpirvReader(buffer.Memory[..BufferLength]).Count;

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
        var wordLength = value.WordLength;
        Span<byte> bytes = stackalloc byte[wordLength * 4];
        Encoding.UTF8.GetBytes(value.Value.AsSpan(), bytes);
        var words = MemoryMarshal.Cast<byte,int>(bytes);
        Expand(wordLength);
        var span = buffer.Span[(BufferLength - wordLength)..BufferLength];
        span.Clear();
        words.CopyTo(span);
    }
    internal void AddInt(int value)
    {
        var start = BufferLength;
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
            AddInt(value.Value);
        }
    }

    internal void Add(LiteralInteger value)
    {
        var start = BufferLength;
        if(value.WordCount == 2)
        {
            Add((int)(value.Words >> 16));
        }
        Add((int)(value.Words & 0xFFFFFFFF));
    }
    internal void Add(LiteralFloat value)
    {
        var start = BufferLength;
        if (value.WordCount == 2)
        {
            Add((int)(value.Words >> 16));
        }
        Add((int)(value.Words & 0xFFFFFFFF));
    }

    internal void Add(Span<IdRef> values)
    {
        Expand(values.Length);
        foreach (var i in values) Add(i.Value);
    }
    internal void Add(Span<LiteralInteger> values)
    {
        Expand(values.Length);
        foreach (var i in values)
        {
            if (i.Size > 32)
                Add((int)i.Words >> 32);
            Add(i.Words & 0xFFFFFFFF);
        }
    }
    internal void Add(Span<PairIdRefIdRef> values)
    {
        Expand(values.Length);
        foreach (var i in values)
        {
            Add(i.Value.Item1);
            Add(i.Value.Item2);
        }
    }
    internal void Add(Span<PairIdRefLiteralInteger> values)
    {
        Expand(values.Length);
        foreach (var i in values)
        {
            Add(i.Value.Item1);
            Add(i.Value.Item2);
        }
    }
    internal void Add(Span<PairLiteralIntegerIdRef> values)
    {
        Expand(values.Length);
        foreach (var i in values)
        {
            Add(i.Value.Item1);
            Add(i.Value.Item2);
        }
    }

    internal void Add<T>(T? value)
    {
        if (value != null)
        {
            if (value is int i)
                AddInt(i);
            else if (value is int[] array)
                AddArray(array);
            else if (value is string s)
                AddString(s);
            else if (value is LiteralString ls)
                AddString(ls.Value);
            else if (value is Enum e)
                Add(Convert.ToInt32(e));
        }
    }

    internal static int GetWordLength<T>(T? value)
    {
        if (value is null) return 0;

        return value switch
        {
            LiteralInteger i => i.WordCount,
            LiteralFloat i => i.WordCount,
            int _ => 1,
            string v => new LiteralString(v).WordLength,
            LiteralString v => v.WordLength,
            int[] a => a.Length,
            Enum _ => 1,
            _ => 0
        };
    }
    internal static int GetWordLength(Span<int> values) => values.Length;
    internal static int GetWordLength(Span<LiteralInteger> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<LiteralFloat> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<IdRef> values) => values.Length;
    internal static int GetWordLength(Span<PairIdRefIdRef> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairIdRefLiteralInteger> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairLiteralIntegerIdRef> values) => values.Length * 2;

}
