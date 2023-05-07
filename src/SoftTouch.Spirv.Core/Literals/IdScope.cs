namespace SoftTouch.Spirv.Core;


public record struct IdScope(int Value) : IFromSpirv<IdScope>
{
    public static implicit operator int(IdScope r) => r.Value;
    public static implicit operator IdScope(int v) => new(v);
    public static IdScope From(Span<int> words) => new() { Value = words[0] };

    public static IdScope From(string value)
    {
        throw new NotImplementedException();
    }
}