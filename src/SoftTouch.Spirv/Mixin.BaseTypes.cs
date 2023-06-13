namespace SoftTouch.Spirv;

public partial class Mixin
{
    public static Mixin BaseTypes { get; internal set; }
    public static Mixin BaseFunc { get; internal set; }
    static Mixin()
    {
        BaseTypes = new Mixin("BaseTypes");
        MixinSourceProvider.Register(BaseTypes);

        var t_byte = BaseTypes.Buffer.AddOpTypeInt(8, 0);
        var t_sbyte = BaseTypes.Buffer.AddOpTypeInt(8, 1);
        var t_ushort = BaseTypes.Buffer.AddOpTypeInt(16, 0);
        var t_short = BaseTypes.Buffer.AddOpTypeInt(16, 1);
        var t_uint = BaseTypes.Buffer.AddOpTypeInt(32, 0);
        var t_int = BaseTypes.Buffer.AddOpTypeInt(32, 1);
        var t_ulong = BaseTypes.Buffer.AddOpTypeInt(64, 0);
        var t_long = BaseTypes.Buffer.AddOpTypeInt(64, 1);

        var t_half = BaseTypes.Buffer.AddOpTypeFloat(16);
        var t_float = BaseTypes.Buffer.AddOpTypeFloat(32);
        var t_double = BaseTypes.Buffer.AddOpTypeFloat(64);



        BaseFunc = new Mixin("BaseFunc");
        MixinSourceProvider.Register(BaseFunc);
        BaseFunc.Parents.Add("BaseTypes");
    }
}