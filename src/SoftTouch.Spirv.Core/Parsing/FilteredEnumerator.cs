using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Spv.Specification;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct FilteredEnumerator
{
    int wordIndex;
    bool started;
    WordBuffer buffer;
    readonly Span<int> instructionWords => buffer.Buffer.Span;

    string? classFilter;
    SDSLOp? filter1;
    SDSLOp? filter2;
    SDSLOp? filter3;
    SDSLOp? filter4;

    FilterType filterType;


    public FilteredEnumerator(WordBuffer buff, string classFilt)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        classFilter = classFilt;
        filterType = FilterType.ClassName;
    }
    public FilteredEnumerator(WordBuffer buff, SDSLOp filt1)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filterType = FilterType.Op1;
    }
    public FilteredEnumerator(WordBuffer buff, SDSLOp filt1, SDSLOp filt2)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filter2 = filt2;
        filterType = FilterType.Op2;
    }
    public FilteredEnumerator(WordBuffer buff, SDSLOp filt1, SDSLOp filt2, SDSLOp filt3)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filter2 = filt2;
        filter3 = filt3;
        filterType = FilterType.Op3;
    }
    public FilteredEnumerator(WordBuffer buff, SDSLOp filt1, SDSLOp filt2, SDSLOp filt3, SDSLOp filt4)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filter2 = filt2;
        filter3 = filt3;
        filter4 = filt4;
        filterType = FilterType.Op4;
    }

    public RefInstruction Current => ParseCurrentInstruction();

    bool Matches(SDSLOp toCheck)
    {
        return filterType switch 
        {
            FilterType.ClassName => InstructionInfo.GetInfo(toCheck).ClassName == classFilter,
            FilterType.Op1 => toCheck == filter1,
            FilterType.Op2 => toCheck == filter1 || toCheck == filter2,
            FilterType.Op3 => toCheck == filter1 || toCheck == filter2 || toCheck == filter3,
            FilterType.Op4 => toCheck == filter1 || toCheck == filter2 || toCheck == filter3 || toCheck == filter4,
            _ => false
        };
    }
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
            while(!Matches((SDSLOp)(instructionWords[wordIndex + sizeToStep] & 0xFFFF)) && wordIndex + sizeToStep < instructionWords.Length)
            {
                sizeToStep += instructionWords[wordIndex + sizeToStep] >> 16;
            }
            wordIndex += sizeToStep;
            if (wordIndex >= instructionWords.Length)
                return false;
            return true;
        }

    }


    public RefInstruction ParseCurrentInstruction()
    {
        var wordNumber = instructionWords[wordIndex] >> 16;
        return RefInstruction.Parse(buffer.Buffer.Memory, wordIndex);
    }

    internal enum FilterType
    {
        ClassName,
        Op1,
        Op2,
        Op3,
        Op4
    }
}
