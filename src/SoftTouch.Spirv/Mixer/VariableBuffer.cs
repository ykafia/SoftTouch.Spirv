using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;

public class VariableBuffer : ExpandableBuffer<(string,IdRef)>
{
    public IdRef this[string name]
    {
        get
        {
            foreach ((var n, var id) in Span)
            {
                if(n == name)
                    return id;
            }
            throw new KeyNotFoundException($"{name} was not found.");
        }
        set
        {
            var idx = 0;
            foreach ((var n, var id) in Span)
            {
                if (n == name)
                {
                    Span[idx].Item2 = value;
                    return;
                }
                idx += 1;
            }
            Add((name, value));
        }
    }

    public void CopyIdsToSpan(Span<IdRef> ids)
    {
        if (ids.Length != Span.Length) throw new ArgumentException($"input length must be {Span.Length}");
        for(int i = 0; i < Span.Length; i++)
        {
            ids[i] = Span[i].Item2;
        }
    }
}
