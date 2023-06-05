using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv.Core;


public struct LiteralInteger : ISpirvElement, IFromSpirv<LiteralInteger>, ILiteralNumber
{

    // internal static Dictionary<byte, LiteralInteger> CacheByte { get; } = new();
    // internal static Dictionary<sbyte, LiteralInteger> CacheSByte { get; } = new();
    // internal static Dictionary<ushort, LiteralInteger> CacheUShort { get; } = new();
    // internal static Dictionary<short, LiteralInteger> CacheShort { get; } = new();
    // internal static Dictionary<uint, LiteralInteger> CacheUInt { get; } = new();
    // internal static Dictionary<int, LiteralInteger> CacheInt { get; } = new();
    // internal static Dictionary<ulong, LiteralInteger> CacheULong { get; } = new();
    // internal static Dictionary<long, LiteralInteger> CacheLong { get; } = new();

    public long Words { get; init; }
    public int Size { get; init; }
    public int WordCount => Size / 32;

    internal LiteralInteger(sbyte value)
    {
        Words = 0 | value;
        // CacheSByte.Add(value, this);
        Size = sizeof(sbyte) * 8;
    }
    internal LiteralInteger(byte value)
    {
        Words = 0 | value;
        // CacheByte.Add(value, this);
        Size = sizeof(byte) * 8;
    }

    internal LiteralInteger(short value)
    {
        Words = 0 | value;
        // CacheInt.Add(value, this);
        Size = sizeof(short) * 8;
    }
    internal LiteralInteger(ushort value)
    {
        Words = 0 | value;
        // CacheUInt.Add(value, this);
        Size = sizeof(ushort) * 8;
    }

    internal LiteralInteger(int value)
    {
        Words = 0 | value;
        // CacheInt.Add(value, this);
        Size = sizeof(int) * 8;
    }
    internal LiteralInteger(uint value)
    {
        Words = 0 | value;
        // CacheUInt.Add(value, this);
        Size = sizeof(uint) * 8;

    }
    internal LiteralInteger(long value)
    {
        Words = 0 | value;
        // CacheLong.Add(value, this);
        Size = sizeof(long) * 8;
    }
    internal LiteralInteger(ulong value)
    {
        Words = (long)value;
        // CacheULong.Add(value, this);
        Size = sizeof(ulong) * 8;

    }

    internal LiteralInteger(Span<int> value)
    {
        if(value.Length == 2)
        {
            Size = sizeof(long) * 8;
            Words = value[0] << 32 | value[1];
        }
        else if (value.Length == 1)
        {
            Size = sizeof(int) * 8;
            Words = value[0];
        }
    }


    public static implicit operator LiteralInteger(byte value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(sbyte value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(ushort value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(short value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(int value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(uint value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(long value) => new LiteralInteger(value);
    public static implicit operator LiteralInteger(ulong value) => new LiteralInteger(value);

    public void Write(ref SpirvWriter writer)
    {
        Span<int> span = stackalloc int[]
        {
            (int)(Words >> 32),
            (int)(Words & 0X000000FF)
        };
        if (Size < 64)
            writer.Write(span[1]);
        else
            writer.Write(span);
    }

    public static LiteralInteger From(Span<int> words)
    {
        return new(words);
    }

    public static LiteralInteger From(string value)
    {
        throw new NotImplementedException();
    }
}

