using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv.Core;

using static Spv.Specification;

public struct Instruction
{
    WordBuffer Buffer { get; init; }
    public int Index { get; init; }
    public Op OpCode => Buffer[Index].OpCode;
    public int? ResultId => Buffer[Index].ResultId;
    public int? ResultType => Buffer[Index].ResultType;
    public Span<int> Operands => Buffer[Index].Operands;

    public Instruction(WordBuffer buffer, int index)
    {
        Buffer = buffer;
        Index = index;
    }

    public bool Set<T>(string propertyName, scoped in T value)
    {
        var info = InstructionInfo.GetInfo(OpCode);
        foreach (var e in info)
        {
            var kind = e.Kind;
        }

        if (value is string s)
            return false;
        if (value is int i)
            return false;
        return false;

    }
    public T Get<T>(string propertyName)
    {
        var info = InstructionInfo.GetInfo(OpCode);
        LogicalOperand? operand = null;
        foreach (var e in info)
        {
            if (e.Name == propertyName)
                operand = e;
        }
        if (operand is null)
            throw new Exception("Property name doesn't exist for " + OpCode.ToString());
        else if (operand.Value.Kind != null && CanBeCastTo<T>(operand.Value.Kind.Value))
        {
            // TODO : improve reflection
            info.IndexOf(operand.Value);
        }
        throw new NotImplementedException();
    }

    public static bool CanBeCastTo<T>(OperandKind kind)
    {
        if (kind == OperandKind.PackedVectorFormat && typeof(T) == typeof(PackedVectorFormat))
            return true;
        else if (kind == OperandKind.ImageOperands && typeof(T) == typeof(ImageOperandsMask))
            return true;
        else if (kind == OperandKind.FPFastMathMode && typeof(T) == typeof(FPFastMathModeMask))
            return true;
        else if (kind == OperandKind.SelectionControl && typeof(T) == typeof(SelectionControlMask))
            return true;
        else if (kind == OperandKind.LoopControl && typeof(T) == typeof(LoopControlMask))
            return true;
        else if (kind == OperandKind.FunctionControl && typeof(T) == typeof(FunctionControlMask))
            return true;
        else if (kind == OperandKind.MemorySemantics && typeof(T) == typeof(MemorySemanticsMask))
            return true;
        else if (kind == OperandKind.MemoryAccess && typeof(T) == typeof(MemoryAccessMask))
            return true;
        else if (kind == OperandKind.KernelProfilingInfo && typeof(T) == typeof(KernelProfilingInfoMask))
            return true;
        else if (kind == OperandKind.RayFlags && typeof(T) == typeof(RayFlagsMask))
            return true;
        else if (kind == OperandKind.FragmentShadingRate && typeof(T) == typeof(FragmentShadingRateMask))
            return true;
        else if (kind == OperandKind.SourceLanguage && typeof(T) == typeof(SourceLanguage))
            return true;
        else if (kind == OperandKind.ExecutionModel && typeof(T) == typeof(ExecutionModel))
            return true;
        else if (kind == OperandKind.AddressingModel && typeof(T) == typeof(AddressingModel))
            return true;
        else if (kind == OperandKind.MemoryModel && typeof(T) == typeof(MemoryModel))
            return true;
        else if (kind == OperandKind.ExecutionMode && typeof(T) == typeof(ExecutionModel))
            return true;
        else if (kind == OperandKind.StorageClass && typeof(T) == typeof(StorageClass))
            return true;
        else if (kind == OperandKind.Dim && typeof(T) == typeof(Dim))
            return true;
        else if (kind == OperandKind.SamplerAddressingMode && typeof(T) == typeof(SamplerAddressingMode))
            return true;
        else if (kind == OperandKind.SamplerFilterMode && typeof(T) == typeof(SamplerFilterMode))
            return true;
        else if (kind == OperandKind.ImageFormat && typeof(T) == typeof(ImageFormat))
            return true;
        else if (kind == OperandKind.ImageChannelOrder && typeof(T) == typeof(ImageChannelOrder))
            return true;
        else if (kind == OperandKind.ImageChannelDataType && typeof(T) == typeof(ImageChannelDataType))
            return true;
        else if (kind == OperandKind.FPRoundingMode && typeof(T) == typeof(FPRoundingMode))
            return true;
        else if (kind == OperandKind.LinkageType && typeof(T) == typeof(LinkageType))
            return true;
        else if (kind == OperandKind.AccessQualifier && typeof(T) == typeof(AccessQualifier))
            return true;
        else if (kind == OperandKind.FunctionParameterAttribute && typeof(T) == typeof(FunctionParameterAttribute))
            return true;
        else if (kind == OperandKind.Decoration && typeof(T) == typeof(Decoration))
            return true;
        else if (kind == OperandKind.BuiltIn && typeof(T) == typeof(BuiltIn))
            return true;
        else if (kind == OperandKind.Scope && typeof(T) == typeof(Scope))
            return true;
        else if (kind == OperandKind.GroupOperation && typeof(T) == typeof(GroupOperation))
            return true;
        else if (kind == OperandKind.KernelEnqueueFlags && typeof(T) == typeof(KernelEnqueueFlags))
            return true;
        else if (kind == OperandKind.Capability && typeof(T) == typeof(Capability))
            return true;
        else if (kind == OperandKind.RayQueryIntersection && typeof(T) == typeof(RayQueryIntersection))
            return true;
        else if (kind == OperandKind.RayQueryCommittedIntersectionType && typeof(T) == typeof(RayQueryCommittedIntersectionType))
            return true;
        else if (kind == OperandKind.RayQueryCandidateIntersectionType && typeof(T) == typeof(RayQueryCandidateIntersectionType))
            return true;
        else if (kind == OperandKind.IdResultType && typeof(T) == typeof(IdResultType))
            return true;
        else if (kind == OperandKind.IdResult && typeof(T) == typeof(IdResult))
            return true;
        else if (kind == OperandKind.IdMemorySemantics && typeof(T) == typeof(IdMemorySemantics))
            return true;
        else if (kind == OperandKind.IdScope && typeof(T) == typeof(IdScope))
            return true;
        else if (kind == OperandKind.IdRef && typeof(T) == typeof(IdRef))
            return true;
        else if (kind == OperandKind.LiteralInteger && typeof(T) == typeof(LiteralInteger))
            return true;
        else if (kind == OperandKind.LiteralString && typeof(T) == typeof(LiteralString))
            return true;
        else if (kind == OperandKind.LiteralContextDependentNumber && typeof(T) == typeof(int))
            return true;
        else if (kind == OperandKind.LiteralExtInstInteger && typeof(T) == typeof(int))
            return true;
        else if (kind == OperandKind.LiteralSpecConstantOpInteger && typeof(T) == typeof(int))
            return true;
        else if (kind == OperandKind.PairLiteralIntegerIdRef && typeof(T) == typeof(ValueTuple<int, int>))
            return true;
        else if (kind == OperandKind.PairIdRefLiteralInteger && typeof(T) == typeof(ValueTuple<int, int>))
            return true;
        else if (kind == OperandKind.PairIdRefIdRef && typeof(T) == typeof(ValueTuple<int, int>))
            return true;
        else return false;
    }

}

internal static class SpirvOperandExtensions
{
    public static T Parse<T>(this ref Span<int> v)
    {
        if (v.Length == 1 && typeof(T).IsEnum)
            return Unsafe.As<int, T>(ref v[0]);
        if (v.Length == 1 && typeof(T) == typeof(int))
            return Unsafe.As<int, T>(ref v[0]);
        if (typeof(T) == typeof(string))
        {
            var s = LiteralString.Parse(v);
            return Unsafe.As<string, T>(ref s);
        }
        else
            throw new ArgumentException("Cannot parse operand");
    }


}