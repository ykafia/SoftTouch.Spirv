using SoftTouch.Spirv.Core.Parsing;
using System.Drawing;
using System.Numerics;


namespace SoftTouch.Spirv.Core;


public struct LiteralFloat : ISpirvElement, IFromSpirv<LiteralFloat>
{
    // internal static Dictionary<Half, LiteralFloat> CacheHalf { get; } = new();
    // internal static Dictionary<float, LiteralFloat> CacheFloat { get; } = new();
    // internal static Dictionary<double, LiteralFloat> CacheDouble { get; } = new();

    public long Words { get; set;  }
    int size;

    public int WordCount => size / 32;

    public LiteralFloat(Half value)
    {
        Words = BitConverter.HalfToInt16Bits(value);
        // CacheHalf.Add(value, this);
        size = 16;

    }
    public LiteralFloat(float value)
    {
        Words = BitConverter.SingleToInt32Bits(value);;
        // CacheFloat.Add(value, this);
        size = sizeof(float) * 8;
    }
    public LiteralFloat(double value)
    {
        Words = BitConverter.DoubleToInt64Bits(value);
        // CacheDouble.Add(value, this);
        size = sizeof(double) * 8;

    }
    public LiteralFloat(Span<int> words)
    {
        if (words.Length == 2)
        {
            size = sizeof(long) * 8;
            Words = words[0] << 32 | words[1];
        }
        else if (words.Length == 1)
        {
            size = sizeof(int) * 8;
            Words = words[0];
        }

    }


    public static implicit operator LiteralFloat(Half value) => new LiteralFloat(value);
    public static implicit operator LiteralFloat(float value) => new LiteralFloat(value); 
    public static implicit operator LiteralFloat(double value) => new LiteralFloat(value);


    
    public bool TryCast(out Half value)
    {
        short bits = (short)(Words & 0X000000FF);
        if(size == 32)
        {
            value = BitConverter.Int16BitsToHalf(bits);
            return true;
        }
        else
        {
            value = Half.Zero;
            return false;
        }
    }
    public bool TryCast(out float value)
    {
        Span<int> span = stackalloc int[]
        {
            (int)(Words >> 32),
            (int)(Words & 0X0000FFFF)
        };
        if(size == 32)
        {
            value = BitConverter.Int32BitsToSingle(span[1]);
            return true;
        }
        else
        {
            value = 0;
            return false;
        }
    }
    public bool TryCast(out double value)
    {
        if(size == 64)
        {
            value = BitConverter.Int64BitsToDouble(Words);
            return true;
        }
        else
        {
            value = 0;
            return false;
        }
    }
    


    public void Write(ref SpirvWriter writer)
    {
        Span<int> span = stackalloc int[]
        {
            (int)(Words >> 32),
            (int)(Words & 0X00FF)
        };
        if(size < 64)
            writer.Write(span[1]);
        else
            writer.Write(span);
    }

    public static LiteralFloat From(Span<int> words) => new(words);
}