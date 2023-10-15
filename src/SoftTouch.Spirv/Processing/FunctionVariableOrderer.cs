using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core;
namespace SoftTouch.Spirv.Processing;

/// <summary>
/// Makes sure variables are created in the beginning of a function definition
/// </summary>
public struct FunctionVariableOrderer : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        foreach(var (_,f) in buffer.Functions)
        {
            ProcessFunction(new(f.InstructionSpan));
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
