using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Core;


public class ExpandableBuffer<T>
    where T : struct
{
    MemoryOwner<T> _owner = MemoryOwner<T>.Empty;

    public Span<T> Span => _owner.Span[..Count];
    public Memory<T> Memory => _owner.Memory[..Count];

    public int Count { get; private set; }

    public Span<T>.Enumerator GetEnumerator() => Span.GetEnumerator();

    public ExpandableBuffer()
    {
        _owner = MemoryOwner<T>.Allocate(4, AllocationMode.Clear);
        Count = 0;
    }

    public ExpandableBuffer(int initialCapacity)
    {
        _owner = MemoryOwner<T>.Allocate(initialCapacity, AllocationMode.Clear);
        Count = 0;
    }

    void Expand(int size)
    {
        if(Count + size > _owner.Length)
        {
            var n = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Count + size)), AllocationMode.Clear);
            Span.CopyTo(n.Span);
            _owner = n;
        }
    }

    public void Add(T item)
    {
        Expand(1);
        _owner.Span[Count] = item;
        Count += 1;
    }
    public void Add(Span<T> items)
    {
        Expand(items.Length);
        items.CopyTo(_owner.Span[Count..]);
        Count += 1;
    }
    public bool RemoveAt(int index)
    {
        if(index < Count && index > 0)
        {
            Span[(index+1)..].CopyTo(Span[index..]);
            Count -= 1;
            return true;
        }
        return false;
    }
}