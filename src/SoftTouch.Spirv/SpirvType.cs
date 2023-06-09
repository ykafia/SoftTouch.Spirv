namespace SoftTouch.Spirv;



public enum SpvTypeVariant
{
    Void,
    Scalar,
    Vector,
    Matrix,
    Array,
    Structure
}

public enum VariableScope
{
    Input,
    Output,
    Uniform,
    Function
}

public record struct SpirvType(string Name, SpvTypeVariant TypeVariant, VariableScope Scope);