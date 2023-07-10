namespace SoftTouch.Spirv;


public record struct MixinGraphInstructions(MixinGraph Graph)
{
    public MixinInstructionEnumerator GetEnumerator() => new(Graph);
}


public struct MixinGraph
{
    public List<string> Names { get; private set; }
    internal MixinList DistinctNames { get; private set; }

    public MixinGraphInstructions Instructions => new(this);

    public int Count => GetCount();

    public Mixin this[int index]
    {
        get
        {
            if(index >= Count)
                throw new IndexOutOfRangeException();
            var enumerator = GetEnumerator();
            for (int i = 0; enumerator.MoveNext() && i < index; i++){}
            return enumerator.Current;
        }
    }

    public MixinGraph()
    {
        Names = new();
        DistinctNames = new();
    }
    public MixinGraph(List<string> names)
    {
        Names = names;
        DistinctNames = new();
    }

    public static implicit operator MixinGraph(List<string> names) => new(names);

    public MixinEnumerator GetEnumerator() => new(Names);

    public void Add(string mixin)
    {
        Names.Add(mixin);
        // RebuildGraph();
    }
    public void Remove(string mixin)
    {
        Names.Remove(mixin);
        // RebuildGraph();
    }

    // public void RebuildGraph()
    // {
    //     DistinctNames.Clear();
    //     foreach (var m in Names)
    //     {
    //         FillMixinHashSet(m);
    //     }
    // }

    // void FillMixinHashSet(string name)
    // {
    //     foreach (string m in MixinSourceProvider.GetParentNames(name))
    //         FillMixinHashSet(m);
    //     DistinctNames.Add(name);
    // }
    int GetCount()
    {
        int count = 0;
        var e = GetEnumerator();
        while (e.MoveNext())
            count += 1;
        return count;
    }
}