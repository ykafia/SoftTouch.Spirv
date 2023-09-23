using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;



public struct MemoryModelDuplicatesRemover : IPostProcessorPass
{

    public void Apply(SpirvBuffer buffer)
    {
        var found = false;
        foreach (var i in buffer)
        {
            if (!found && i.OpCode == SDSLOp.OpMemoryModel)
                found = true;
            else if (found && i.OpCode == SDSLOp.OpMemoryModel)
                SetOpNop(i.Words.Span);
        }
    }

    static void SetOpNop(Span<int> words)
    {
        words[0] = words.Length << 16;
        words[1..].Clear();
    }
    
}
