namespace SoftTouch.Spirv;


public class MixinSourceProvider
{

    internal static MixinSourceProvider Instance { get; } = new();

    Dictionary<string, Mixin> Mixins;
    Dictionary<string, List<string>> MixinGraph;


    private MixinSourceProvider()
    {
        Mixins = new();
        MixinGraph = new();
    }

    public static void Register(Mixin mixin)
    {
        Instance.Mixins.Add(mixin.Name, mixin);
        Instance.MixinGraph.Add(mixin.Name, mixin.Parents);
    }
    public static Mixin Get(string name)
    {
        return Instance.Mixins[name];
    }
    public static List<string> GetParentNames(string name)
    {
        return Instance.MixinGraph[name];
    }
}