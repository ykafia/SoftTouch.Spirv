using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;



public readonly struct LiteralInteger : INumLiteral<int>
{
    public int Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    internal LiteralInteger(int value)
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

public readonly struct LiteralLong : INumLiteral<long>
{
    public long Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    internal LiteralLong(long value)
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

public readonly struct LiteralFloat : INumLiteral<float>
{
    public float Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    internal LiteralFloat(float value)
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

public readonly struct LiteralDouble : INumLiteral<double>
{
    public double Value { get; private init; }

    public MemoryOwner<int> Words { get; private init; }

    internal LiteralDouble(double value)
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