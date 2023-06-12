namespace SoftTouch.Spirv;


public class Mixin : BaseMixin
{
    public List<Mixin> Mixins;
    public List<VariableData> Variables;
    public List<MethodData> Methods;

    public Mixin()
    {
        Mixins = new();
        Variables = new();
        Methods = new();
    }
}