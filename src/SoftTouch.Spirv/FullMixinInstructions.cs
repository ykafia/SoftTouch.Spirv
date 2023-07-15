using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;


public struct FullMixinInstructions
{
    Mixin mixin;
    public FullMixinInstructions(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public Enumerator GetEnumerator() => new(mixin);
    
    public ref struct Enumerator
    {
        MixinGraph graph;
        bool graphFinished;

        MixinInstructionEnumerator enumerator;
        InstructionEnumerator self;

        public RefInstruction Current => graphFinished ? self.Current : enumerator.Current;

        public Enumerator(Mixin mixin)
        {
            graphFinished = false;
            graph = MixinSourceProvider.GetMixinGraph(mixin.Name);
            enumerator = graph.Instructions.GetEnumerator();
            self = mixin.Instructions.GetEnumerator();
        }

        public bool MoveNext()
        {
            if(enumerator.MoveNext())
                return true;
            else
            {
                graphFinished = false;
                return self.MoveNext();
            }
        }
    }
}