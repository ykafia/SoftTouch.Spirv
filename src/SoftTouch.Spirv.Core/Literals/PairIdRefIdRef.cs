namespace SoftTouch.Spirv.Core;


public record struct PairIdRefIdRef((int,int) Value) : IFromSpirv<PairIdRefIdRef>
{
    public static implicit operator (int,int)(PairIdRefIdRef r) => r.Value;
    public static implicit operator PairIdRefIdRef((int,int) v) => new(v);
    public static implicit operator LiteralInteger(PairIdRefIdRef v) => new((ulong)(v.Value.Item1 << 16 | v.Value.Item2));
    public static PairIdRefIdRef From(Span<int> words) => new() { Value = (words[0], words[1]) };

    public static PairIdRefIdRef From(string value)
    {
        throw new NotImplementedException();
    }
}