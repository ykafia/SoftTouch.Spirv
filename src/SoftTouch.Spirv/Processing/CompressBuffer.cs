using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessing;

public struct CompressBuffer : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        var span = buffer.Declarations.InstructionSpan;
        var wid = 0;
        var previous = 0;
        while(wid < span.Length)
        {
            if (span[wid] >> 16 == 0)
                break;
            previous = wid;
            wid += span[wid] >> 16;
            if ((span[previous] & 0xFFFF) == 0 && (span[previous] >> 16) != 0)
            {
                span[wid..].CopyTo(span[previous..]);
                wid = previous;
            }
            
            
        }
        buffer.Declarations.RecomputeLength();
        foreach (var (_, f) in buffer.Functions)
        {
            span = f.InstructionSpan;
            wid = 0;
            previous = 0;
            while (wid < span.Length)
            {
                if (span[wid] >> 16 == 0)
                    break;
                previous = wid;
                wid += span[wid] >> 16;
                if ((span[previous] & 0xFFFF) == 0 && (span[previous] >> 16) != 0)
                {
                    span[wid..].CopyTo(span[previous..]);
                    wid = previous;
                }
            }
            f.RecomputeLength();

        }
    }
}