using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public interface ILiteral<T> : IDisposable
    where T : unmanaged, INumber<T>
{
    public T Value { get; }
    public MemoryOwner<int> Words { get; }
}

public readonly struct LiteralInteger : ILiteral<int>
{
    public int Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    public LiteralInteger(int value)
    {
        Value = value;
        Words = MemoryOwner<int>.Allocate(1);
        Words.Span[0] = Value;
    }

    public void Dispose()
    {
        Words.Dispose();
    }
}

public readonly struct LiteralLong : ILiteral<long>
{
    public long Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    public LiteralLong(long value)
    {
        Value = value;
        Words = MemoryOwner<int>.Allocate(2);
        Words.Span[0] = (int)(Value >> 4);
        Words.Span[1] = (int)(Value & 0x00FF);
    }

    public void Dispose()
    {
        Words.Dispose();
    }
}

public readonly struct LiteralFloat : ILiteral<float>
{
    public float Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    public LiteralFloat(float value)
    {
        Value = value;
        Words = MemoryOwner<int>.Allocate(1);
        Words.Span[0] = BitConverter.SingleToInt32Bits(Value);
    }

    public void Dispose()
    {
        Words.Dispose();
    }
}

public readonly struct LiteralDouble : ILiteral<double>
{
    public double Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    public LiteralDouble(double value)
    {
        Value = value;
        Words = MemoryOwner<int>.Allocate(2);
        Words.Span[0] = (int)(BitConverter.DoubleToInt64Bits(Value) >> 4);
        Words.Span[1] = (int)(BitConverter.DoubleToInt64Bits(Value) & 0x00FF);
        
    }

    public void Dispose()
    {
        Words.Dispose();
    }
}