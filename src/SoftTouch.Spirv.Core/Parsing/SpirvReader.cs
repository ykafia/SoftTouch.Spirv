using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct SpirvReader
{
    public static void ParseToList(byte[] byteCode, List<OwnedInstruction> instructions)
    {
        var data = MemoryOwner<int>.Allocate(byteCode.Length / 4, AllocationMode.Clear);
        var span = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        span.CopyTo(data.Span);
        var reader = new SpirvReader(data);
        foreach (var instruction in reader)
        {
            if(instruction.ToOwned(out var owned))
                instructions.Add(owned);
        }
    }
    public static List<OwnedInstruction> ParseToList(byte[] byteCode)
    {
        var data = MemoryOwner<int>.Allocate(byteCode.Length / 4, AllocationMode.Clear);
        var span = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        span.CopyTo(data.Span);
        var reader = new SpirvReader(data,true);
        var list = new List<OwnedInstruction>(reader.Count);
        foreach(var instruction in reader)
        {
            if(instruction.ToOwned(out var owned))
                list.Add(owned);
        }
        return list;
    }
    public static List<OwnedInstruction> ParseToList(MemoryOwner<int> data)
    {
        var reader = new SpirvReader(data);
        var list = new List<OwnedInstruction>(reader.Count);
        foreach (var instruction in reader)
        {
            if(instruction.ToOwned(out var owned))
                list.Add(owned);
        }
        return list;
    }
    public static void ParseToList(MemoryOwner<int> data, List<OwnedInstruction> list)
    {
        var reader = new SpirvReader(data);
        foreach (var instruction in reader)
        {
            if(instruction.ToOwned(out var owned))
                list.Add(owned);
        }
    }


    MemoryOwner<int>? data;
    Span<int> words;
    public int Count => GetInstructionCount();
    public int WordCount => words.Length;
    public bool HasHeader { get; init; }

    public SpirvReader(byte[] byteCode, bool hasHeader = false)
    {
        words = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        HasHeader = hasHeader;
    }
    public SpirvReader(MemoryOwner<int> slice, bool hasHeader = false)
    {
        words = slice.Span[(hasHeader ? 5 : 0)..];
        data = slice;
        HasHeader = hasHeader;
    }
    public SpirvReader(Memory<int> slice, bool hasHeader = false)
    {
        words = slice.Span[(hasHeader ? 5 : 0)..];
        //data = slice;
    }


    public InstructionEnumerator GetEnumerator() => new(words, data?.Memory[(HasHeader ? 5 : 0)..]);

    public int GetInstructionCount()
    {
        var count = 0;
        var index = 0;
        while(index < words.Length) 
        {
            count += 1;
            index += words[index] >> 16;
        }
        return count;
    }

    public int ComputeBound()
    {
        var result = 0;
        foreach(var e in this)
            if(e.ResultId != null && e.ResultId > result)
                result = e.ResultId.Value;
        return result;
    }
}
