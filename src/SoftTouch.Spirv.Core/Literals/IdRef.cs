namespace SoftTouch.Spirv.Core;


public struct IdRef
{
    public int Value { get; set; }

    public IdRef(int v) { Value = v; }
    public static implicit operator int(IdRef r) => r.Value;
    public static implicit operator IdRef(int v) => new(v);
}