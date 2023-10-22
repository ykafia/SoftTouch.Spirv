using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv.Core;

/// <summary>
/// Helps create instruction through stack allocations instead of buffers
/// </summary>
public ref struct MutRefInstruction
{
    public Span<int> Words { get; }
    public Span<int> Operands => Words [1..];
    public SDSLOp OpCode 
    {
        get => (SDSLOp)(Words[0] & 0xFFFF); 
        set { unchecked { Words[0] = (Words[0] & (int)0xFFFF0000) | (int)value;}}
    }
    public int WordCount
    {
        get => Words[0] >> 16; 
        set => Words[0] = value << 16 | Words[0] & 0xFFFF;
    }


    private int _index;

    public MutRefInstruction(Span<int> words)
    {
        Words = words;
        WordCount = words.Length;
        _index = 1;
    }

    public void AddInt(LiteralInteger value)
    {
        if(value.WordCount > 1)
        {
            Words[_index++] = (int)(value.Words >> 32);
            Words[_index++] = (int)(value.Words & 0xFFFF);
        }
        else
        {
            Words[_index++] = (int)value.Words;
        }
    }
    public void AddFloat(LiteralFloat value)
    {
        if(value.WordCount > 1)
        {
            Words[_index++] = (int)(value.Words >> 32);
            Words[_index++] = (int)(value.Words & 0xFFFF);
        }
        else
        {
            Words[_index++] = (int)value.Words;
        }
    }
    
    public void AddString(LiteralString value)
    {
        value.WriteTo(Words[_index..(_index+value.WordLength)]);
        LiteralString.Parse(Words[1..]);
        _index += value.WordLength;
    }
    public void Add(Span<IdRef> values)
    {
        foreach(var e in values)
            Add(e);
    }
    public void Add(Span<LiteralInteger> values)
    {
        foreach(var e in values)
            Add(e);
    }
    public void Add(Span<PairLiteralIntegerIdRef> values)
    {
        foreach(var e in values)
            Add(e);
    }
    public void Add(Span<PairIdRefLiteralInteger> values)
    {
        foreach(var e in values)
            Add(e);
    }
    public void Add(Span<PairIdRefIdRef> values)
    {
        foreach(var e in values)
            Add(e);
    }

    public void Add<T>(T? value)
    {
        if (value != null)
        {
            if (value is int i)
                AddInt(i);
            else if (value is LiteralInteger li)
                AddInt(li);
            else if (value is IdRef id)
                AddInt(id);
            else if (value is IdResultType result)
                AddInt(result);
            else if (value is IdResult resultid)
                AddInt(resultid);
            else if (value is float f)
                AddFloat(f);
            else if (value is LiteralFloat lf)
                AddFloat(lf);
            else if (value is PairIdRefIdRef pair)
            {
                AddInt(pair.Value.Item1);
                AddInt(pair.Value.Item2);
            }
            else if (value is PairIdRefLiteralInteger pair2)
            {
                AddInt(pair2.Value.Item1);
                AddInt(pair2.Value.Item2);
            }
            else if (value is PairLiteralIntegerIdRef pair3)
            {
                AddInt(pair3.Value.Item1);
                AddInt(pair3.Value.Item2);
            }
            else if (value is string s)
                AddString(s);
            else if (value is LiteralString ls)
                AddString(ls.Value);
            else if (value is Enum e)
                Add(Convert.ToInt32(e));
        }
    }

    public OperandEnumerator GetEnumerator() => new OperandEnumerator(RefInstruction.ParseRef(Words));
    public T GetOperand<T>(string name)
        where T : struct, IFromSpirv<T>
    {
        var info = InstructionInfo.GetInfo(OpCode);
        var infoEnumerator = info.GetEnumerator();
        var operandEnumerator = GetEnumerator();
        while (infoEnumerator.MoveNext())
        {
            operandEnumerator.MoveNext();
            if (infoEnumerator.Current.Name == name)
            {
                return operandEnumerator.Current.To<T>();
            }
        }
        throw new Exception($"Instruction {OpCode} has no operand named \"{name}\"");
    }
}