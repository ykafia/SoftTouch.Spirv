//using CommunityToolkit.HighPerformance.Buffers;
//using System.Collections;

//namespace SoftTouch.Spirv.Core;

//public class InstructionList : IList<RefInstruction>
//{

//    MemoryOwner<int> data;

//    public RefInstruction this[int index]
//    {
//        get 
//        {
//            var start = ConvertIndex(index);
//            var length = data.Span[start] >> 16;
//            return RefInstruction.Parse(data.Span.Slice(start,length));
//        }
//        set 
//        {
//            var toRemove = data[index];
//            toRemove.Dispose();
//            data[index] = value;
//        }
//    }

//    public int Count => CountInstructions();

//    public bool IsReadOnly => false;

//    public InstructionSet(int initialSize = 32)
//    {
//        data = MemoryOwner<int>.Allocate(initialSize);
//    }

//    int GetWordLength(int position)
//    {
//        return data.Span[position] >> 16;
//    }

//    int ConvertIndex(int index)
//    {
//        int widx = 5;
//        int count = 0;
//        while(count < index)
//        {
//            widx += data.Span[widx] >> 16;
//            count += 1;
//        }
//        return widx;
//    }

//    int CountInstructions()
//    {
//        int widx = 5;
//        int count = 0;
//        while (widx < data.Length)
//        {
//            widx += data.Span[widx] >> 16;
//            count += 1;
//        }
//        return widx;
//    }


//    public void Add(Instruction item)
//    {
//        data.Add(item);
//    }

//    public void Clear()
//    {
//        data.Clear();
//    }

//    public bool Contains(Instruction item)
//    {
//        return data.Contains(item);
//    }

//    public void CopyTo(Instruction[] array, int arrayIndex)
//    {
//        data.CopyTo(array, arrayIndex);
//    }

//    public IEnumerator<Instruction> GetEnumerator()
//    {
//        return data.GetEnumerator();
//    }

//    public int IndexOf(Instruction item)
//    {
//        return data.IndexOf(item);
//    }

//    public void Insert(int index, Instruction item)
//    {
//        data.Insert(index,item);
//    }

//    public bool Remove(Instruction item)
//    {
//        item.Dispose();
//        return data.Remove(item);
//    }

//    public void RemoveAt(int index)
//    {
//        data[index].Dispose();
//        data.RemoveAt(index);
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}