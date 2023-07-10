using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;

public ref struct MixinInstructionEnumerator
{

    MixinGraph Mixins { get; init; }

    int currentGroup;
    int lastMixin;
    int lastIndex;
    int boundOffset;

    public MixinInstructionEnumerator(MixinGraph mixins)
    {
        Mixins = mixins;
        currentGroup = 0;
        lastIndex = -1;
        lastMixin = -1;
        boundOffset = -1;
    }
    public RefInstruction Current => Mixins[lastMixin].Instructions[lastIndex] with { IdRefOffset = boundOffset };
    public bool MoveNext()
    {
        if(Mixins.Count == 0)
            return false;
        if (lastMixin == -1)
        {
            lastMixin = 0;
            lastIndex = 0;
            boundOffset = 0;
            return true;
        }
        else
        {
            var count = Mixins.Count;
            // If the current mixin has no other
            while (currentGroup < 14)
            {
                while (lastMixin < count)
                {
                    var offset = 1;
                    var instruction = Mixins[lastMixin].Instructions[lastIndex + offset];
                    while (lastIndex + offset < count && (instruction.IsEmpty || instruction.OpCode >= SDSLOp.OpSDSLMixinName))
                    {
                        offset += 1;
                    }
                    if (!instruction.IsEmpty && InstructionInfo.GetGroupOrder(instruction) == currentGroup)
                    {
                        lastIndex += offset;
                        return true;
                    }
                    else
                    {
                        boundOffset += Mixins[lastMixin].Bound;
                        lastMixin += 1;
                        lastIndex = 0;
                    }
                }
                currentGroup += 1;
                lastMixin = 0;
                boundOffset = 0;
            }
            return false;
        }

    }
}
