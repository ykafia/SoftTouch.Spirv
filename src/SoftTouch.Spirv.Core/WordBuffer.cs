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

public partial class WordBuffer
{

    // TODO : Generate overloads so that instructions are added with automatic bound
    Bound bound = new();
    internal MemoryOwner<int> buffer;
    public int BufferLength { get; private set; }
    public Span<int> Span => buffer.Span[..BufferLength];
    public int Count => new SpirvReader(buffer.Memory[..BufferLength]).Count;

    public SortedList<int, SpirvUpdate> Redirection;

    public RefInstruction this[int index]
    {
        get
        {
            var usedIndex = index;
            if(Redirection.TryGetValue(index, out var redirect) && redirect.Kind == UpdateKind.Replace)
            {
                usedIndex = redirect.DataIndex;
            }
            int id = 0;
            int wid = 0;
            while (id < usedIndex)
            {
                wid += buffer.Span[wid] >> 16;
                id++;
            }
            return RefInstruction.ParseRef(buffer.Span.Slice(wid, buffer.Span[wid] >> 16));
        }
    }

    public WordBuffer(int initialCapacity = 32)
    {
        BufferLength = 0;
        buffer = MemoryOwner<int>.Allocate(initialCapacity, AllocationMode.Clear);
        Redirection = new();
    }
    public WordBuffer(MemoryOwner<int> data)
    {
        buffer = data;
        BufferLength = Count;
        Redirection = new();
    }

    public InstructionEnumerator GetEnumerator() => new(buffer.Span);


    public void Expand(int size)
    {
        if (buffer.Length < BufferLength + size)
        {
            var tmp = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(BufferLength + size)), AllocationMode.Clear);
            buffer.Span.CopyTo(tmp.Span);
            buffer = tmp;
            BufferLength += size;
        }
        else
            BufferLength += size;
    }
    internal void Insert(int start, Span<int> words)
    {
        Expand(words.Length);
        var slice = buffer.Span[start..(BufferLength-words.Length)];
        slice.CopyTo(buffer.Span[(start + words.Length)..]);
        words.CopyTo(buffer.Span.Slice(start, words.Length));
    }

    internal Instruction Add(MutRefInstruction instruction)
    {
        int group = 0;
        if (instruction.OpCode == Op.OpVariable)
            group = InstructionInfo.GetGroupOrder(instruction.OpCode, (StorageClass)instruction.Words[3]);
        else
            group = InstructionInfo.GetGroupOrder(instruction.OpCode);

        if (group == 13)
        {
            Insert(BufferLength, instruction.Words);
            return new(this, Count - 1);
        }
        else
        {

            var index = 0;
            var wIndex = 0;
            while (InstructionInfo.GetGroupOrder((Op)(buffer.Span[wIndex] & 0xFF), ((Op)(buffer.Span[wIndex] & 0xFF)) == Op.OpVariable ? (StorageClass)buffer.Span[wIndex+3] : null) < group)
            {
                wIndex += buffer.Span[wIndex] >> 16;
                index += 1;
                if(wIndex >= BufferLength)
                {
                    Insert(BufferLength, instruction.Words);
                    return new(this, Count - 1);
                }

            }
            Insert(wIndex, instruction.Words);
            return new(this, index);
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
            IdRef _ => 1,
            IdResultType _ => 1,
            string v => new LiteralString(v).WordLength,
            LiteralString v => v.WordLength,
            int[] a => a.Length,
            Enum _ => 1,
            _ => throw new NotImplementedException()
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
