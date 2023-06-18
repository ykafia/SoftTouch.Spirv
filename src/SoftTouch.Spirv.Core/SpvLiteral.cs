namespace SoftTouch.Spirv.Core;


public ref struct SpvLiteral
{
    public OperandKind Kind { get; init; }
    Span<int> Words { get; init; }

    public SpvLiteral(OperandKind kind, Span<int> words)
    {
        Kind = kind;
        Words = words;
    }

    public void OffsetIdRef(int offset)
    {
        if(Kind == OperandKind.IdRef)
            Words[0] += offset;
    }

    public T To<T>() where T : IFromSpirv<T>
    {
        return T.From(Words);
    }
}