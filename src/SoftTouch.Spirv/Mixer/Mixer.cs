using System.Numerics;
using System.Security.Cryptography;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    // public Mixin? Module { get; protected set; }
    MixinGraph mixins;
    public string Name { get; init; }
    WordBuffer buffer;

    ExpandableBuffer<(string, int)> constants;

    public static Inheritance Create(string name)
    {
        return new(new(name));
    }

    private Mixer(string name)
    {
        Name = name;
        buffer = new();
        buffer.AddOpSDSLMixinName(Name);
        mixins = new();
        constants = new();
    }

    public EntryPoint WithEntryPoint(Spv.Specification.ExecutionModel model, string name)
    {
        return new EntryPoint(this, model, name);
    }

    public Mixin Build()
    {
        buffer.AddOpSDSLMixinEnd();
        // TODO : do some validation here
        MixinSourceProvider.Register(new(Name, new(buffer)));
        buffer.Dispose();
        return MixinSourceProvider.Get(Name);
    }

    public Mixer Inherit(string mixin)
    {
        mixins.Add(mixin);
        buffer.AddOpSDSLMixinInherit(mixin);
        return this;
    }

    public Mixer WithType(string type)
    {
        GetOrCreateBaseType(type);
        return this;
    }
    
    public Mixer WithConstant<T>(string name, T value)
        where T : struct
    {
        CreateConstant(name, value);
        return this;
    }
    public MixinRefInstruction CreateConstant<T>(string name, T value)
        where T : struct
    {
        if (value is int vi8)
        {
            var t_const = GetOrCreateBaseType("sbyte");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi8);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is int vi16)
        {
            var t_const = GetOrCreateBaseType("short");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi16);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is int vi32)
        {
            var t_const = GetOrCreateBaseType("int");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi32);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is long vi64)
        {
            var t_const = GetOrCreateBaseType("long");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi64);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is int vu8)
        {
            var t_const = GetOrCreateBaseType("byte");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu8);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is int vu16)
        {
            var t_const = GetOrCreateBaseType("ushort");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu16);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is int vu32)
        {
            var t_const = GetOrCreateBaseType("uint");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu32);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is long vu64)
        {
            var t_const = GetOrCreateBaseType("ulong");
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu64);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is float vf32)
        {
            var t_const = GetOrCreateBaseType("float");
            var cons = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vf32);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is float vf64)
        {
            var t_const = GetOrCreateBaseType("double");
            var cons = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vf64);
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is Vector2 vec2)
        {
            var t_const = GetOrCreateBaseType("float");
            var t_const2 = GetOrCreateBaseType("float2");

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.Y);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1 });
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is Vector3 vec3)
        {
            var t_const = GetOrCreateBaseType("float");
            var t_const2 = GetOrCreateBaseType("float3");

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Z);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1 });
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }
        else if (value is Vector4 vec4)
        {
            var t_const = GetOrCreateBaseType("float");
            var t_const2 = GetOrCreateBaseType("float3");

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Z);
            var c4 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.W);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1, c4.ResultId ?? -1 });
            constants.Add((name, cons.ResultId ?? -1));
            return cons.AsRef();
        }

        throw new NotImplementedException();
    }


    public override string ToString()
    {
        var dis = new Disassembler();
        return dis.Disassemble(buffer);
    }
}