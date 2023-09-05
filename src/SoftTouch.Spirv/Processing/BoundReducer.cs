﻿using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;




public struct BoundReducer : IPostProcessorPass
{
    public BoundReducer() { }

    public void Apply(SpirvBuffer buffer)
    {
        int previousId = 0;
        foreach (var i in buffer.Instructions)
        {
            var id = i.ResultId;
            if (id != null && id == previousId + 1)
            {
                previousId = id.Value;
            }
            else if (id != null && id > previousId + 1)
            {
                previousId += 1;
                i.SetResultId(previousId);
                ReplaceRefs(id.Value, previousId, buffer);
            }
        }

    }
    static void ReplaceRefs(int from, int to, SpirvBuffer buffer)
    {
        foreach (var i in buffer.Instructions)
        {
            foreach (var op in i)
            {
                if (op.Kind == OperandKind.IdRef || op.Kind == OperandKind.IdResultType)
                    op.Words[0] = op.Words[0] == from ? to : op.Words[0];
                else if (op.Kind == OperandKind.PairIdRefLiteralInteger)
                    op.Words[0] = op.Words[0] == from ? to : op.Words[0];
                else if (op.Kind == OperandKind.PairLiteralIntegerIdRef)
                    op.Words[1] = op.Words[1] == from ? to : op.Words[1];
                else if (op.Kind == OperandKind.PairIdRefIdRef)
                {
                    op.Words[0] = op.Words[0] == from ? to : op.Words[0];
                    op.Words[1] = op.Words[1] == from ? to : op.Words[1];
                }
            }
        }
    }
}