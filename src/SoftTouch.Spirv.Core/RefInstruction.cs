using System.Text;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;


namespace SoftTouch.Spirv.Core;

/// <summary>
/// A ref struct representation of an instruction in a buffer.
/// </summary>
public ref struct RefInstruction
{

    public static RefInstruction Empty => new() { Words = Span<int>.Empty, Operands = Span<int>.Empty };


    /// <summary>
    /// Word Count is the high-order 16 bits of word 0 of the instruction, holding its total WordCount. 
    /// <br/> If the instruction takes a variable number of operands, Word Count also says "+ variable", after stating the minimum size of the instruction.
    /// </summary>
    public int WordCount => Words[0] >> 16;
    public SDSLOp OpCode => (SDSLOp)(Words[0] & 0xFFFF);
    public int? ResultId { get; init; }
    public int? ResultType { get; init; }
    public Span<int> Operands { get; init; }
    public Memory<int>? Slice { get; init; }
    public int OwnerIndex { get; set; }
    public Span<int> Words { get; init; }



    public bool IsEmpty => Words == Span<int>.Empty;




    public OperandEnumerator GetEnumerator() => new(this);


    public T? GetOperand<T>(string name)
        where T : struct, IFromSpirv<T>
    {
        var info = InstructionInfo.GetInfo(OpCode);
        var infoEnumerator = info.GetEnumerator();
        var operandEnumerator = GetEnumerator();
        while(infoEnumerator.MoveNext())
        {
            if(operandEnumerator.MoveNext())
            {
                if(infoEnumerator.Current.Name == name)
                {
                    return operandEnumerator.Current.To<T>();
                }
            }
        }
        return null;
    }


    public static bool operator ==(RefInstruction r1, RefInstruction r2) => r1.Words == r2.Words;
    public static bool operator !=(RefInstruction r1, RefInstruction r2) => r1.Words != r2.Words;

    public static RefInstruction Parse(Memory<int> owner, int ownerIndex)
    {
        var words = owner.Span.Slice(ownerIndex, owner.Span[ownerIndex] >> 16);
        var index = 0;
        var op = (SDSLOp)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);
        if (info.HasResultType)
            resultType = words[++index];
        if (info.HasResult)
            result = words[++index];


        return new RefInstruction()
        {
            ResultId = result,
            ResultType = resultType,
            Operands = words[index..],
            OwnerIndex = ownerIndex,
            Slice = owner,
            Words = words
        };
    }
    public static RefInstruction ParseRef(Span<int> words)
    {
        var index = 0;
        var op = (SDSLOp)(words[0] & 0xFFFF);
        int? result = null!;
        int? resultType = null!;

        var info = InstructionInfo.GetInfo(op);

        if (info.HasResultType)
            resultType = words[++index];
        if (info.HasResult)
            result = words[++index];

        return new RefInstruction()
        {
            ResultId = result,
            ResultType = resultType,
            Operands = words[1..],
            Words = words,
        };
    }


    int? GetResultId()
    {
        foreach (var o in this)
            if (o.Kind == OperandKind.IdResult)
                return o.Words[0];
        return null;
    }
    int? GetResultType()
    {
        foreach (var o in this)
            if (o.Kind == OperandKind.IdResultType)
                return o.Words[0];
        return null;
    }

    public void SetResultId(int id)
    {
        foreach(var o in this)
            if(o.Kind == OperandKind.IdResult)
                o.Words[0] = id;
    }
    public void SetResultType(int id)
    {
        foreach (var o in this)
            if (o.Kind == OperandKind.IdResultType)
                o.Words[0] = id;
    }
    //public void CopyTo(Span<int> destination)
    //{
    //    Words.CopyTo(destination);
    //    var refi = new RefInstruction() { Words = destination};
    //    foreach(var o in refi)
    //        o.ReplaceIdResult(ResultIdReplacement);
    //}

    public void OffsetIds(int offset)
    {
        foreach(var o in this)
        {
            if (o.Kind == OperandKind.IdRef)
                o.Words[0] += offset;
            else if (o.Kind == OperandKind.IdResult)
                o.Words[0] += offset;
            else if (o.Kind == OperandKind.IdResultType)
                o.Words[0] += offset;
            else if (o.Kind == OperandKind.PairIdRefLiteralInteger)
                o.Words[0] += offset;
            else if (o.Kind == OperandKind.PairLiteralIntegerIdRef)
                o.Words[1] += offset;
            else if (o.Kind == OperandKind.PairIdRefIdRef)
            {
                o.Words[0] += offset;
                o.Words[1] += offset;
            }
        }
    }


    

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(OpCode).Append(' ');
        foreach(var o in this)
        {
            builder.Append(o.ToString()).Append(' ');
        }
        return builder.ToString();
    }
}