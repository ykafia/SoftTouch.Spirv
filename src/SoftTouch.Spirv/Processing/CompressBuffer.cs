using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessing;

public struct CompressBuffer : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        using var tmp = new WordBuffer();
        foreach (var e in buffer.Declarations.UnorderedInstructions)
            if (e.OpCode != SDSLOp.OpNop)
                tmp.Insert(e);
        buffer.Declarations.InstructionSpan.Clear();
        tmp.InstructionSpan.CopyTo(buffer.Declarations.InstructionSpan);
        buffer.Declarations.RecomputeLength();
        foreach (var (_, f) in buffer.Functions)
        {
            tmp.InstructionSpan.Clear();
            tmp.RecomputeLength();
            foreach (var e in f.UnorderedInstructions)
                if (e.OpCode != SDSLOp.OpNop)
                    tmp.Insert(e);
            f.InstructionSpan.Clear();
            tmp.InstructionSpan.CopyTo(f.InstructionSpan);
            f.RecomputeLength();
        }
    }
}