namespace SoftTouch.Spirv.Core;


public struct IdResultType
{
    public int Value { get; set; }

    public IdResultType(int v) { Value = v; }
    public static implicit operator int(IdResultType r) => r.Value;
    public static implicit operator IdResultType(int v) => new(v);
}