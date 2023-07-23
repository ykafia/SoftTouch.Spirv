using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    // public Mixin? Module { get; protected set; }
    MixinGraph mixins;
    public string Name { get; init; }
    WordBuffer buffer;

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
    }

    public Mixin Build()
    {
        buffer.AddOpSDSLMixinEnd();
        // TODO : do some validation here
        MixinSourceProvider.Register(new(Name, new(buffer)));
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

    public override string ToString()
    {
        var dis = new Disassembler();
        return dis.Disassemble(buffer);
    }
}