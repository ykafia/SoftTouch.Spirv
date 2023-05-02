namespace SoftTouch.Spirv.Core;


public struct IdResult
{
    public int Value { get; set; }

    public IdResult(int v) { Value = v; }
    public static implicit operator int(IdResult r) => r.Value;
    public static implicit operator IdResult(int v) => new(v);
}