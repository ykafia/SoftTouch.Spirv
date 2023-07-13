using CommunityToolkit.HighPerformance;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;


namespace SoftTouch.Spirv;


public ref struct MixinInstructionEnumerator
{
    MixinGraph Mixins { get; init; }

    InstructionEnumerator lastEnumerator;
    int lastMixin;
    int boundOffset;

    public MixinInstructionEnumerator(MixinGraph mixins)
    {
        Mixins = mixins;
        lastMixin = -1;
        boundOffset = -1;
    }
    public RefInstruction Current => lastEnumerator.Current with { IdRefOffset = boundOffset };
    public bool MoveNext()
    {
        var count = Mixins.Count;
        if (count == 0)
            return false;
        else if (lastMixin == -1)
        {
            lastMixin = 0;
            boundOffset = 0;
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
                    lastMixin += 1;
                    if (lastMixin < count)
                        lastEnumerator = Mixins[lastMixin].Buffer.GetEnumerator();
                }
            }
            return false;
        }

    }
}
