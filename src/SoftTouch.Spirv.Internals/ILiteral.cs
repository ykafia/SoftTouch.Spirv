using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;

internal interface ILiteral<T> : IDisposable
{
    public T Value { get; }
    public MemoryOwner<int> Words { get; }
}

internal interface INumLiteral<T> : ILiteral<T>
    where T : unmanaged, INumber<T>
{
    
}