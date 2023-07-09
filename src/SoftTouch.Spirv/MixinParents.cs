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

    public FilteredEnumerator<SortedWordBuffer> GetEnumerator() => new(mixin.Buffer,SDSLOp.OpSDSLMixinInherit);

    public int GetCount()
    {
        var result = 0;
        foreach(var p in this)
            result += 1;
        return result;
    }
    public List<string> ToList()
    {
        if(GetCount() == 0)
            return new();
        var result = new List<string>(4);
        foreach (var e in this)
        {
            foreach (var name in e)
            {
                result.Add(name.To<LiteralString>().Value);
            }
        }
        return result;
    }
}
