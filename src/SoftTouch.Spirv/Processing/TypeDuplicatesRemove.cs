using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;




/// <summary>
/// Remove duplicate simple types.
/// Should be applied before the IdRefOffsetter.
/// </summary>
public struct TypeDuplicateRemover : IPostProcessorPass
{

    public readonly void Apply(SpirvBuffer buffer)
    {
        var idx1 = 0;

        // First base types
        foreach (var i in buffer.Instructions)
        {
            if (i.OpCode == SDSLOp.OpTypeInt || i.OpCode == SDSLOp.OpTypeFloat)
            {
                var idx2 = 0;
                foreach (var j in buffer.Instructions)
                {
                    if (j.OpCode == i.OpCode && idx1 != idx2 && i.Operands[1..].SequenceEqual(j.Operands[1..]))
                    {
                        ReplaceRefs(j.ResultId ?? -1, i.ResultId ?? -1, buffer);
                        SetOpNop(j.Words);
                    }
                    idx2 += 1;
                }
            }
            else if (i.OpCode == SDSLOp.OpTypeVoid || i.OpCode == SDSLOp.OpTypeBool)
            {
                var idx2 = 0;
                foreach (var j in buffer.Instructions)
                {
                    if (j.OpCode == i.OpCode && idx1 != idx2)
                    {
                        ReplaceRefs(j.ResultId ?? -1, i.ResultId ?? -1, buffer);
                        SetOpNop(j.Words);
                    }
                    idx2 += 1;
                }
            }
            idx1 += 1;
        }
        idx1 = 0;
        // Then vectors
        foreach (var i in buffer.Instructions)
        {
            if (i.OpCode == SDSLOp.OpTypeVector)
            {
                var idx2 = 0;
                foreach (var j in buffer.Instructions)
                {
                    if (j.OpCode == i.OpCode && idx1 != idx2 && i.Operands[1..].SequenceEqual(j.Operands[1..]))
                    {
                        ReplaceRefs(j.ResultId ?? -1, i.ResultId ?? -1, buffer);
                        SetOpNop(j.Words);
                    }
                    idx2 += 1;
                }
            }
            idx1 += 1;
        }
        idx1 = 0;

        // Then matrices
        foreach (var i in buffer.Instructions)
        {
            if (i.OpCode == SDSLOp.OpTypeMatrix)
            {
                var idx2 = 0;
                foreach (var j in buffer.Instructions)
                {
                    if (j.OpCode == i.OpCode && idx1 != idx2 && i.Operands[1..].SequenceEqual(j.Operands[1..]))
                    {
                        ReplaceRefs(j.ResultId ?? -1, i.ResultId ?? -1, buffer);
                        SetOpNop(j.Words);
                    }
                    idx2 += 1;
                }
            }
            idx1 += 1;
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

    static void SetOpNop(Span<int> words)
    {
        words[0] = words.Length << 16;
        words[1..].Clear();
    }
}
