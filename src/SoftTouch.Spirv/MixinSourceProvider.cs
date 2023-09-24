namespace SoftTouch.Spirv;

/// <summary>
/// Mixin buffer repository
/// </summary>
public class MixinSourceProvider
{

    internal static MixinSourceProvider Instance { get; } = new();

    readonly Dictionary<string, Mixin> Mixins;
    readonly Dictionary<string, MixinGraph> MixinGraph;

    private MixinSourceProvider()
    {
        Mixins = new();
        MixinGraph = new();
    }

    public static void Register(Mixin mixin)
    {
        Instance.Mixins.Add(mixin.Name, mixin);
        Instance.MixinGraph.Add(mixin.Name, mixin.Parents.ToGraph());
    }
    public static Mixin Get(string name)
    {
        return Instance.Mixins[name];
    }
    public static MixinGraph GetMixinGraph(string name)
    {
        return Instance.MixinGraph[name];
    }
    public static bool TryGetMixinGraph(string name, out MixinGraph? graph)
    {
        return Instance.MixinGraph.TryGetValue(name, out graph);
    }
}