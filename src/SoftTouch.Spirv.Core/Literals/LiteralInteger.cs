using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv.Core;


public struct LiteralInteger : ISpirvElement
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
    int size;

    internal LiteralInteger(sbyte value)
    {
        Words = 0 | value;
        // CacheSByte.Add(value, this);
        size = sizeof(sbyte) * 4;
    }
    internal LiteralInteger(byte value)
    {
        Words = 0 | value;
        // CacheByte.Add(value, this);
        size = sizeof(byte) * 4;
    }

    internal LiteralInteger(short value)
    {
        Words = 0 | value;
        // CacheInt.Add(value, this);
        size = sizeof(short) * 4;
    }
    internal LiteralInteger(ushort value)
    {
        Words = 0 | value;
        // CacheUInt.Add(value, this);
        size = sizeof(ushort) * 4;
    }

    internal LiteralInteger(int value)
    {
        Words = 0 | value;
        // CacheInt.Add(value, this);
        size = sizeof(int) * 4;
    }
    internal LiteralInteger(uint value)
    {
        Words = 0 | value;
        // CacheUInt.Add(value, this);
        size = sizeof(uint) * 4;

    }
    internal LiteralInteger(long value)
    {
        Words = 0 | value;
        // CacheLong.Add(value, this);
        size = sizeof(long) * 4;
    }
    internal LiteralInteger(ulong value)
    {
        Words = (long)value;
        // CacheULong.Add(value, this);
        size = sizeof(ulong) * 4;

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
        if (size < 64)
            writer.Write(span[1]);
        else
            writer.Write(span);
    }
}

