namespace SoftTouch.Spirv;

public enum VariableScope
{
    Input,
    Output,
    Uniform,
    Function,
    Undefined
}
public record struct VariableData(string Name, SpirvType TypeInfo, VariableScope Scope);