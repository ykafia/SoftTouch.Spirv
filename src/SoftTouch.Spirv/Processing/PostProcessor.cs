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
        var parents = MixinSourceProvider.GetMixinGraph(mixinName);
        var bound = 0;
        foreach(var p in parents)
        {
            foreach(var i in p.Instructions)
                buffer.Duplicate(i.AsRef(), bound);
            bound += p.Bound;
        }
        foreach(var i in mixin.Instructions)
        {
            if(i.OpCode == SDSLOp.OpLabel)
            {
                var x = 0;
            }
            buffer.Duplicate(i.AsRef(), bound);
        }

        Apply(buffer);

        return new(buffer);
    }

    static void Apply(MultiBuffer buffer)
    {
        Apply<FunctionVariableOrderer>(buffer);
        Apply<TypeDuplicateRemover>(buffer);
        Apply<MemoryModelDuplicatesRemover>(buffer);
        Apply<BoundReducer>(buffer);
        Apply<SDSLOpRemover>(buffer);
        Apply<CompressBuffer>(buffer);
    }

    static void Apply<T>(MultiBuffer buffer)
        where T : struct, INanoPass
    {
        var p = new T();
        p.Apply(buffer);
    }
}