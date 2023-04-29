using SoftTouch.Spirv.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;

public record struct TypeEntry(SpvSnippet Owner, Instruction Instruction);

public class IdProvider
{
    public int Length { get; private set; } = 0;
    public Dictionary<int, TypeEntry> TypeMap { get; } = new();    

    public int Register(SpvSnippet snippet, Instruction instruction)
    {
        Length += 1;
        TypeMap.Add(Length, new(snippet,instruction));
        return Length;
    }
}
