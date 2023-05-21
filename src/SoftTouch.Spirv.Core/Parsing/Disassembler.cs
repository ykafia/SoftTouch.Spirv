using System;
using CommunityToolkit.HighPerformance.Buffers;
using System.Text;
using System.Numerics;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct Disassembler
{
    static char space = ' ';
    static string tab = "    ";
    public int Length {get; private set;}
    MemoryOwner<char> buffer;

    public Disassembler(int initialCapacity = 256)
    {
        buffer = MemoryOwner<char>.Allocate(initialCapacity, AllocationMode.Clear);
    }

    public void Expand(int size)
    {
        if(Length + size > buffer.Length)
        {
            var tmp = MemoryOwner<char>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Length+size)), AllocationMode.Clear);
            buffer.Span.CopyTo(tmp.Span);
            buffer = tmp;
        }
        else
            Length += size;
    }

    internal void Add(ReadOnlySpan<char> snippet)
    {
        Expand(snippet.Length);
        snippet.CopyTo(buffer.Span[(Length - snippet.Length)..Length]);
    }
    internal void Add(scoped Span<char> snippet)
    {
        Expand(snippet.Length);
        snippet.CopyTo(buffer.Span[(Length - snippet.Length)..Length]);
    }
    internal void Add(char c)
    {
        Expand(1);
        buffer.Span[Length - 1] = c;
    }
    internal void AddResult(IdRef snippet)
    {
        var num1 = snippet.Value;
        var num2 = snippet.Value;
        var digitNum = 1;
        while(num1 > 0)
        {
            num1 /= 10;
            digitNum += 1;
        }
        Span<char> chars = stackalloc char[digitNum + 3];
        chars[0] = '%';
        chars[^3] = ' ';
        chars[^2] = '=';
        chars[^1] = ' ';
        var index = 4;
        while(num2 > 0)
        {
            chars[chars.Length - index] = (char)((num2 % 10) + 48);
            num1 /= 10;
            index -= 1;
        }
        Add(chars);
    }
    internal void Add(IdRef snippet)
    {
        var num1 = snippet.Value;
        var num2 = snippet.Value;
        var digitNum = 1;
        while(num1 > 0)
        {
            num1 /= 10;
            digitNum += 1;
        }
        Span<char> chars = stackalloc char[digitNum + 1];
        chars[0] = '%';
        var index = 1;
        while(num2 > 0)
        {
            chars[chars.Length - index] = (char)((num2 % 10) + 48);
            num1 /= 10;
            index -= 1;
        }
        Add(chars);
    }

    public void Disassemble(SpirvHeader header)
    {
        
    }
    public void Disassemble(RefInstruction instruction)
    {
        if(instruction.ResultId != null)
            AddResult(instruction.ResultId.Value);
        Add(Enum.GetName(instruction.OpCode));
        
    }

    public override string ToString()
    {
        return new string(buffer.Span);
    }
}