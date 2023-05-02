namespace SoftTouch.Spirv.Core;


public struct IdScope
{
    public int Value { get; set; }

    public IdScope(int v) { Value = v; }
    public static implicit operator int(IdScope r) => r.Value;
    public static implicit operator IdScope(int v) => new(v);
}