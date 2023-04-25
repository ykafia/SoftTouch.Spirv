using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public interface IInstruction : IDisposable
{
    public static abstract int Op { get; }
    public static abstract string OpName { get; }
    public int Id { get; }
    public string Name { get; }

    public OperandArray? Operands {get;}
}

public struct DummyInstruction : IInstruction
{
    public static int Op => 42;
    public static string OpName {get;} = "Dummy";

    public OperandArray? Operands {get; init;}

    public int Id => Op;

    public string Name => OpName;

    public DummyInstruction()
    {
        Operands = new(5);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}