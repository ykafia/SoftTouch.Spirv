using System.Runtime.CompilerServices;
using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;

public partial struct Mixin
{

    public string Name { get; set; }
    internal SortedWordBuffer Buffer { get; }

    public MixinGraph Parents;
    public List<SpirvType> Types;
    public List<VariableData> Variables;
    public List<MethodData> Methods;

    public Mixin(string name, SortedWordBuffer wordBuffer, List<string>? parents = null)
    {
        Name = name;
        Buffer = wordBuffer;
        Parents = parents ?? new();
        Variables = new();
        Methods = new();
        Types = new();
    }


    public MixinEnumerator GetEnumerator() => Parents.GetEnumerator();

    public void AddMixin(string mixin)
    {
        Parents.Add(mixin);
    }
}