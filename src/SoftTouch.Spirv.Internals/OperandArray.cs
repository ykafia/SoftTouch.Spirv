using CommunityToolkit.HighPerformance.Buffers;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static Spv.Specification;

namespace SoftTouch.Spirv.Internals;


public sealed partial class OperandArray : IDisposable
{
    public int Length { get; private set; } = 0;
    MemoryOwner<int> data;

    public Span<int> Span => data.Span;

    public Span<int> this[Range range]
    {
        get
        {
            var a = range.Start.IsFromEnd ? Length - range.Start.Value : range.Start.Value;
            var b = range.End.IsFromEnd ? Length - range.End.Value : range.End.Value;
            var start = Math.Min(a, b);
            var end = Math.Max(a, b);
            var length = end - start;
            return data.Slice(start, length).Span;
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
        if (capacity != 0)
            data = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)capacity));
        else
            data = MemoryOwner<int>.Empty;
    }
    public OperandArray(Span<int> span)
    {
        data = MemoryOwner<int>.Allocate(span.Length);
        span.CopyTo(data.Span);
    }

    public void Expand(int size)
    {
        Length += size;
        if (data.Length < Length)
        {
            data.Dispose();
            data = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)Length));
        }
    }

    public void Clear()
    {
        Length = 0;
        data.Span.Clear();
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
        foreach( var item in value )
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