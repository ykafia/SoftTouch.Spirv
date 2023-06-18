namespace SoftTouch.Spirv.Core.Parsing;


public ref struct OperandEnumerator
{
    RefInstruction instruction;
    readonly LogicalOperandArray logicalOperands;
    int wid;
    int oid;
    public OperandEnumerator(RefInstruction instruction)
    {
        this.instruction = instruction;
        logicalOperands = InstructionInfo.GetInfo(instruction.OpCode);
        oid = -1;
        wid = 1;
    }

    public SpvLiteral Current => ParseCurrent();

    public bool MoveNext()
    {
        if (oid < 0)
        {
            oid = 0;
            return true;
        }
        else
        {


            if (oid + 1 >= logicalOperands.Count && (wid >= instruction.Words.Length -1))
                return false;

            var logOp = logicalOperands[oid];
            if (logOp.Kind == OperandKind.LiteralString && logOp.Quantifier == OperandQuantifier.One)
            {
                while (!instruction.Words[wid].HasEndString())
                    wid += 1;
                wid += 1;
            }
            else if ( 
                    logOp.Kind switch 
                    {
                        OperandKind.PairIdRefIdRef 
                        or OperandKind.PairLiteralIntegerIdRef 
                        or OperandKind.PairIdRefLiteralInteger => true,
                        _ => false
                    }
                    
                    && logOp.Quantifier == OperandQuantifier.One
                )
            {
                wid += 2;
            }
            else if ( 
                    logOp.Kind switch 
                    {
                        OperandKind.PairIdRefIdRef 
                        or OperandKind.PairLiteralIntegerIdRef 
                        or OperandKind.PairIdRefLiteralInteger => true,
                        _ => false
                    }
                    
                    && logOp.Quantifier == OperandQuantifier.ZeroOrOne
                    && wid < instruction.Words.Length - 2
                )
            {
                wid += 2;
                return true;
            }
            else if ( 
                    logOp.Kind switch 
                    {
                        OperandKind.PairIdRefIdRef 
                        or OperandKind.PairLiteralIntegerIdRef 
                        or OperandKind.PairIdRefLiteralInteger => true,
                        _ => false
                    }
                    
                    && logOp.Quantifier == OperandQuantifier.ZeroOrOne
                    && wid == instruction.Words.Length - 2
                )
            {
                return false;
            }
            else if ( 
                    logOp.Kind switch 
                    {
                        OperandKind.PairIdRefIdRef 
                        or OperandKind.PairLiteralIntegerIdRef 
                        or OperandKind.PairIdRefLiteralInteger => true,
                        _ => false
                    }
                    
                    && logOp.Quantifier == OperandQuantifier.ZeroOrMore
                    && wid < instruction.Words.Length - 2
                )
            {
                wid += 2;
                return true;
            }
            else if ( 
                    logOp.Kind switch 
                    {
                        OperandKind.PairIdRefIdRef 
                        or OperandKind.PairLiteralIntegerIdRef 
                        or OperandKind.PairIdRefLiteralInteger => true,
                        _ => false
                    }
                    
                    && logOp.Quantifier == OperandQuantifier.ZeroOrMore
                    && wid == instruction.Words.Length - 2
                )
            {
                return false;
            }
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrOne && wid < instruction.Words.Length - 1)
                wid += 1;
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrOne && wid == instruction.Words.Length - 1)
                return false;
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrMore && wid < instruction.Words.Length - 1)
            {
                wid += 1;
                return true;
            }
            else if (logOp.Quantifier == OperandQuantifier.ZeroOrMore && wid == instruction.Words.Length - 1)
                return false;
            oid += 1;
            return wid < instruction.Words.Length;
        }

    }

    public SpvLiteral ParseCurrent()
    {
        var logOp = logicalOperands[oid];
        if (logOp.Kind == OperandKind.LiteralString)
        {
            var twid = wid;
            while (!instruction.Words[twid].HasEndString())
                twid += 1;
            twid += 1;
            var result = new SpvLiteral(OperandKind.LiteralString, instruction.Words[wid..twid]);

            return result;
        }
        else return new(logOp.Kind.Value, instruction.Words[wid..(wid + 1)]);
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