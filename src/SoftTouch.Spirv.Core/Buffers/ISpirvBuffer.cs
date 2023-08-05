namespace SoftTouch.Spirv.Core.Buffers;


public interface ISpirvBuffer
{
    Span<int> Span { get; }
    Memory<int> Memory { get; }
}