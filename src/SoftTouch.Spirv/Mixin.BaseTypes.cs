namespace SoftTouch.Spirv;

public partial class Mixin
{

    public void AddType<T>() where T : struct
    {
        if(typeof(T) == typeof(byte))
        {
            Types.Add(new("byte",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(8, 0)));
        }
        else if(typeof(T) == typeof(sbyte))
        {
            Types.Add(new("sbyte",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(8, 1)));
        }
        else if(typeof(T) == typeof(ushort))
        {
            Types.Add(new("ushort",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(16, 0)));
        }
        else if(typeof(T) == typeof(short))
        {
            Types.Add(new("short",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(16, 1)));
        }
        else if(typeof(T) == typeof(uint))
        {
            Types.Add(new("uint",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(32, 0)));
        }
        else if(typeof(T) == typeof(int))
        {
            Types.Add(new("int",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(32, 1)));
        }
        else if(typeof(T) == typeof(ulong))
        {
            Types.Add(new("ulong",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(64, 0)));
        }
        else if(typeof(T) == typeof(long))
        {
            Types.Add(new("long",SpvTypeVariant.Scalar,Buffer.AddOpTypeInt(64, 1)));
        }
        else if(typeof(T) == typeof(Half))
        {
            Types.Add(new("Half",SpvTypeVariant.Scalar,Buffer.AddOpTypeFloat(16)));
        }
        else if(typeof(T) == typeof(float))
        {
            Types.Add(new("float",SpvTypeVariant.Scalar,Buffer.AddOpTypeFloat(32)));
        }
        else if(typeof(T) == typeof(double))
        {
            Types.Add(new("double",SpvTypeVariant.Scalar,Buffer.AddOpTypeFloat(64)));
        }
        else 
            throw new Exception("Can only accept numeric types");
    }


    // public static Mixin BaseTypes { get; internal set; }
    // public static Mixin BaseFunc { get; internal set; }
    // static Mixin()
    // {
    //     BaseTypes = new Mixin("BaseTypes");
    //     MixinSourceProvider.Register(BaseTypes);

    //     var t_byte = BaseTypes.Buffer.AddOpTypeInt(8, 0);
    //     var t_sbyte = BaseTypes.Buffer.AddOpTypeInt(8, 1);
    //     var t_ushort = BaseTypes.Buffer.AddOpTypeInt(16, 0);
    //     var t_short = BaseTypes.Buffer.AddOpTypeInt(16, 1);
    //     var t_uint = BaseTypes.Buffer.AddOpTypeInt(32, 0);
    //     var t_int = BaseTypes.Buffer.AddOpTypeInt(32, 1);
    //     var t_ulong = BaseTypes.Buffer.AddOpTypeInt(64, 0);
    //     var t_long = BaseTypes.Buffer.AddOpTypeInt(64, 1);

    //     var t_half = BaseTypes.Buffer.AddOpTypeFloat(16);
    //     var t_float = BaseTypes.Buffer.AddOpTypeFloat(32);
    //     var t_double = BaseTypes.Buffer.AddOpTypeFloat(64);



    //     BaseFunc = new Mixin("BaseFunc");
    //     MixinSourceProvider.Register(BaseFunc);
    //     BaseFunc.Parents.Add("BaseTypes");
    // }
}