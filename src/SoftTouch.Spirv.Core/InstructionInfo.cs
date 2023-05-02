using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;



public partial class InstructionInfo
{
    internal static InstructionInfo Instance { get; } = new();
    Dictionary<Op, LogicalOperandArray> Info = new();
    InstructionInfo(){}

    internal void Register(Op op, OperandKind? kind, OperandQuantifier? quantifier, string? name = null)
    {
        if(Info.TryGetValue(op, out var list))
        {
            list.Add(new(kind, quantifier, name));
        }
        else
        {
            Info.Add(op, new() { new(kind, quantifier, name)});
        }
    }

    public static LogicalOperandArray GetInfo(Op op)
    {
        return Instance.Info[op];
    }
}
