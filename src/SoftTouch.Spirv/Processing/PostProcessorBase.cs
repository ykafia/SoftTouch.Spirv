using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv.PostProcessor;


public abstract class PostProcessorBase
{
    public SortedWordBuffer Words {get; init;}

    public PostProcessorBase(string mixinName)
    {
        // Words = new BufferBase<int>();
        var mixin = MixinSourceProvider.Get(mixinName);
        foreach (var i in mixin.FullInstructions)
        {
            // Words.Insert(i.Instruction);
        }
        
    }
}