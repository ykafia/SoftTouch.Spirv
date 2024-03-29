namespace SoftTouch.Spirv.Core;


public record struct IdRef(int Value) : IFromSpirv<IdRef>
{
    public static implicit operator int(IdRef r) => r.Value;
    public static implicit operator IdRef(int v) => new(v);
    public static implicit operator LiteralInteger(IdRef v) => new(v);
    public static IdRef From(Span<int> words) => new() { Value = words[0] };

    public static IdRef From(string value)
    {
        throw new NotImplementedException();
    }
}