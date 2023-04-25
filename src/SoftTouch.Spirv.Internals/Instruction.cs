using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public interface IInstruction : IDisposable
{
    public static abstract int Op { get; }
    public OperandArray Operands {get;}
}

public struct DummyInstruction : IInstruction
{
    public static int Op => 42;

    public OperandArray Operands {get; init;}

    public DummyInstruction()
    {
        Operands = new(5);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}