using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public readonly struct LiteralString : ILiteral<string>
{
    public string Value {get;}

    public MemoryOwner<int> Words {get;}

    internal LiteralString(string value)
    {
        Value = value;
        var wordLength = value.Length / 4;
        var rest = value.Length % 4;
        if(rest > 0)
            wordLength += 1;
        var byteLength = wordLength * 4;
        Words = MemoryOwner<int>.Allocate(wordLength);
        var span = value.AsSpan();
        for(int i = 0; i < wordLength; i++)
        {
            if(rest == 0)
            {
                int word = 
                    Convert.ToByte(span[4 * i]) << 24
                    | Convert.ToByte(span[4 * i + 1]) << 16
                    | Convert.ToByte(span[4 * i + 2]) << 8
                    | Convert.ToByte(span[4 * i + 3]);
                Words.Span[i] = word;
            }
            else
            {
                if(i < wordLength -1)
                {
                    int word = 
                        Convert.ToByte(span[4 * i]) << 24
                        | Convert.ToByte(span[4 * i + 1]) << 16
                        | Convert.ToByte(span[4 * i + 2]) << 8
                        | Convert.ToByte(span[4 * i + 3]);
                    Words.Span[i] = word;

                }
                else 
                {
                    if(rest == 1)
                        Words.Span[i] = 
                            Convert.ToByte(span[4*i]) << 24;
                    else if(rest == 2)
                        Words.Span[i] = 
                            Convert.ToByte(span[4 * i]) << 24
                            | Convert.ToByte(span[4 * i + 1]) << 16;
                    else if(rest == 3)
                        Words.Span[i] = 
                            Convert.ToByte(span[4 * i]) << 24
                            | Convert.ToByte(span[4 * i + 1]) << 16
                            | Convert.ToByte(span[4 * i + 2]) << 8;
                }
            }
        }
    }


    public void Dispose()
    {
        Words.Dispose();
    }
}