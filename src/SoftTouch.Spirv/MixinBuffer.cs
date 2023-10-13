using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;

public sealed class MixinMultiBuffer
{
    public string Name { get; }
    public SortedWordBuffer Declarations { get; }
    public SortedFunctionBufferCollection Functions {get;}
    public MixinGraph Parents { get; }

    public MixinMultiBuffer(string name, MultiBuffer buffers)
    {
        Name = name;
        Declarations = new(buffers.Declarations);
        Functions = new(buffers.Functions);
        Parents = new();

        foreach(var i in Declarations)
        {
            if (i.OpCode == Core.SDSLOp.OpSDSLMixinInherit)
                Parents.Add(i.GetOperand<LiteralString>("mixinName").Value);
        }
    }
}
