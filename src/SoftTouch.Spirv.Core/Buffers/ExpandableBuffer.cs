using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Core;


public class ExpandableBuffer<T> : BufferBase<T>
    where T : struct
{
    public Span<T>.Enumerator GetEnumerator() => Span.GetEnumerator();

    public ExpandableBuffer()
    {
        _owner = MemoryOwner<T>.Allocate(4, AllocationMode.Clear);
        Length = 0;
    }

    public ExpandableBuffer(int initialCapacity)
    {
        _owner = MemoryOwner<T>.Allocate(initialCapacity, AllocationMode.Clear);
        Length = 0;
    }

    public void Expand(int size)
    {
        if(Length + size > _owner.Length)
        {
            var n = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Length + size)), AllocationMode.Clear);
            Span.CopyTo(n.Span);
            _owner = n;
        }
    }

    public void Add(T item)
    {
        Expand(1);
        _owner.Span[Length] = item;
        Length += 1;
    }
    public void Add(Span<T> items)
    {
        Expand(items.Length);
        items.CopyTo(_owner.Span[Length..]);
        Length += items.Length;
    }

    public void Insert(int start, Span<T> words)
    {
        Expand(words.Length);
        var slice = _owner.Span[start..Length];
        slice.CopyTo(_owner.Span[(start + words.Length)..]);
        words.CopyTo(_owner.Span.Slice(start, words.Length));
        Length += words.Length;
    }
    

    public bool RemoveAt(int index)
    {
        if(index < Length && index > 0)
        {
            Span[(index+1)..].CopyTo(Span[index..]);
            Length -= 1;
            return true;
        }
        return false;
    }
}