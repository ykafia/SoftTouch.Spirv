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

    public InstructionEnumerator(Span<int> words)
    {
        started = false;
        wordIndex = 0;
        instructionWords = words;
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
        return RefInstruction.Parse(instructionWords.Slice(wordIndex, wordNumber));
    }
}
