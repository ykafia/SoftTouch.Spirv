using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;

public partial class WordBuffer : ISpirvBuffer
{
    public Bound Bound { get; private set; }
    public ExpandableBuffer<int> Buffer { get; private set; }
    public int BufferLength => Buffer.Count;
    public Span<int> Span => Buffer.Span[..BufferLength];
    public Memory<int> Memory => Buffer.Memory[..BufferLength];
    public int Count => new SpirvReader(Buffer.Memory[..BufferLength]).Count;


    public RefInstruction this[int index]
    {
        get
        {
            int id = 0;
            int wid = 0;
            while (id < index)
            {
                wid += Buffer.Span[wid] >> 16;
                id++;
            }
            return RefInstruction.ParseRef(Buffer.Span.Slice(wid, Buffer.Span[wid] >> 16), Bound.Offset);
        }
    }
    public WordBuffer()
    {
        Bound = new();
        Buffer = new();
    }

    public WordBuffer(int initialCapacity = 32, int offset = 0)
    {
        Bound = new() { Offset = offset };
        Buffer = new(initialCapacity);
    }

    internal WordBuffer(Span<int> words)
    {
        Buffer = new(words.Length);
        Buffer.Add(words);
        Bound = new();
    }

    public OrderedEnumerator GetEnumerator() => new(this);


    public int GetNextId()
    {
        Bound = Bound with { Count = Bound.Count + 1 };
        return Bound.Count + Bound.Offset;
    }
    public void SetBoundOffset(int offset)
    {
        Bound = Bound with { Offset = offset };
    }



    public void Expand(int size) => Buffer.Expand(size);


    public void Insert(RefInstruction instruction)
    {
        Insert(Buffer.Count, instruction.Words);
    }

    internal void Insert(int start, Span<int> words)
    {
        Buffer.Insert(start,words);
    }

    internal Instruction Add(MutRefInstruction instruction)
    {
        Buffer.Add(instruction.Words);
        return new(this, Count - 1);
    }


    public byte[] GenerateSpirv()
    {
        var output = new byte[BufferLength * 4 + 5 * 4];
        var span = output.AsSpan();
        var ints = MemoryMarshal.Cast<byte, int>(span);
        var instructionWords = ints[5..];

        var header = new SpirvHeader(new SpirvVersion(1, 3), 0, Bound.Count + 1);
        header.WriteTo(ints[0..5]);
        var id = 0;
        var enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            var curr = enumerator.Current;
            curr.Words.CopyTo(instructionWords.Slice(id, curr.Words.Length));
            id += curr.Words.Length;
        }
        return output;
    }


    internal static int GetWordLength<T>(T? value)
    {
        if (value is null) return 0;

        return value switch
        {
            LiteralInteger i => i.WordCount,
            LiteralFloat i => i.WordCount,
            int _ => 1,
            IdRef _ => 1,
            IdResultType _ => 1,
            string v => new LiteralString(v).WordLength,
            LiteralString v => v.WordLength,
            int[] a => a.Length,
            Enum _ => 1,
            _ => throw new NotImplementedException()
        };
    }

    public void Dispose() => Buffer.Dispose();
    internal static int GetWordLength(Span<int> values) => values.Length;
    internal static int GetWordLength(Span<LiteralInteger> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<LiteralFloat> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<IdRef> values) => values.Length;
    internal static int GetWordLength(Span<PairIdRefIdRef> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairIdRefLiteralInteger> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairLiteralIntegerIdRef> values) => values.Length * 2;

}
