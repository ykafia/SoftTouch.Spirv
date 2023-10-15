using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv;

/// <summary>
/// Mixer base class
/// </summary>
public abstract class MixerBase
{
    protected MixinGraph mixins;
    protected MultiBuffer buffer;

    protected Action DisposeBuffers;

    public string Name { get; init; }
    

    public MixerBase(string name)
    {
        Name = name;
        buffer = new();
        buffer.AddOpSDSLMixinName(Name);
        mixins = new();
        DisposeBuffers = buffer.Dispose;
    }

    public virtual MixinBuffer Build()
    {
        buffer.AddOpSDSLMixinEnd();
        // TODO : do some validation here
        MixinSourceProvider.Register(new(Name, buffer));
        DisposeBuffers.Invoke();
        return MixinSourceProvider.Get(Name);
    }
}