using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessor;


public static class PostProcessor
{
    public static SpirvBuffer Process(string mixinName)
    {
        var buffer = new SpirvBuffer();
        var mixin = MixinSourceProvider.Get(mixinName);
        foreach (var m in mixin.Parents.ToGraph())
            buffer.Add(m.Buffer);
        buffer.Add(mixin.Buffer);

        Apply(buffer);

        return buffer;
    }

    static void Apply(SpirvBuffer buffer)
    {
        Apply<IdRefOffsetter>(buffer);
        Apply<TypeDuplicateRemover>(buffer);
        Apply<BoundReducer>(buffer);
        Apply<SDSLOpRemover>(buffer);
        Apply<MixinMerger>(buffer);
    }

    static void Apply<T>(SpirvBuffer buffer)
        where T : struct, IPostProcessorPass
    {
        var p = new T();
        p.Apply(buffer);
    }
}