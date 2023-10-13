using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;

namespace SoftTouch.Spirv.PostProcessing;

/// <summary>
/// Nano pass merger/optimizer/compiler
/// </summary>
public static class PostProcessor
{
    public static SpirvBuffer Process(string mixinName)
    {
        var buffer = new MultiBuffer();
        var mixin = MixinSourceProvider.Get(mixinName);
        #if DEBUG
        Console.WriteLine($"Processing {mixinName}");
        #endif
        foreach (var m in mixin.Parents)
        {
            buffer.Declarations.Add(m.Declarations.InstructionSpan);
            foreach (var (nf,f) in buffer.Functions)
                buffer.Functions.Add(nf, f);
        }
        //buffer.Add(mixin.Declarations);

        Apply(buffer);

        return buffer;
    }

    static void Apply(SpirvBuffer buffer)
    {
        Apply<IdRefOffsetter>(buffer);
        Apply<MemoryModelDuplicatesRemover>(buffer);
        Apply<TypeDuplicateRemover>(buffer);
        Apply<BoundReducer>(buffer);
        Apply<SDSLOpRemover>(buffer);
        Apply<MixinMerger>(buffer);
        Apply<FunctionVariableOrderer>(buffer);
    }

    static void Apply<T>(SpirvBuffer buffer)
        where T : struct, INanoPass
    {
        var p = new T();
        p.Apply(buffer);
    }
}