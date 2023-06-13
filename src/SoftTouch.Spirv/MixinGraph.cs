namespace SoftTouch.Spirv;

public struct MixinGraph
{
    public List<string> Names { get; private set; }
    internal MixinList DistinctNames { get; private set; }

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

    public MixinEnumerator GetEnumerator() => new(DistinctNames.AsList());
    
    public void Add(string mixin)
    {
        Names.Add(mixin);
        RebuildGraph();
    }
    public void Remove(string mixin)
    {
        Names.Remove(mixin);
        RebuildGraph();
    }

    public void RebuildGraph()
    {
        DistinctNames.Clear();
        foreach (var m in Names)
        {
            FillMixinHashSet(m);
        }
    }

    void FillMixinHashSet(string name)
    {
        foreach (string m in MixinSourceProvider.GetParentNames(name))
            FillMixinHashSet(m);
        DistinctNames.Add(name);
    }
}