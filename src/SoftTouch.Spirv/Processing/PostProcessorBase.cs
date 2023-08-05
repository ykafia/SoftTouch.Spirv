using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv.PostProcessor;


public abstract class PostProcessorBase
{
    public SpirvBuffer Buffer { get; init; }

    public PostProcessorBase(string mixinName)
    {
        Buffer = new();
        // Words = new BufferBase<int>();
        var mixin = MixinSourceProvider.Get(mixinName);
        foreach (var m in mixin.Parents.ToGraph())
        {
            Buffer.Add(mixin.Buffer);
        }
        Buffer.Add(mixin.Buffer);
        Console.WriteLine(new Disassembler().Disassemble(Buffer));
    }
}