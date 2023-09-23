using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Buffers;
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
    int index;
    bool started;
    ISpirvBuffer buffer;

    public int ResultIdReplacement { get; set; }

    public InstructionEnumerator(ISpirvBuffer buffer)
    {
        started = false;
        wordIndex = 0;
        this.buffer = buffer;
        ResultIdReplacement = 0;
    }

    public Instruction Current => ParseCurrentInstruction();

    public bool MoveNext()
    {
        if (!started)
        {
            started = true;
            return true;
        }
        else
        {
            var sizeToStep = buffer.InstructionSpan[wordIndex] >> 16;
            wordIndex += sizeToStep;
            index += 1;
            if (wordIndex >= buffer.InstructionSpan.Length)
                return false;
            return true;
        }

    }


    public Instruction ParseCurrentInstruction()
    {
        var count = buffer.InstructionSpan[wordIndex] >> 16;
        return new Instruction(buffer, buffer.InstructionMemory[wordIndex..(wordIndex + count)], index, wordIndex);

    }
}
