using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.Spirv.Core.Buffers;

using static Spv.Specification;

namespace SoftTouch.Spirv.Core.Parsing;

public ref struct OrderedEnumerator
{
    int index;
    int wordIndex;
    bool started;
    

    ISpirvBuffer wbuff;
    readonly Span<int> instructionWords {get; init;}
    Memory<int> memorySlice {get; init;}

    public OrderedEnumerator(WordBuffer buffer)
    {
        started = false;
        wordIndex = 0;
        index = 0;
        wbuff = buffer;
        instructionWords = buffer.Span;
        memorySlice = buffer.Memory;
    }
    public OrderedEnumerator(SpirvBuffer buffer)
    {
        started = false;
        wordIndex = 0;
        index = 0;
        wbuff = buffer;
        instructionWords = buffer.InstructionSpan;
        memorySlice = buffer.InstructionMemory;
    }

    public Instruction Current => ParseCurrentInstruction();

    public bool MoveNext()
    {
        if (!started)
        {
            var wid = 0;
            var count = new SpirvReader(memorySlice).Count;
            var currentGroup = GetGroupOrder(0);
            for (int i = 0; i < count && wid < instructionWords.Length; i++)
            {
                wid += instructionWords[wid] >> 16;
                if (wid >= instructionWords.Length)
                    break;
                if (GetGroupOrder(wid) < currentGroup)
                {
                    currentGroup = GetGroupOrder(wid);
                    index = i;
                    wordIndex = wid;
                }
            }
            started = true;
            return true;
        }
        else
        {
            
            var count = new SpirvReader(memorySlice).Count;
            var currentGroup = GetGroupOrder(wordIndex);
            for (int groupOffset = 0; groupOffset < 14; groupOffset++)
            {
                var wid = 0;
                for (int i = 0; i < count; i++)
                {
                    if (wid >= instructionWords.Length)
                        break;
                    var g = GetGroupOrder(wid);
                    if (GetGroupOrder(wid) == currentGroup + groupOffset && i != index)
                    {
                        if (!(groupOffset == 0 && i < index))
                        {
                            index = i;
                            wordIndex = wid;
                            return true;
                        }
                    }
                    wid += instructionWords[wid] >> 16;
                }
            }
            return false;
        }

    }

    int GetGroupOrder(int wid)
    {
        var op = (SDSLOp)(instructionWords[wid] & 0xFFFF);
        return InstructionInfo.GetGroupOrder(op, op == SDSLOp.OpVariable ? (StorageClass)instructionWords[wid + 3] : null);
    }


    public Instruction ParseCurrentInstruction()
    {
        return new(wbuff, index);
    }
}