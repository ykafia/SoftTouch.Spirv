using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;

public ref struct MixinInstructionEnumerator
{

    MixinGraph Mixins { get; init; }

    int currentGroup;
    int lastMixin;
    int lastIndex;

    public MixinInstructionEnumerator(MixinGraph mixins)
    {
        Mixins = mixins;
        currentGroup = 0;
        lastIndex = -1;
        lastMixin = -1;
    }
    public RefInstruction Current => Mixins[lastMixin].Instructions[lastIndex];
    public bool MoveNext()
    {
        if (lastMixin == -1)
        {
            lastMixin = 0;
            lastIndex = 0;
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
                    while (lastIndex + offset < count && Mixins[lastMixin].Instructions[lastIndex + offset].IsEmpty)
                    {
                        offset += 1;
                    }
                    if(!Mixins[lastMixin].Instructions[lastIndex + offset].IsEmpty)
                    {
                        lastIndex += offset;
                        return true;
                    }
                    else 
                        lastMixin += 1;
                }
                currentGroup += 1;
            }
            return false;
        }

    }
}
