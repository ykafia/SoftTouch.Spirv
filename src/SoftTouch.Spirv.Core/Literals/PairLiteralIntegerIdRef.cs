namespace SoftTouch.Spirv.Core;


public record struct PairLiteralIntegerIdRef((int,int) Value) : IFromSpirv<PairLiteralIntegerIdRef>
{
    public static implicit operator (int,int)(PairLiteralIntegerIdRef r) => r.Value;
    public static implicit operator PairLiteralIntegerIdRef((int,int) v) => new(v);
    public static implicit operator LiteralInteger(PairLiteralIntegerIdRef v) => new((ulong)(v.Value.Item1 << 16 | v.Value.Item2));
    public static PairLiteralIntegerIdRef From(Span<int> words) => new() { Value = (words[0], words[1]) };

    public static PairLiteralIntegerIdRef From(string value)
    {
        throw new NotImplementedException();
    }
}