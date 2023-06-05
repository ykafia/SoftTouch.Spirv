using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;
using System.Text;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;


public struct LiteralString : ISpirvElement, IFromSpirv<LiteralString>
{
    // internal static Dictionary<string, LiteralString> Cache { get;} = new();

    public string Value { get; init; }
    public int Length => Value.Length + 1;

    public int WordLength => (Length / 4) + (HasRest ? 1 : 0);
    internal bool HasRest => Length % 4 > 0;
    internal int RestSize => Length % 4;

    internal LiteralString(string value)
    {
        Value = value;
    }
    internal LiteralString(Span<int> words)
    {
        var chars = MemoryMarshal.Cast<int, char>(words);
        Value = chars.ToString();
    }
    public static implicit operator LiteralString(string s) => new LiteralString(s);


    public void WriteTo(Span<int> slice)
    {
        for (int i = 0; i < Length + 1; i++)
        {
            var pos = i / 4;
            var shift = 8 * (i % 4);
            var value = i < Value.Length ? Value[i] : '\0';
            slice[pos] |=  value << shift;
        }
        var x = 0;

        //Span<byte> bytes = stackalloc byte[WordLength * 4];
        //Encoding.UTF8.GetBytes(Value.AsSpan(), bytes);
        //var words = MemoryMarshal.Cast<byte, int>(bytes);
        //slice.Clear();
        //words.CopyTo(slice);
    }

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

    public static string Parse(Span<int> input)
    {
        //Console.WriteLine((Op)input[0]);
        //foreach (var e in input)
        //{
        //    Console.Write((char)(e & 0xFF) + "-");
        //    Console.Write((char)(e >> 8 & 0xFF) + "-");
        //    Console.Write((char)(e >> 16 & 0xFF) + "-");
        //    Console.WriteLine((char)(e >> 24 & 0xFF));
        //}

        Span<char> chars = stackalloc char[input.Length * 4];
        for(int i = 0;  i < input.Length; i++)
        {
            chars[i * 4] = (char)(input[i] & 0xFF);
            chars[i * 4 + 1] = (char)(input[i] >> 8 & 0xFF);
            chars[i * 4 + 2] = (char)(input[i] >> 16 & 0xFF);
            chars[i * 4 + 3] = (char)(input[i] >> 24 & 0xFF);
        }
        var bytes = MemoryMarshal.Cast<int,byte>(input);
        var result = Encoding.UTF8.GetString(bytes);
        return Encoding.ASCII.GetString(bytes);   
    }

    public static LiteralString From(Span<int> words)
    {
        return new(words);
    }

    public static LiteralString From(string value) => value;
}