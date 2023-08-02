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
        Mixin mixin;
        MixinGraph graph;
        bool graphFinished;

        MixinInstructionEnumerator enumerator;
        InstructionEnumerator self;


        public MixinRefInstruction Current => graphFinished ? new(mixin.Name, self.Current) : enumerator.Current;

        public Enumerator(Mixin mixin)
        {
            this.mixin = mixin;
            graphFinished = false;
            graph = MixinSourceProvider.GetMixinGraph(mixin.Name);
            enumerator = graph.Instructions.GetEnumerator();
            self = mixin.Instructions.GetEnumerator();
        }

        public bool MoveNext()
        {
            if (enumerator.MoveNext())
                return true;
            else
            {
                if (!graphFinished)
                {
                    self.ResultIdReplacement = -1;
                    graphFinished = true;
                }
                return self.MoveNext();
            }
        }
    }
}