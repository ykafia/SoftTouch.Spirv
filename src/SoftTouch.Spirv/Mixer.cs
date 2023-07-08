using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    // public Mixin? Module { get; protected set; }
    MixinGraph mixins;
    public string Name { get; init; }
    WordBuffer buffer;

    public Mixer(string name)
    {
        Name = name;
        buffer = new();
        buffer.AddOpSDSLMixinName(Name);
        mixins = new();
    }

    public void Build()
    {
        throw new NotImplementedException();
    }

    public Mixer Inherit(string mixin)
    {
        mixins.Add(mixin);
        buffer.AddOpSDSLMixinInherit(mixin);
        return this;
    }

    public override string ToString()
    {
        var dis = new Disassembler();
        return dis.Disassemble(buffer);
    }
}