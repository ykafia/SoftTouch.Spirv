using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Core.Buffers;


public abstract class BufferBase<T>
    where T : struct
{
    protected MemoryOwner<T> _owner = MemoryOwner<T>.Empty;

    public virtual Span<T> Span => _owner.Span[..Length];
    public virtual Memory<T> Memory => _owner.Memory[..Length];
    public int Length { get; protected set; }
    public void Dispose() => _owner.Dispose();
}