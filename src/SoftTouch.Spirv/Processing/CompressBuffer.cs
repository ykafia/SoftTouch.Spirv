using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessing;

public struct CompressBuffer : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        var span = buffer.Declarations.InstructionSpan;
        var wid = 0;
        while(wid < span.Length)
        {
            if (span[wid] >> 16 == 0)
                break;
            var offset = 0;
            var i = RefInstruction.ParseRef(span.Slice(wid,span[wid] >> 16));
            if(i.OpCode == SDSLOp.OpNop)
            {
                var tmp = i;
                while (tmp.OpCode == SDSLOp.OpNop)
                {
                    offset =+ tmp.WordCount;
                    if(span[wid+offset] >> 16 == 0)
                        break;
                    tmp = RefInstruction.ParseRef(span.Slice(wid + offset, span[wid + offset] >> 16));
                }
                span[(wid + offset)..].CopyTo(span[wid..]);
            }
            wid += span[wid] >> 16;

        }
        buffer.Declarations.RecomputeLength();
        foreach (var (_, f) in buffer.Functions)
        {
            span = f.InstructionSpan;
            wid = 0;
            while (wid < span.Length)
            {
                if (span[wid] >> 16 == 0)
                    break;
                var offset = 0;
                var i = RefInstruction.ParseRef(span.Slice(wid, span[wid] >> 16));
                if (i.OpCode == SDSLOp.OpNop)
                {
                    var tmp = i;
                    while (tmp.OpCode == SDSLOp.OpNop)
                    {
                        offset = +tmp.WordCount;
                        if (span[wid + offset] >> 16 == 0)
                            break;
                        tmp = RefInstruction.ParseRef(span.Slice(wid + offset, span[wid + offset] >> 16));
                    }
                    span[(wid + offset)..].CopyTo(span[wid..]);
                }
                wid += span[wid] >> 16;
            }
            f.RecomputeLength();

        }
    }
}