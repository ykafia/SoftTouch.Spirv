using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public sealed partial class OperandArray : IDisposable
{
    public int Length {get; private set;} = 0;
    MemoryOwner<int> data;

    public Span<int> Span => data.Span;

    public Span<int> this[Range range] 
    {
        get
        {
            var a = range.Start.IsFromEnd ? Length - range.Start.Value : range.Start.Value;
            var b = range.End.IsFromEnd ? Length - range.End.Value : range.End.Value;
            var start = Math.Min(a,b);
            var end = Math.Max(a,b);
            var length = end - start;
            return data.Slice(start,length).Span;
        }
    } 
    public int this[Index index]
    {
        get
        {
            if(index.IsFromEnd)
                return this[..][Length - index.Value];
            return this[..][index.Value];
        }
        set
        {
            if(index.IsFromEnd)
                this[..][Length - index.Value] = value;
            this[..][index.Value] = value;
        }
    }

    public OperandArray(int capacity)
    {
        data = MemoryOwner<int>.Allocate(capacity);
    }

    public void Clear()
    {
        Length = 0;
        data.Span.Fill(0);
    }

    public void Dispose()
    {
        data.Dispose();
    }
}