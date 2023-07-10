using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv.Core;


public ref struct SpvOperand
{
    public OperandKind Kind { get; init; }
    Span<int> Words { get; init; }
    public int Offset { get; init; }

    public SpvOperand(OperandKind kind, Span<int> words, int idRefOffset = 0)
    {
        Kind = kind;
        Words = words;
        Offset = idRefOffset;
    }

    public void OffsetIdRef(int offset)
    {
        if (Kind == OperandKind.IdRef)
            Words[0] += offset;
        else if(Kind == OperandKind.IdResult)
            Words[0] += offset;
        else if(Kind == OperandKind.IdResultType)
            Words[0] += offset;
    }
    public T ToEnum<T>() where T : Enum
    {
        return Unsafe.As<int,T>(ref Words[0]);
    }
    public T To<T>() where T : struct, IFromSpirv<T>
    {
        if (Kind == OperandKind.IdRef && typeof(T) == typeof(IdRef))
        {
            var id = new IdRef(Words[0] + Offset);
            var result = Unsafe.As<IdRef, T>(ref id);
            return result;
        }
        return T.From(Words);
    }
}