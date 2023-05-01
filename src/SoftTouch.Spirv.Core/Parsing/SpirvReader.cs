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
    public static List<OwnedInstruction> ParseToList(byte[] byteCode)
    {
        var data = MemoryOwner<int>.Allocate(byteCode.Length / 4);
        var span = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        span.CopyTo(data.Span);
        var reader = new SpirvReader(data);
        var list = new List<OwnedInstruction>();
        foreach(var instruction in reader)
        {
            instruction.ToOwned(out var owned);
            if(owned is not null)
                list.Add(owned.Value);
        }
        return list;
    }
    public static List<OwnedInstruction> ParseToList(MemoryOwner<int> data)
    {
        var reader = new SpirvReader(data);
        var list = new List<OwnedInstruction>();
        foreach (var instruction in reader)
        {
            instruction.ToOwned(out var owned);
            if (owned is not null)
                list.Add(owned.Value);
        }
        return list;
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
    public SpirvReader(MemoryOwner<int> owned)
    {
        var wordLength = owned.Length;
        words = owned.Span;
        data = owned;
        var header = SpirvHeader.Read(words[0..5]);
    }


    public InstructionEnumerator GetEnumerator() => new(words,data);

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
