﻿using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;


public record struct OrderedSpvBuffer(SpirvBuffer Buffer)
{
    public readonly OrderedEnumerator GetEnumerator() => new(Buffer);
}

/// <summary>
/// Merges mixins into one final spirv file
/// </summary>
public struct MixinMerger : IPostProcessorPass
{
    public readonly void Apply(SpirvBuffer buffer)
    {
        var temp = new SpirvBuffer();
        var ordered = new OrderedSpvBuffer(buffer);
        foreach (var e in ordered)
            if(e.OpCode != SDSLOp.OpNop)
                temp.Add(e.Words.Span);
        
        buffer.Replace(temp, out var dispose);
        if(dispose)
            temp.Dispose();
        buffer.RecomputeBound();
    }
}
