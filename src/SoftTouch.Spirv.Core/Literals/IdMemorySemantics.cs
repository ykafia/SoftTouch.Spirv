namespace SoftTouch.Spirv.Core;


public record struct IdMemorySemantics(int Value) : IFromSpirv<IdMemorySemantics>
{
    public static implicit operator int(IdMemorySemantics r) => r.Value;
    public static implicit operator IdMemorySemantics(int v) => new(v);
    public static IdMemorySemantics From(Span<int> words) => new() { Value = words[0] };

    public static IdMemorySemantics From(string value)
    {
        throw new NotImplementedException();
    }
}