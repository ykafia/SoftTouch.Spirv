using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;



public ref struct MixinParents
{
    Mixin mixin;
    public MixinParents(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public ParentEnumerator GetEnumerator() => new(mixin.Instructions.GetEnumerator());

    public ref struct ParentEnumerator
    {
        InstructionEnumerator enumerator;

        public ParentEnumerator(InstructionEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        public RefInstruction Current => enumerator.Current;
        
        public bool MoveNext()
        {
            var result = true;
            while(enumerator.Current.OpCode != SDSLOp.OpSDSLMixinInherit)
            {
                result = enumerator.MoveNext();
            }
            return result;
        }
    }

    public List<string> ToList()
    {
        var result = new List<string>(4);
        foreach(var e in this)
        {
            foreach(var name in e)
            {
                result.Add(name.To<LiteralString>().Value);
            }
        }
        return result;
    }
}
