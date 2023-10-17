using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public ref partial struct FunctionBuilder
{

    public readonly Instruction FindVariable(string name)
    {
        if (mixer.LocalVariables.TryGet(name, out var local))
            return local;
        else if (mixer.GlobalVariables.TryGet(name, out var global))
            return global;
        else
            throw new Exception($"Variable {name} was not found");
    }

    public readonly Instruction Constant<T>(T value)
        where T : struct
    {
        return mixer.CreateConstant(value).Instruction;
    }

    public readonly Instruction Load(string name)
    {
        var variable = FindVariable(name);
        var rtype = Instruction.Empty;
        foreach(var i in mixer.Buffer.Declarations.UnorderedInstructions)
        {
            if(i.ResultId != null && i.ResultId == variable.ResultType && i.OpCode != SDSLOp.OpTypePointer)
            {
                rtype = i;
                break;
            }
            else if(i.ResultId != null && i.ResultId == variable.ResultType && i.OpCode == SDSLOp.OpTypePointer)
            {
                var toFind = i.GetOperand<IdRef>("type");
                foreach(var j in mixer.Buffer.Declarations.UnorderedInstructions)
                {
                    if (j.ResultId != null && j.ResultId == toFind && j.OpCode != SDSLOp.OpTypePointer)
                    {
                        rtype = j;
                        break;
                    }
                }
                break;
            }
        }
        if(rtype.IsEmpty)
            throw new Exception("type of variable was not found");
        
        return mixer.Buffer.AddOpLoad(rtype, variable, null);
    }
    public readonly Instruction FindById(int id)
    {
        foreach (var i in mixer.Buffer.Instructions)
            if (i.ResultId == id)
                return i;
        return Instruction.Empty;
    }

    public readonly Instruction Add(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "byte" or "sbyte"
            or "ushort" or "short"
            or "uint" or "int"
            or "long" or "ulong" => mixer.Buffer.AddOpIAdd(rtype, a, b),
            "half" or "float" or "double" => mixer.Buffer.AddOpFAdd(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }
    public readonly Instruction Sub(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "byte" or "sbyte"
            or "ushort" or "short"
            or "uint" or "int"
            or "long" or "ulong" => mixer.Buffer.AddOpISub(rtype, a, b),
            "half" or "float" or "double" => mixer.Buffer.AddOpFSub(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }
    public readonly Instruction Div(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "sbyte"
            or "short"
            or "int"
            or "long" => mixer.Buffer.AddOpSDiv(rtype, a, b),
            "byte"
            or "ushort"
            or "uint"
            or "ulong" => mixer.Buffer.AddOpUDiv(rtype, a, b),
            "half" or "float" or "double" => mixer.Buffer.AddOpFDiv(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }
    public readonly Instruction Mul(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "byte" or "sbyte"
            or "ushort" or "short"
            or "uint" or "int"
            or "long" or "ulong" => mixer.Buffer.AddOpIMul(rtype, a, b),
            "half" or "float" or "double" => mixer.Buffer.AddOpFMul(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }

    public readonly Instruction Mod(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "sbyte"
            or "short"
            or "int"
            or "long" => mixer.Buffer.AddOpSMod(rtype, a, b),
            "byte"
            or "ushort"
            or "uint"
            or "ulong" => mixer.Buffer.AddOpUMod(rtype, a, b),
            "half" or "float" or "double" => mixer.Buffer.AddOpFMod(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }
    public readonly Instruction Rem(string resultType, IdRef a, IdRef b)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return resultType switch
        {
            "sbyte"
            or "short"
            or "int"
            or "long" => mixer.Buffer.AddOpSRem(rtype, a, b),
            "byte"
            or "ushort"
            or "uint"
            or "ulong" => throw new Exception("Cannot compute remainder of unsigned number"),
            "half" or "float" or "double" => mixer.Buffer.AddOpFRem(rtype, a, b),
            _ => throw new NotImplementedException($"{resultType} not yet implemented for this")
        };
    }
    public readonly Instruction VectorTimesScalar(string resultType, IdRef vector, IdRef scalar)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpVectorTimesScalar(rtype, vector, scalar);
    }
    public readonly Instruction VectorTimesMatrix(string resultType, IdRef vector, IdRef matrix)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpVectorTimesMatrix(rtype, vector, matrix);
    }
    public readonly Instruction MatrixTimesScalar(string resultType, IdRef matrix, IdRef scalar)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpMatrixTimesScalar(rtype, matrix, scalar);
    }
    public readonly Instruction MatrixTimesVector(string resultType, IdRef matrix, IdRef vector)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpMatrixTimesVector(rtype, matrix, vector);
    }
    public readonly Instruction MatrixTimesMatrix(string resultType, IdRef matrix, IdRef matrix2)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpMatrixTimesMatrix(rtype, matrix, matrix2);
    }

    public readonly Instruction OuterProduct(string resultType, IdRef vector1, IdRef vector2)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpOuterProduct(rtype, vector1, vector2);
    }
    public readonly Instruction Dot(string resultType, IdRef vector1, IdRef vector2)
    {
        var rtype = mixer.GetOrCreateBaseType(resultType.AsMemory());
        return mixer.Buffer.AddOpDot(rtype, vector1, vector2);
    }


}