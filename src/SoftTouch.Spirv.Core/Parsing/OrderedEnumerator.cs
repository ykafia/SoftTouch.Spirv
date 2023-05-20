using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Spv.Specification;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct OrderedEnumerator
{
    int index;
    int wordIndex;
    bool started;

    WordBuffer wbuff;

    readonly Span<int> instructionWords => wbuff.Span;
    Memory<int> memorySlice => wbuff.buffer.Memory;

    public OrderedEnumerator(WordBuffer buffer)
    {
        started = false;
        wordIndex = 0;
        index = 0;
        wbuff = buffer;
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
            var previousGroup = GetGroupOrder(wordIndex);
            var groupOffset = 0;
            var sizeToStep = instructionWords[wordIndex] >> 16;
            var offset = 1;
            if (wordIndex + sizeToStep >= instructionWords.Length)
                return false;

            while (GetGroupOrder(wordIndex + sizeToStep) != previousGroup + groupOffset)
            {
                sizeToStep += instructionWords[wordIndex + sizeToStep] >> 16;
                offset += 1;
                if(wordIndex + sizeToStep >= instructionWords.Length)
                {
                    groupOffset += 1;
                    offset = 1;
                    sizeToStep = instructionWords[wordIndex] >> 16;
                }
            }
            wordIndex += sizeToStep;
            index += offset;

            if (wordIndex >= instructionWords.Length)
                return false;
            return true;
        }

    }

    int GetGroupOrder(int wid)
    {
        var op = (Op)(instructionWords[wid] & 0xFF);
        return InstructionInfo.GetGroupOrder(op, op == Op.OpVariable ? (StorageClass)instructionWords[wid + 3] : null);
    }


    public Instruction ParseCurrentInstruction()
    {
        return new(wbuff, index);
    }
}