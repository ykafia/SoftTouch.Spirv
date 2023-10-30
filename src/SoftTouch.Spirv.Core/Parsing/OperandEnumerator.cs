namespace SoftTouch.Spirv.Core.Parsing;

/// <summary>
/// An instruction operands enumerator, useful for parsing instructions
/// </summary>
public ref struct OperandEnumerator
{
    static OperandKind[] pairs { get; } = Enum.GetValues<OperandKind>().Where(x => x.ToString().StartsWith("Pair")).ToArray();
    RefInstruction instruction;
    Span<int> operands => instruction.Operands;
    readonly LogicalOperandArray logicalOperands;
    int wid;
    int oid;

    public OperandEnumerator(RefInstruction instruction)
    {
        this.instruction = instruction;
        logicalOperands = InstructionInfo.GetInfo(instruction.OpCode);
        oid = -1;
        wid = 0;
    }

    public SpvOperand Current => ParseCurrent();

    public bool MoveNext()
    {
        if (oid < 0)
        {
            oid = 0;
            if (logicalOperands[0].Kind == OperandKind.None)
                return false;
            return true;
        }
        else
        {
            
            var logOp = logicalOperands[oid];

            if (logOp.Quantifier == OperandQuantifier.One)
            {
                if (logOp.Kind == OperandKind.LiteralString)
                {
                    while (!operands[wid].HasEndString())
                        wid += 1;
                    wid += 1;
                }
                else if (pairs.Contains(logOp.Kind ?? throw new Exception("kind is inexistent")))
                    wid += 2;
                else
                    wid += 1;
                oid += 1;

            }
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrOne)
            {
                if (
                    pairs.Contains(logOp.Kind ?? throw new Exception("kind is inexistent"))
                    && wid < operands.Length - 1
                )
                {
                    wid += 2;
                }
                else if (
                    logOp.Kind == OperandKind.LiteralString
                    && wid < operands.Length
                )
                {
                    while (!operands[wid].HasEndString())
                        wid += 1;
                    wid += 1;
                }
                else if (wid < operands.Length)
                    wid += 1;
                oid += 1;

            }
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrMore)
            {
                if (logOp.Kind == OperandKind.LiteralString)
                    throw new NotImplementedException("params of strings is not yet implemented");
                else if (
                    pairs.Contains(logOp.Kind ?? throw new Exception())
                    && wid < operands.Length - 2
                )
                    wid += 2;
                else if (wid < operands.Length - 1)
                    wid += 1;
                else
                    oid += 1;
                
            }
            if (oid >= logicalOperands.Count)
                return false;
            return wid < operands.Length;
        }

    }

    public SpvOperand ParseCurrent()
    {
        var logOp = logicalOperands[oid];
        if (logOp.Quantifier != OperandQuantifier.ZeroOrMore)
        {
            if (logOp.Kind == OperandKind.LiteralString)
            {
                var length = 0;
                while (!operands[wid + length].HasEndString())
                    length += 1;
                length += 1;
                var result = new SpvOperand(OperandKind.LiteralString, logOp.Quantifier ?? OperandQuantifier.One, operands.Slice(wid, length));

                return result;
            }
            else if (pairs.Contains(logOp.Kind ?? throw new NotImplementedException("")))
            {
                var result = new SpvOperand(logOp.Kind ?? OperandKind.None, logOp.Quantifier ?? OperandQuantifier.One, operands.Slice(wid, 2));
                return result;
            }
            else
                return new(logOp.Kind ?? OperandKind.None, logOp.Quantifier ?? OperandQuantifier.One, operands.Slice(wid, 1));
        }
        else
            return new(logOp.Kind ?? OperandKind.None, logOp.Quantifier ?? OperandQuantifier.One, operands[wid..]);
        throw new NotImplementedException();
    }

}

public static class IntExtensions
{
    public static bool HasEndString(this int i)
    {
        return
            (char)(i >> 24) == '\0'
            || (char)(i >> 16 & 0XFF) == '\0'
            || (char)(i >> 8 & 0xFF) == '\0'
            || (char)(i & 0xFF) == '\0';
    }
}