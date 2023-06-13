using SoftTouch.Spirv.Core;

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

public record struct SpirvType(string Name, SpvTypeVariant TypeVariant, Instruction instruction);