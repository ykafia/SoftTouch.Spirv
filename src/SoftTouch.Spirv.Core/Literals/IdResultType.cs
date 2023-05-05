namespace SoftTouch.Spirv.Core;


public record struct IdResultType(int Value) : IFromSpirv<IdResultType>
{
    public static implicit operator int(IdResultType r) => r.Value;
    public static implicit operator IdResultType(int v) => new(v);
    public static IdResultType From(Span<int> words) => new() { Value = words[0] };
}