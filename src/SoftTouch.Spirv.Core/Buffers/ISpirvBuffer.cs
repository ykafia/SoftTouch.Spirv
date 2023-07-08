namespace SoftTouch.Spirv.Core;


public interface ISpirvBuffer
{
    Span<int> Span { get; }
    Memory<int> Memory { get; }
}