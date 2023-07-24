using CommunityToolkit.HighPerformance;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;


namespace SoftTouch.Spirv;


public ref struct MixinInstructionEnumerator
{
    MixinGraph Mixins { get; init; }

    InstructionEnumerator lastEnumerator;
    int lastMixin;
    public int MixinResultId { get; set; }

    public MixinInstructionEnumerator(MixinGraph mixins)
    {
        Mixins = mixins;
        lastMixin = -1;
        MixinResultId = -1;
    }
    public RefInstruction Current => lastEnumerator.Current with { ResultIdReplacement = MixinResultId };
    public bool MoveNext()
    {
        var count = Mixins.Count;
        if (count == 0)
            return false;
        else if (lastMixin == -1)
        {
            lastMixin = 0;
            MixinResultId = 1;
            lastEnumerator = Mixins[lastMixin].Buffer.GetEnumerator();
            return lastEnumerator.MoveNext();
        }
        else
        {
            while (lastMixin < count)
            {
                var hadId = lastEnumerator.Current.ResultId != null;
                if (lastEnumerator.MoveNext())
                {
                    MixinResultId += hadId ? 1 : 0;
                    return true;
                }
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
