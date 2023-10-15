using System.Numerics;
using System.Security.Cryptography;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using static SoftTouch.Spirv.Core.Buffers.MultiBuffer;
using static Spv.Specification;

namespace SoftTouch.Spirv;

/// <summary>
/// Spirv Mixer object mainly designed around SDSL
/// </summary>
public sealed partial class Mixer : MixerBase
{
    //public FunctionFinder Functions => new(this);
    //FunctionBuffer functions;

    public MultiBufferLocalVariables LocalVariables => buffer.LocalVariables;
    public MultiBufferGlobalVariables GlobalVariables => buffer.GlobalVariables;




    public static Inheritance Create(string name)
    {
        return new(new(name));
    }

    public Mixer(string name) : base(name)
    {
        buffer.AddOpMemoryModel(AddressingModel.Logical, MemoryModel.GLSL450);
        buffer.AddOpExtension("SPV_GOOGLE_decorate_string");
    }

    public Mixer WithCapability(Capability capability)
    {
        buffer.AddOpCapability(capability); 
        return this;
    }

    public EntryPoint WithEntryPoint(ExecutionModel model, string name)
    {
        return new EntryPoint(this, model, name);
    }

    public Mixer Inherit(string mixin)
    {
        mixins.Add(mixin);
        buffer.AddOpSDSLMixinInherit(mixin);
        return this;
    }

    public Mixer WithType(string type, StorageClass? storage = null)
    {
        if (type.Contains('*'))
            CreateTypePointer(type.AsMemory(), storage ?? throw new Exception("storage should not be null"));
        else 
            GetOrCreateBaseType(type.AsMemory());
        return this;
    }

    public Mixer WithInput(string type, string name, string semantic)
    {
        var t_variable = GetOrCreateBaseType(type.AsMemory());
        var p_t_variable = buffer.AddOpTypePointer(StorageClass.Input, t_variable.ResultId ?? -1);
        buffer.AddOpSDSLIOVariable(p_t_variable.ResultId ?? -1, StorageClass.Input, name, semantic, null);
        return this;
    }
    public Mixer WithOutput(string type, string name, string semantic)
    {
        var t_variable = GetOrCreateBaseType(type.AsMemory());
        var p_t_variable = buffer.AddOpTypePointer(StorageClass.Output, t_variable.ResultId ?? -1);
        buffer.AddOpSDSLIOVariable(p_t_variable.ResultId ?? -1, StorageClass.Output, name, semantic, null);
        return this;
    }

    public Mixer WithConstant<T>(string name, T value)
        where T : struct
    {
        CreateConstant(name, value);
        return this;
    }
    public MixinInstruction CreateConstant<T>(T value)
        where T : struct
    {
        return value switch
        {
            sbyte v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("sbyte".AsMemory()).ResultId ?? -1, v),
            short v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("short".AsMemory()).ResultId ?? -1, v),
            int v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("int".AsMemory()).ResultId ?? -1, v),
            long v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("long".AsMemory()).ResultId ?? -1, v),
            byte v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("byte".AsMemory()).ResultId ?? -1, v),
            ushort v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("ushort".AsMemory()).ResultId ?? -1, v),
            uint v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("uint".AsMemory()).ResultId ?? -1, v),
            ulong v => buffer.AddOpConstant<LiteralInteger>(GetOrCreateBaseType("ulong".AsMemory()).ResultId ?? -1, v),
            float v => buffer.AddOpConstant<LiteralFloat>(GetOrCreateBaseType("float".AsMemory()).ResultId ?? -1, v),
            double v => buffer.AddOpConstant<LiteralFloat>(GetOrCreateBaseType("double".AsMemory()).ResultId ?? -1, v),
            Vector2 v => CreateConstantVector(v),
            Vector3 v => CreateConstantVector(v),
            Vector4 v => CreateConstantVector(v),
            _ => throw new NotImplementedException()
        };
    }
    public MixinInstruction CreateConstantVector<T>(T value)
        where T : struct
    {
        if (value is Vector2 vec2)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float2".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.Y);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1 });
            return cons;
        }
        else if (value is Vector3 vec3)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float3".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Z);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1 });
            return cons;
        }
        else if (value is Vector4 vec4)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float4".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Z);
            var c4 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.W);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1, c4.ResultId ?? -1 });
            return cons;
        }
        throw new NotImplementedException();
    }
    public MixinInstruction CreateConstant<T>(string name, T value)
        where T : struct
    {
        if (value is sbyte vi8)
        {
            var t_const = GetOrCreateBaseType("sbyte".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi8);
            return cons;
        }
        else if (value is short vi16)
        {
            var t_const = GetOrCreateBaseType("short".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi16);
            return cons;
        }
        else if (value is int vi32)
        {
            var t_const = GetOrCreateBaseType("int".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi32);
            return cons;
        }
        else if (value is long vi64)
        {
            var t_const = GetOrCreateBaseType("long".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vi64);
            return cons;
        }
        else if (value is byte vu8)
        {
            var t_const = GetOrCreateBaseType("byte".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu8);
            return cons;
        }
        else if (value is ushort vu16)
        {
            var t_const = GetOrCreateBaseType("ushort".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu16);
            return cons;
        }
        else if (value is uint vu32)
        {
            var t_const = GetOrCreateBaseType("uint".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu32);
            return cons;
        }
        else if (value is ulong vu64)
        {
            var t_const = GetOrCreateBaseType("ulong".AsMemory());
            var cons = buffer.AddOpConstant<LiteralInteger>(t_const.ResultId ?? -1, vu64);
            return cons;
        }
        else if (value is float vf32)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var cons = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vf32);
            return cons;
        }
        else if (value is double vf64)
        {
            var t_const = GetOrCreateBaseType("double".AsMemory());
            var cons = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vf64);
            return cons;
        }
        else if (value is Vector2 vec2)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float2".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec2.Y);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1 });
            return cons;
        }
        else if (value is Vector3 vec3)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float3".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec3.Z);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1 });
            return cons;
        }
        else if (value is Vector4 vec4)
        {
            var t_const = GetOrCreateBaseType("float".AsMemory());
            var t_const2 = GetOrCreateBaseType("float4".AsMemory());

            var c1 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.X);
            var c2 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Y);
            var c3 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.Z);
            var c4 = buffer.AddOpConstant<LiteralFloat>(t_const.ResultId ?? -1, vec4.W);
            var cons = buffer.AddOpConstantComposite(t_const2.ResultId ?? -1, stackalloc IdRef[] { c1.ResultId ?? -1, c2.ResultId ?? -1, c3.ResultId ?? -1, c4.ResultId ?? -1 });
            return cons;
        }

        throw new NotImplementedException();
    }
    public override string ToString()
    {
        return Disassembler.Disassemble(new SortedWordBuffer(buffer));
    }
}