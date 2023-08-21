using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv;


public abstract class MixerBase
{
    protected MixinGraph mixins;
    protected WordBuffer buffer;

    public string Name { get; init; }
    

    public MixerBase(string name)
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
        buffer.Dispose();
        return MixinSourceProvider.Get(Name);
    }
}