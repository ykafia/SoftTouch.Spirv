using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.InteropServices;
using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct SpirvReader
{
    public static void ParseToList(byte[] byteCode, List<Instruction> instructions)
    {
        
        var span = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        var data = new WordBuffer(span);
        foreach (var instruction in data)
            instructions.Add(instruction);
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
