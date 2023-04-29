using SoftTouch.Spirv.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;



public readonly struct SnippetId
{
    public int Id { get; }
    SpvSnippet Snippet { get; }

    public static implicit operator int(SnippetId id) => id.Id;
    public static implicit operator Instruction(SnippetId id) => id.Snippet.TypeMap[id.Id];

    public SnippetId(int id, [NotNull] SpvSnippet snippet)
    {
        Id = id;
        Snippet = snippet;
    }
}
