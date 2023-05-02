namespace SoftTouch.Spirv.Core;


public struct IdMemorySemantics
{
    public int Value { get; set; }

    public IdMemorySemantics(int v) { Value = v; }
    public static implicit operator int(IdMemorySemantics r) => r.Value;
    public static implicit operator IdMemorySemantics(int v) => new(v);
}