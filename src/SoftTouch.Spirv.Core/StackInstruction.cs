using static Spv.Specification;

namespace SoftTouch.Spirv.Core;


public ref struct MutRefInstruction
{
    public Span<int> Words { get;}
    public Op OpCode 
    {
        get => (Op)(Words[0] & 0xFF); 
        set { unchecked { Words[0] = (Words[0] & (int)(0xFFFF0000)) | (int)value;}}
    }
    public int WordCount
    {
        get => Words[0] >> 16; 
        private set => Words[0] = value << 16 | Words[0] & 0xFF;
    }

    private int _index;

    public MutRefInstruction(Span<int> words)
    {
        Words = words;
        WordCount = words.Length;
        _index = 1;
    }

    public void Add(LiteralInteger value)
    {
        if(value.WordCount > 1)
        {
            Words[_index++] = (int)(value.Words >> 32);
            Words[_index++] = (int)(value.Words & 0xFFFF);
        }
        else
        {
            Words[_index++] = (int)value.Words;
        }
    }
    public void Add(LiteralFloat value)
    {
        if(value.WordCount > 1)
        {
            Words[_index++] = (int)(value.Words >> 32);
            Words[_index++] = (int)(value.Words & 0xFFFF);
        }
        else
        {
            Words[_index++] = (int)value.Words;
        }
    }
    public void Add(LiteralString value)
    {
        value.WriteTo(Words[_index..(_index+value.WordLength)]);
        _index += value.WordLength;
    }
}