using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;




public struct IdRefOffsetter : IPostProcessorPass
{
    public IdRefOffsetter() { }

    public void Apply(SpirvBuffer buffer)
    {
        int offset = 0;
        int nextOffset = 0;
        foreach (var i in buffer.Instructions)
        {
            // if we hit a mixin name we reset stuff
            if (i.OpCode == SDSLOp.OpSDSLMixinName)
            {
                offset += nextOffset;
                nextOffset = 0;
            }
            else
            {
                if (i.ResultId != null)
                    nextOffset = i.ResultId.Value;
                i.OffsetIds(offset);
            }
        }
    }
}
