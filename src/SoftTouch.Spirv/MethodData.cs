namespace SoftTouch.Spirv;


public enum MethodKind
{
    Vertex,
    Fragment,
    Compute,
    Static
}

public record struct MethodData(string Name, MethodKind Kind, SpirvType ReturnType, VariableData[] Inputs);