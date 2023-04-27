using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Internals;


public struct LiteralString : ISpirvElement
{
    // internal static Dictionary<string, LiteralString> Cache { get;} = new();

    public string Value { get; init; }

    internal int WordLength => (Value.Length / 4) + (HasRest ? 1 : 0);
    internal bool HasRest => Value.Length % 4 > 0;
    internal int RestSize => Value.Length % 4;

    internal LiteralString(string value)
    {
        Value = value;
    }
    public static implicit operator LiteralString(string s) => new LiteralString(s);

    public void Write(ref SpirvWriter writer)
    {
        var wordLength = Value.Length / 4;
        var rest = Value.Length % 4;
        if (rest > 0)
            wordLength += 1;
        var byteLength = wordLength * 4;
        var span = Value.AsSpan();
        for (int i = 0; i < wordLength; i++)
        {
            if (rest == 0)
            {
                int word =
                    Convert.ToByte(span[4 * i]) << 24
                    | Convert.ToByte(span[4 * i + 1]) << 16
                    | Convert.ToByte(span[4 * i + 2]) << 8
                    | Convert.ToByte(span[4 * i + 3]);
                writer.Write(word);
            }
            else
            {
                if (i < wordLength - 1)
                {
                    int word =
                        Convert.ToByte(span[4 * i]) << 24
                        | Convert.ToByte(span[4 * i + 1]) << 16
                        | Convert.ToByte(span[4 * i + 2]) << 8
                        | Convert.ToByte(span[4 * i + 3]);
                    writer.Write(word);

                }
                else
                {
                    if (rest == 1)
                        writer.Write(
                            Convert.ToByte(span[4 * i]) << 24
                        );
                    else if (rest == 2)
                        writer.Write(
                            Convert.ToByte(span[4 * i]) << 24
                            | Convert.ToByte(span[4 * i + 1]) << 16
                        );
                    else if (rest == 3)
                        writer.Write(
                            Convert.ToByte(span[4 * i]) << 24
                            | Convert.ToByte(span[4 * i + 1]) << 16
                            | Convert.ToByte(span[4 * i + 2]) << 8
                        );
                }
            }
        }
    }
}