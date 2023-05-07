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

public partial class WordBuffer
{
    MemoryOwner<int> replaceBuffer;
    private int replaceLength;


    MemoryOwner<int> buffer;
    public int BufferLength { get; private set; }
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
        replaceBuffer = MemoryOwner<int>.Allocate(32);
    }
    public WordBuffer(MemoryOwner<int> data)
    {
        buffer = data;
        BufferLength = Count;
        replaceBuffer = MemoryOwner<int>.Allocate(32);
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

    private void ExpandReplace(int size)
    {
        if (replaceBuffer.Length < replaceLength + size)
        {
            var tmp = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(replaceLength + size)));
            replaceBuffer.Span.CopyTo(tmp.Span);
            replaceBuffer = tmp;
        }
        else
            replaceLength += size;
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
        ASCIIEncoding.UTF8.GetBytes(value.Value.AsSpan(), bytes);
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
}
