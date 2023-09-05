using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Buffers;

public ref struct SpirvSpan
{
    Span<int> words;

    public int Length => words.Length - (HasHeader ? 5 : 0);
    public bool HasHeader => words[0] == Spv.Specification.MagicNumber;

    public Span<int> Span => HasHeader ? words[5..] : words;


    public int this[int index] { get => words[index]; set => words[index] = value; }

    public SpirvSpan(Span<int> words)
    {
        this.words = words;
    }
}
