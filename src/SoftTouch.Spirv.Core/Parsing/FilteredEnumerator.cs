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
    readonly Span<int> instructionWords => buffer.buffer.Span;

    string? classFilter;
    Op? filter1;
    Op? filter2;
    Op? filter3;
    Op? filter4;

    FilterType filterType;


    public FilteredEnumerator(WordBuffer buff, string classFilt)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        classFilter = classFilt;
        filterType = FilterType.ClassName;
    }
    public FilteredEnumerator(WordBuffer buff, Op filt1)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filterType = FilterType.Op1;
    }
    public FilteredEnumerator(WordBuffer buff, Op filt1, Op filt2)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filter2 = filt2;
        filterType = FilterType.Op2;
    }
    public FilteredEnumerator(WordBuffer buff, Op filt1, Op filt2, Op filt3)
    {
        started = false;
        wordIndex = 0;
        buffer = buff;
        filter1 = filt1;
        filter2 = filt2;
        filter3 = filt3;
        filterType = FilterType.Op3;
    }
    public FilteredEnumerator(WordBuffer buff, Op filt1, Op filt2, Op filt3, Op filt4)
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

    bool Matches(Op toCheck)
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
            while(!Matches((Op)(instructionWords[wordIndex + sizeToStep] & 0xFFFF)) && wordIndex + sizeToStep < instructionWords.Length)
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
        return RefInstruction.Parse(buffer.buffer.Memory, wordIndex);
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
