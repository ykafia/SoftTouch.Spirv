namespace SoftTouch.Spirv.Internals;


public readonly struct LiteralString
{
    public string Value { get; private init; }

    public LiteralString(string value)
    {
        Value = value;
    }
}