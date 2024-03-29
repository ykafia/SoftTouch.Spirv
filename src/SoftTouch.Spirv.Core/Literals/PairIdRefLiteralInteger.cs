namespace SoftTouch.Spirv.Core;


public record struct PairIdRefLiteralInteger((int,int) Value) : IFromSpirv<PairIdRefLiteralInteger>
{
    public static implicit operator (int,int)(PairIdRefLiteralInteger r) => r.Value;
    public static implicit operator PairIdRefLiteralInteger((int,int) v) => new(v);
    public static implicit operator LiteralInteger(PairIdRefLiteralInteger v) => new((ulong)(v.Value.Item1 << 16 | v.Value.Item2));
    public static PairIdRefLiteralInteger From(Span<int> words) => new() { Value = (words[0], words[1]) };

    public static PairIdRefLiteralInteger From(string value)
    {
        throw new NotImplementedException();
    }
}