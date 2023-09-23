using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;




public struct VariableFunctionOrder : IPostProcessorPass
{
    public VariableFunctionOrder() { }

    public void Apply(SpirvBuffer buffer)
    {
        int index = 0;
        int wid = 0;
        var instructions = buffer.InstructionSpan;
        while (wid < buffer.Length)
        {
            var i = RefInstruction.ParseRef(instructions[wid..(instructions[wid] >> 16)]);
            if(i.OpCode == SDSLOp.OpFunction)
            {
                var offset = i.WordCount;
            }
        }
    }
}
