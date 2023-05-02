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
        var data = MemoryOwner<int>.Allocate(byteCode.Length / 4);
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
        var data = MemoryOwner<int>.Allocate(byteCode.Length / 4);
        var span = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        span.CopyTo(data.Span);
        var reader = new SpirvReader(data);
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

    public SpirvReader(byte[] byteCode)
    {
        var wordLength = byteCode.Length / 4;
        words = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        var header = SpirvHeader.Read(words[0..5]);
    }
    public SpirvReader(MemoryOwner<int> slice)
    {
        var wordLength = slice.Length;
        words = slice.Span[5..];
        data = slice;
        var header = SpirvHeader.Read(words[0..5]);
    }


    public InstructionEnumerator GetEnumerator() => new(words, data?.Memory[5..]);

    public int GetInstructionCount()
    {
        var count = 0;
        var index = 5;
        while(index < words.Length) 
        {
            count += 1;
            index += words[index] >> 16;
        }
        return count;
    }
}
