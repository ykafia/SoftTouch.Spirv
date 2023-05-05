using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public partial struct Instruction
{
    WordBuffer Buffer { get; init; }
    public int Index { get; init; }

    public int CountOfWords => Buffer[Index].WordCount;
    public Op OpCode => Buffer[Index].OpCode;
    public int? ResultId => Buffer[Index].ResultId;
    public int? ResultType => Buffer[Index].ResultType;
    public Span<int> Operands => Buffer[Index].Operands;

    public Instruction(WordBuffer buffer, int index)
    {
        Buffer = buffer;
        Index = index;
    }

    public bool Set<T>(string propertyName, scoped in T value)
    {
        var info = InstructionInfo.GetInfo(OpCode);
        foreach (var e in info)
        {
            var kind = e.Kind;
        }

        if (value is LiteralString s)
            return false;
        if (value is int i)
            return false;
        return false;   

    }
    
}

internal static class SpirvOperandExtensions
{
    public static T Parse<T>(this ref Span<int> v)
    {
        if (v.Length == 1 && typeof(T).IsEnum)
            return Unsafe.As<int, T>(ref v[0]);
        if (v.Length == 1 && typeof(T) == typeof(int))
            return Unsafe.As<int, T>(ref v[0]);
        if (typeof(T) == typeof(string))
        {
            var s = LiteralString.Parse(v);
            return Unsafe.As<string, T>(ref s);
        }
        else
            throw new ArgumentException("Cannot parse operand");
    }


}