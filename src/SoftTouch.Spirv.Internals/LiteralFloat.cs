using System.Numerics;


namespace SoftTouch.Spirv.Internals;


public struct LiteralFloat : ISpirvElement
{
    // internal static Dictionary<Half, LiteralFloat> CacheHalf { get; } = new();
    // internal static Dictionary<float, LiteralFloat> CacheFloat { get; } = new();
    // internal static Dictionary<double, LiteralFloat> CacheDouble { get; } = new();

    long words;
    int size;

    public LiteralFloat(Half value)
    {
        words = BitConverter.HalfToInt16Bits(value);
        // CacheHalf.Add(value, this);
        size = 16;

    }
    public LiteralFloat(float value)
    {
        words = BitConverter.SingleToInt32Bits(value);;
        // CacheFloat.Add(value, this);
        size = sizeof(float) * 4;
    }
    public LiteralFloat(double value)
    {
        words = BitConverter.DoubleToInt64Bits(value);
        // CacheDouble.Add(value, this);
        size = sizeof(double) * 4;

    }

    public static implicit operator LiteralFloat(Half value) => new LiteralFloat(value);
    public static implicit operator LiteralFloat(float value) => new LiteralFloat(value); 
    public static implicit operator LiteralFloat(double value) => new LiteralFloat(value);


    
    public bool TryCast(out Half value)
    {
        short bits = (short)(words & 0X000000FF);
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
            (int)(words >> 32),
            (int)(words & 0X0000FFFF)
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
            value = BitConverter.Int64BitsToDouble(words);
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
            (int)(words >> 32),
            (int)(words & 0X00FF)
        };
        if(size < 64)
            writer.Write(span[1]);
        else
            writer.Write(span);
    }
}