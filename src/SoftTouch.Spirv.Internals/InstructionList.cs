using System.Collections;

namespace SoftTouch.Spirv.Internals;


public class InstructionList
{
    public bool TryCast<T>(out InstructionList<T> list)
        where T : IInstruction
    {
        if(this is InstructionList<T> result)
        {
            list = result;
            return true;
        }
        list = null!;
        return false;
    }
}


public class InstructionList<T> : InstructionList, IList<T>
    where T : IInstruction
{

    List<T> data = new();

    public T this[int index] 
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

    public void Add(T item)
    {
        data.Add(item);
    }

    public void Clear()
    {
        data.Clear();
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        data.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return data.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        data.Insert(index,item);
    }

    public bool Remove(T item)
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