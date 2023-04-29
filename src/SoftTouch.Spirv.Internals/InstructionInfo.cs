using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Spv.Specification;

namespace SoftTouch.Spirv.Internals;

public struct LogicalOperandArray : IList<LogicalOperand>
{
    List<LogicalOperand> LogicalOperands { get; }
    public bool HasResult => GetHasResult();
    public bool HasResultType => GetHasResultType();

    public int Count => LogicalOperands.Count;

    public bool IsReadOnly => false;

    public LogicalOperand this[int index] 
    {   get => LogicalOperands[index]; 
        set => LogicalOperands[index] = value; 
    }

    public LogicalOperandArray()
    {
        LogicalOperands = new();
    }

    bool GetHasResult()
    {
        foreach(var o in LogicalOperands)
        {
            if(o.Kind == OperandKind.IdResult)
                return true;
        }
        return false;
    }
    bool GetHasResultType()
    {
        foreach (var o in LogicalOperands)
        {
            if (o.Kind == OperandKind.IdResultType)
                return true;
        }
        return false;
    }


    public int IndexOf(LogicalOperand item)
    {
        return LogicalOperands.IndexOf(item);
    }

    public void Insert(int index, LogicalOperand item)
    {
        LogicalOperands.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        LogicalOperands.RemoveAt(index);
    }

    public void Add(LogicalOperand item)
    {
        LogicalOperands.Add(item);
    }

    public void Clear()
    {
        LogicalOperands.Clear();
    }

    public bool Contains(LogicalOperand item)
    {
        return LogicalOperands.Contains(item);
    }

    public void CopyTo(LogicalOperand[] array, int arrayIndex)
    {
        LogicalOperands.CopyTo(array, arrayIndex);
    }

    public bool Remove(LogicalOperand item)
    {
        return LogicalOperands.Remove(item);
    }

    public IEnumerator<LogicalOperand> GetEnumerator()
    {
        return LogicalOperands.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

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
