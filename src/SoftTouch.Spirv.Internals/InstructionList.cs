using System.Collections;

namespace SoftTouch.Spirv.Internals;

public class InstructionList : IList<Instruction>
{

    List<Instruction> data = new();

    public Instruction this[int index] 
    { 
        get => data[index]; 
        set 
        {
            var toRemove = data[index];
            toRemove.Dispose();
            data[index] = value;
        }
    }

    public int Count => data.Count;

    public bool IsReadOnly => false;

    public void Add(Instruction item)
    {
        data.Add(item);
    }

    public void Clear()
    {
        data.Clear();
    }

    public bool Contains(Instruction item)
    {
        return data.Contains(item);
    }

    public void CopyTo(Instruction[] array, int arrayIndex)
    {
        data.CopyTo(array, arrayIndex);
    }

    public IEnumerator<Instruction> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    public int IndexOf(Instruction item)
    {
        return data.IndexOf(item);
    }

    public void Insert(int index, Instruction item)
    {
        data.Insert(index,item);
    }

    public bool Remove(Instruction item)
    {
        item.Dispose();
        return data.Remove(item);
    }

    public void RemoveAt(int index)
    {
        data[index].Dispose();
        data.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}