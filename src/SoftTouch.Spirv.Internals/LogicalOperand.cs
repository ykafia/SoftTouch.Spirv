using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Internals;

public readonly struct LogicalOperand
{
    public string? Name { get; init; }
    public OperandKind? Kind { get; init; }
    public OperandQuantifier? Quantifier { get; init; }

    public LogicalOperand() { }

    public LogicalOperand(OperandKind? kind, OperandQuantifier? quantifier, string? name = null)
    {
        Name = name;
        Kind = kind;
        Quantifier = quantifier;
    }
    public LogicalOperand(string kind, string quantifier, string? name = null)
    {
        Name = name;
        Kind = Enum.Parse<OperandKind>(kind);
        Quantifier = Enum.Parse<OperandQuantifier>(quantifier);
    }
}
