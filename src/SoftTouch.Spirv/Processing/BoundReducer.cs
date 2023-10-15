using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;



/// <summary>
/// Makes sure indices used in spirv module are all continuous.
/// </summary>
public struct BoundReducer : INanoPass
{
    public BoundReducer() { }

    public void Apply(MultiBuffer buffer)
    {
        throw new NotImplementedException();

        var next = Instruction.Empty;
        var previous = Instruction.Empty;

        foreach(var i in buffer.Declarations.UnorderedInstructions)
        {
            if (i.ResultId == 1)
            {
                previous = i;
                break;
            }
            if (previous.IsEmpty)
                previous = i;
            else if (previous.ResultId != null && i.ResultId < previous.ResultId)
                previous = i;
        }
        
        var shouldContinue = true;
        while(shouldContinue)
        {
            foreach (var i in buffer.Instructions)
            {
                if (next.IsEmpty && i.Words.Span != previous.Words.Span)
                    next = i;
                else if(!next.IsEmpty)
                {
                }
            }
        }
        buffer.RecomputeBound();
    }
    static void ReplaceRefs(int from, int to, MultiBuffer buffer)
    {
        foreach (var i in buffer.Declarations.UnorderedInstructions)
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
        foreach (var (_, f) in buffer.Functions)
            foreach (var i in f.UnorderedInstructions)
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
    static void ReplaceRefs(int from, int to, WordBuffer func)
    {
        foreach (var i in func.UnorderedInstructions)
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
