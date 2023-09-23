using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core;
namespace SoftTouch.Spirv.Processing;

public struct FunctionVariableOrderer : IPostProcessorPass
{
    public void Apply(SpirvBuffer buffer)
    {
        var started = false;
        var foundLabel = false;
        var start = 0; var end = 0;
        foreach(var i in buffer)
        {
            if(!started && i.OpCode == SDSLOp.OpFunction)
            {
                started = true;
            }
            if(started && !foundLabel && i.OpCode == SDSLOp.OpLabel)
            {
                foundLabel = true;
                start = i.WordCount + i.WordIndex;
            }
            if(foundLabel && i.OpCode == SDSLOp.OpFunctionEnd)
            {
                end = i.WordIndex + i.WordCount;
                ProcessFunction(new(buffer.InstructionSpan[start..end]));
            }
        }
    }
    public static void ProcessFunction(SpirvSpan function)
    {
        using var tmp = new SpirvBuffer(function.Span.Length);
        foreach(var i in function)
        {
            if(i.OpCode == SDSLOp.OpVariable)
            {
                tmp.Insert(tmp.Length,i.Words);
            }
        }
        foreach (var i in function)
        {
            if (i.OpCode != SDSLOp.OpVariable)
            {
                tmp.Insert(tmp.Length, i.Words);
            }
        }
        tmp.InstructionSpan.CopyTo(function.Span);
    }
}
