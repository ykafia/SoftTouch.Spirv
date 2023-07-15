using System.Numerics;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;


public struct ParentList
{
    MemoryOwner<string> _owner;
    public int Length { get; private set; }

    public readonly string this[int index] => _owner.Span[index];

    public ParentList()
    {
        _owner = MemoryOwner<string>.Allocate(2);
    }

    public ParentList(int size)
    {
        _owner = MemoryOwner<string>.Allocate(size);
    }

    public void Add(string name)
    {
        if(_owner.Length <= Length +1)
            Expand();
        _owner.Span[Length] = name;
        Length += 1;
    }

    internal void Expand()
    {
        var r = MemoryOwner<string>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)Length + 1),AllocationMode.Clear);
        _owner.Span.CopyTo(r.Span);
        _owner.Dispose();
        _owner = r;
    }

    public readonly Enumerator GetEnumerator() => new(this);

    public ref struct Enumerator
    {
        readonly ParentList parentList;
        int index;
        public string Current => parentList[index];

        public Enumerator(ParentList parentList)
        {
            this.parentList = parentList;
        }

        public bool MoveNext()
        {
            return ++index < parentList.Length;
        }
    }

    public readonly void Dispose() => _owner.Dispose();
}


public ref struct MixinParents
{
    Mixin mixin;
    public MixinParents(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public FilteredEnumerator<SortedWordBuffer> GetEnumerator() => new(mixin.Buffer, SDSLOp.OpSDSLMixinInherit);

    public int GetCount()
    {
        var result = 0;
        foreach (var p in this)
            result += 1;
        return result;
    }
    public ParentList ToList()
    {
        var count = GetCount();
        if (GetCount() == 0)
            return new();
        var result = new ParentList(count);
        foreach (var e in this)
        {
            foreach (var name in e)
            {
                result.Add(name.To<LiteralString>().Value);
            }
        }
        return result;
    }
    public MixinGraph ToGraph()
    {
        return new(ToList());
    }
}
