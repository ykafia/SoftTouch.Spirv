using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Spv.Specification;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct InstructionEnumerator
{
    int wordIndex;
    bool started;
    readonly Span<int> instructionWords;
    Memory<int>? memorySlice;

    public int BoundOffset { get; set; }

    public InstructionEnumerator(Span<int> words, Memory<int>? slice = null)
    {
        started = false;
        wordIndex = 0;
        instructionWords = words;
        memorySlice = slice;
        BoundOffset = 0;
    }

    public RefInstruction Current => ParseCurrentInstruction();

    public bool MoveNext()
    {
        if (!started)
        {
            started = true;
            return true;
        }
        else
        {
            var sizeToStep = instructionWords[wordIndex] >> 16;
            wordIndex += sizeToStep;
            if (wordIndex >= instructionWords.Length)
                return false;
            return true;
        }

    }


    public RefInstruction ParseCurrentInstruction()
    {
        var wordNumber = instructionWords[wordIndex] >> 16;
        if (memorySlice is not null)
        {
            return RefInstruction.Parse(memorySlice.Value, wordIndex) with { IdRefOffset = BoundOffset };
        }
        else
            return RefInstruction.ParseRef(instructionWords.Slice(wordIndex, wordNumber)) with { IdRefOffset = BoundOffset };
    }
}
