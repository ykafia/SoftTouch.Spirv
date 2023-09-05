﻿using CommunityToolkit.HighPerformance.Buffers;
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
    readonly Span<int> instructionWords => wbuff.InstructionSpan;
    Memory<int> memorySlice => wbuff.InstructionMemory;

    public OrderedEnumerator(ISpirvBuffer buffer)
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
            (var firstGroup, var firstPos) = (int.MaxValue, int.MaxValue);
            var wid = 0;
            var idx = 0;
            while(wid < instructionWords.Length)
            {
                var group = GetGroupOrder(wid);
                if(group < firstGroup)
                {
                    firstGroup = group;
                    firstPos = wid;
                    index = idx;
                }
                idx += 1;
                wid += instructionWords[wid] >> 16;
            }
            wordIndex = firstPos;
            started = true;
            return true;
        }
        else
        {
            var currentGroup = GetGroupOrder(wordIndex);
            for (int group = currentGroup; group < 14; group += 1)
            {
                if(group == currentGroup)
                {
                    var offset = instructionWords[wordIndex] >> 16;
                    var idx = index;
                    while(wordIndex + offset <  instructionWords.Length)
                    {
                        if(GetGroupOrder(wordIndex + offset) == group)
                        {
                            wordIndex += offset;
                            index = idx;
                            return true;
                        }
                        offset += instructionWords[offset] >> 16;
                        idx += 1;
                    }
                }
                else
                {
                    var wid = 0;
                    var idx = 0;
                    while (wid < instructionWords.Length)
                    {
                        var g = GetGroupOrder(wid);
                        if (g == group)
                        {
                            wordIndex = wid;
                            index = idx;
                            return true;
                        }
                        idx += 1;
                        wid += instructionWords[wid] >> 16;
                    }
                }
            }
            return false;

            //var count = new SpirvReader(memorySlice).Count;
            //var currentGroup = GetGroupOrder(wordIndex);
            //for (int groupOffset = 0; groupOffset < 14; groupOffset++)
            //{
            //    var wid = 0;
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (wid >= instructionWords.Length)
            //            break;
            //        var g = GetGroupOrder(wid);
            //        if (GetGroupOrder(wid) == currentGroup + groupOffset && i != index)
            //        {
            //            if (!(groupOffset == 0 && i < index))
            //            {
            //                index = i;
            //                wordIndex = wid;
            //                return true;
            //            }
            //        }
            //        wid += instructionWords[wid] >> 16;
            //    }
            //}
            //return false;
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