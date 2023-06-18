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
    Bound bound = new();
    internal MemoryOwner<int> buffer;
    public int BufferLength { get; protected set; }
    public Span<int> Span => buffer.Span[..BufferLength];
    public int Count => new SpirvReader(buffer.Memory[..BufferLength]).Count;

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

    public WordBuffer(int initialCapacity = 32)
    {
        BufferLength = 0;
        buffer = MemoryOwner<int>.Allocate(initialCapacity, AllocationMode.Clear);
    }
    public WordBuffer(MemoryOwner<int> data)
    {
        buffer = data;
        BufferLength = Count;
    }

    public OrderedEnumerator GetEnumerator() => new(this);


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
        Insert(BufferLength, instruction.Words);
        return new(this, Count - 1);
        //int group = 0;
        //if (instruction.OpCode == Op.OpVariable)
        //    group = InstructionInfo.GetGroupOrder(instruction.OpCode, (StorageClass)instruction.Words[3]);
        //else
        //    group = InstructionInfo.GetGroupOrder(instruction.OpCode);

        //if (group == 13)
        //{
        //    Insert(BufferLength, instruction.Words);
        //    return new(this, Count - 1);
        //}
        //else
        //{

        //    var index = 0;
        //    var wIndex = 0;
        //    while (InstructionInfo.GetGroupOrder((Op)(buffer.Span[wIndex] & 0xFF), ((Op)(buffer.Span[wIndex] & 0xFF)) == Op.OpVariable ? (StorageClass)buffer.Span[wIndex+3] : null) <= group)
        //    {
        //        wIndex += buffer.Span[wIndex] >> 16;
        //        index += 1;
        //        if(wIndex >= BufferLength)
        //        {
        //            Insert(BufferLength, instruction.Words);
        //            return new(this, Count - 1);
        //        }

        //    }
        //    Insert(wIndex, instruction.Words);
        //    return new(this, index);
        //}
    }


    public byte[] GenerateSpirv()
    {
        var output = new byte[BufferLength*4 + 5*4];
        var span = output.AsSpan();
        var ints = MemoryMarshal.Cast<byte,int>(span);
        var instructionWords = ints[5..];

        var header = new SpirvHeader(new SpirvVersion(1,3), 0, bound.End + 1);
        header.WriteTo(ints[0..5]);
        var id = 0;
        var enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            var curr = enumerator.Current;
            curr.Words.CopyTo(instructionWords.Slice(id, curr.Words.Length));
            id += curr.Words.Length;
        }

        //Span<byte> tmp = stackalloc byte[4];
        //for(int i = 0; i < ints.Length; i++ )
        //{
        //    var tmpSlice = span.Slice(i * 4, 4);
        //    tmpSlice.CopyTo(tmp);
        //    tmp.Reverse();
        //    tmp.CopyTo(tmpSlice);
        //}
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
    internal static int GetWordLength(Span<int> values) => values.Length;
    internal static int GetWordLength(Span<LiteralInteger> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<LiteralFloat> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<IdRef> values) => values.Length;
    internal static int GetWordLength(Span<PairIdRefIdRef> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairIdRefLiteralInteger> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairLiteralIntegerIdRef> values) => values.Length * 2;

}
