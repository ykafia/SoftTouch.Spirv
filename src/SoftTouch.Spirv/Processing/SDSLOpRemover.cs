using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;



public struct SDSLOpRemover : IPostProcessorPass
{

    public void Apply(SpirvBuffer buffer)
    {
        foreach (var i in buffer.Instructions)
        {
            if (
                i.OpCode == SDSLOp.OpSDSLImportIdRef
                || i.OpCode == SDSLOp.OpSDSLMixinEnd
                || i.OpCode == SDSLOp.OpSDSLMixinInherit
                || i.OpCode == SDSLOp.OpSDSLMixinName
                || i.OpCode == SDSLOp.OpSDSLMixinOffset
                || i.OpCode == SDSLOp.OpSDSLMixinVariable
            ) SetOpNop(i.Words);
        }
    }

    static void SetOpNop(Span<int> words)
    {
        words[0] = words.Length << 16;
        words[1..].Clear();
    }
    
}
