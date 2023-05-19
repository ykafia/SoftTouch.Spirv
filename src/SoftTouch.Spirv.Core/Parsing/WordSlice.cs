namespace SoftTouch.Spirv.Core;


public ref struct WordSlice
{
    public int Start { get; init; }
    public int Length { get; init; }
    internal Span<int> words;

    public WordSlice(int start, int length, Span<int> data)
    {
        Start = start;
        Length = length;
        words = data.Slice(Start, Length);
    }

    public void Clear()
    {
        words.Clear();
    }
    public void CopyTo(WordSlice destination)
    {
        words.CopyTo(destination.words);
    }
    public bool TryCopyTo(WordSlice destination)
    {
        return words.TryCopyTo(destination.words);
    }
}