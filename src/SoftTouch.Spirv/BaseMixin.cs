using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


// TODO: reflect on types, variables and methods
public class BaseMixin
{
    public string Name { get; set; }
    internal WordBuffer Buffer { get; }

    public BaseMixin(string name, WordBuffer? buffer = null)
    {
        Name = name;
        Buffer = buffer ?? new();
    }
}
