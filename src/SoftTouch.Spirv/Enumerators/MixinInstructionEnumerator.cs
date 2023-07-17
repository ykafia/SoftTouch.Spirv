using CommunityToolkit.HighPerformance;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;


namespace SoftTouch.Spirv;


public ref struct MixinInstructionEnumerator
{
    MixinGraph Mixins { get; init; }

    InstructionEnumerator lastEnumerator;
    int lastMixin;
    public int BoundOffset { get; set; }

    public MixinInstructionEnumerator(MixinGraph mixins)
    {
        Mixins = mixins;
        lastMixin = -1;
        BoundOffset = -1;
    }
    public RefInstruction Current => lastEnumerator.Current with { IdRefOffset = BoundOffset };
    public bool MoveNext()
    {
        var count = Mixins.Count;
        if (count == 0)
            return false;
        else if (lastMixin == -1)
        {
            lastMixin = 0;
            BoundOffset = 0;
            lastEnumerator = Mixins[lastMixin].Buffer.GetEnumerator();
            return lastEnumerator.MoveNext();
        }
        else
        {
            while (lastMixin < count)
            {
                if (lastEnumerator.MoveNext())
                    return true;
                else
                {
                    BoundOffset += Mixins[lastMixin].Bound;
                    lastMixin += 1;
                    if (lastMixin < count)
                        lastEnumerator = Mixins[lastMixin].Buffer.GetEnumerator();
                }
            }
            return false;
        }

    }
}
