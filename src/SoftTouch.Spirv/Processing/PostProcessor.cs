using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessor;


public class PostProcessor : IDisposable
{
    public SpirvBuffer Buffer { get; init; }

    public List<PostProcessorPassBase> AdditionalPasses { get; init; }

    public PostProcessor(SpirvBuffer buffer)
    {
        Buffer = buffer;
        AdditionalPasses = new();
    }

    public PostProcessor(string mixinName)
    {
        Buffer = new();
        // Words = new BufferBase<int>();
        var mixin = MixinSourceProvider.Get(mixinName);
        foreach (var m in mixin.Parents.ToGraph())
        {
            Buffer.Add(m.Buffer);
        }
        Buffer.Add(mixin.Buffer);
        AdditionalPasses = new();
        AddPasses<IdRefOffsetter>();
        //AddPasses<SDSLOpRemover>();
    }

    public void AddPasses<T>()
        where T : IPostProcessorPassCreator
    {
        AdditionalPasses.Add(T.Create(Buffer));
    }


    public void Apply()
    {
        foreach(var pass in AdditionalPasses)
        {
            pass.Apply();
        }
    }


    public void Dispose() => Buffer.Dispose();

    public static IPostProcessorPassCreator Create(SpirvBuffer buffer) => (IPostProcessorPassCreator)new PostProcessor(buffer);


}