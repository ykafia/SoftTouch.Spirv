using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct InstructionEnumerator
{
    int wordIndex;
    bool started;
    readonly Span<int> instructionWords;
    MemoryOwner<int>? owner;

    public InstructionEnumerator(Span<int> words, MemoryOwner<int>? data = null)
    {
        started = false;
        wordIndex = 0;
        instructionWords = words;
        owner = data;
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
        if(owner != null)
            return RefInstruction.Parse(owner, wordIndex);
        else
            return RefInstruction.ParseRef(instructionWords.Slice(wordIndex, wordNumber));
    }
}
