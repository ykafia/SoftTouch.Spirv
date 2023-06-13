namespace SoftTouch.Spirv;

public partial class Mixer
{
    public Mixin? Module { get; protected set; }
    public Mixer(){}

    public Mixer Create(string name)
    {
        Module = new(name);
        return this;
    }
    public Mixin? Get() => Module;

    public Mixer With(string mixin)
    {
        Module?.Parents.Add(mixin);
        return this;
    }
}