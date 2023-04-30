using CommunityToolkit.HighPerformance.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core;

public struct ArrayOrUnique
{
    public int Length { get; private set; } = 0;

    private MemoryOwner<int>? array;
    int unique;

    public bool IsArray => array != null;
    public bool IsUnique => array == null;

    public Span<int> Span => GetSpan();

    public ArrayOrUnique(Span<int> values)
    {
        if (values.Length == 1)
            unique = values[0];
        else if (values.Length == 0)
            array = MemoryOwner<int>.Empty;
        else
        {
            array = MemoryOwner<int>.Allocate(values.Length);
            values.CopyTo(array.Span);
        }
    }
    public ArrayOrUnique(int capacity)
    {
        if(capacity > 1)
            array = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)capacity));
        else if(capacity == 0)
            array = MemoryOwner<int>.Empty;
    }

    public Span<int> GetSpan()
    {
        if (!IsArray)
        {
            return MemoryMarshal.CreateSpan(ref unique, 1);
        }
        else if (array != null)
            return array.Span;
        else throw new Exception("No data");
    }

    public void Expand(int size)
    {
        Length += size;
        if (array?.Length < Length)
        {
            array.Dispose();
            array = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)Length));
        }
    }
    public void Clear()
    {
        Length = 0;
        array?.Span.Clear();
    }

    public Span<int> Slice(int start) => Span[start..];
    public Span<int> Slice(int start, int length) => Span.Slice(start, length);

    public void Dispose() =>array?.Dispose();
}

public partial struct OperandArray : IDisposable
{

    ArrayOrUnique data;

    public Span<int> Span => data.Span;
    public int Length => data.Length;

    public Span<int> this[Range range]
    {
        get
        {
            var a = range.Start.IsFromEnd ? Length - range.Start.Value : range.Start.Value;
            var b = range.End.IsFromEnd ? Length - range.End.Value : range.End.Value;
            var start = Math.Min(a, b);
            var end = Math.Max(a, b);
            var length = end - start;
            return data.Slice(start, length);
        }
    }
    public int this[Index index]
    {
        get
        {
            if (index.IsFromEnd)
                return this[..][Length - index.Value];
            return this[..][index.Value];
        }
        set
        {
            if (index.IsFromEnd)
                this[..][Length - index.Value] = value;
            this[..][index.Value] = value;
        }
    }

    public OperandArray(int capacity)
    {
        data = new(capacity);
    }
    public OperandArray(Span<int> span)
    {
        data = new(span);
    }

    
    public void Expand(int size)
    {
        data.Expand(size);
    }

    public void Clear()
    {
        data.Clear();
    }

    public void Dispose()
    {
        data.Dispose();
    }

    public void Add(LiteralString value)
    {
        var start = Length - 1;
        var span = value.Value.AsSpan();
        var wordLength = value.WordLength;

        for (int i = 0; i < wordLength; i++)
        {
            if (!value.HasRest)
            {
                Add(
                    Convert.ToByte(
                       (byte)span[i * 4] << 24 | (byte)span[i * 4 + 1] << 16 | (byte)span[i * 4 + 2] << 8 | (byte)span[i * 4 + 3]
                ));
            }
            else
            {
                var rest = 0;
                var offset = 24;
                int result = 0;
                while (offset > 0 && rest < value.RestSize)
                {
                    result |= span[i * 4 + rest] << offset;
                    offset -= 8;
                    rest += 1;
                }
                Add(result);
            }
        }
    }
    public void Add(int value)
    {
        var start = Length - 1;
        Expand(1);
        data.Span[start] = value;
    }
    public void Add(int[] value)
    {
        var start = Length - 1;
        foreach (var item in value)
        {
            Add(item);
        }
    }
    public void Add(int? value)
    {
        if (value.HasValue)
        {
            var start = Length - 1;
            Expand(1);
            data.Span[start] = value.Value;
        }
    }
    public void Add<T>(T value)
        where T : Enum
    {
        Expand(1);
        Add(Convert.ToInt32(value));
    }

}