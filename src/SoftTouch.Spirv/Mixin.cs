using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv;


public partial class Mixin : BaseMixin
{
    public List<SpirvType> Types;
    public List<string> Parents;
    public List<VariableData> Variables;
    public List<MethodData> Methods;

    public Mixin(string name, List<string>? parents = null) : base(name)
    {
        Parents = parents ?? new();
        Variables = new();
        Methods = new();
        Types = new();
    }

    public void Add(Mixin mixin)
    {
        
    }
}