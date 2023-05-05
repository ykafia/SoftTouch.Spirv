namespace SoftTouch.Spirv.Core;


public record struct IdResult(int Value) : IFromSpirv<IdResult>
{
    public static implicit operator int(IdResult r) => r.Value;
    public static implicit operator IdResult(int v) => new(v);
    public static IdResult From(Span<int> words) => new() { Value = words[0] };
}