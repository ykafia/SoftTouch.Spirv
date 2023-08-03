using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;


public ref struct MixinInstructions
{
    Mixin mixin;
    public MixinInstructions(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public RefInstruction this[int index]
    {
        get
        {
            var count = mixin.Buffer.Length;
            if(index >= count) return RefInstruction.Empty;
            var enumerator = GetEnumerator();
            for(int i = 0; enumerator.MoveNext() && i < index; i++);
            return enumerator.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => mixin.Buffer.GetEnumerator();
}