using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;


public class Disassembler
{
    Dictionary<int, string> debugNames;

    public Disassembler()
    {
        debugNames = new();
    }

    public string Disassemble(Memory<int> memory)
    {
        var words = MagicNumber == memory.Span[0] ?
            memory.Span[5..] : memory.Span;
        
        var wbuff = new WordBuffer(words);
        return Disassemble(wbuff);
    }
    public string Disassemble(WordBuffer wbuff)
    {
        var dis = new DisWriter(new SpirvReader(wbuff.Memory, wbuff.Span[0] == MagicNumber).ComputeBound());
        
        foreach(var e in wbuff)
        {
            dis.Append(e.ResultId != null ? new IdResult(e.ResultId.Value) : null);
            dis.AppendLiteral(Enum.GetName(e.OpCode) ?? "Op.OpNop");
            foreach(var o in e)
            {
                if(o.Kind == OperandKind.IdRef)
                    dis.Append(o.To<IdRef>());
                else if(o.Kind == OperandKind.PackedVectorFormat)
                    dis.Append(o.ToEnum<PackedVectorFormat>());
                else if(o.Kind == OperandKind.ImageOperands)
                    dis.Append(o.ToEnum<ImageOperandsMask>());
                else if(o.Kind == OperandKind.FPFastMathMode)
                    dis.Append(o.ToEnum<FPFastMathModeMask>());
                else if(o.Kind == OperandKind.SelectionControl)
                    dis.Append(o.ToEnum<SelectionControlMask>());
                else if(o.Kind == OperandKind.LoopControl)
                    dis.Append(o.ToEnum<LoopControlMask>());
                else if(o.Kind == OperandKind.FunctionControl)
                    dis.Append(o.ToEnum<FunctionControlMask>());
                else if(o.Kind == OperandKind.MemorySemantics)
                    dis.Append(o.ToEnum<MemorySemanticsMask>());
                else if(o.Kind == OperandKind.MemoryAccess)
                    dis.Append(o.ToEnum<MemoryAccessMask>());
                else if(o.Kind == OperandKind.KernelProfilingInfo)
                    dis.Append(o.ToEnum<KernelProfilingInfoMask>());
                else if(o.Kind == OperandKind.RayFlags)
                    dis.Append(o.ToEnum<RayFlagsMask>());
                else if(o.Kind == OperandKind.FragmentShadingRate)
                    dis.Append(o.ToEnum<FragmentShadingRateMask>());
                else if(o.Kind == OperandKind.SourceLanguage)
                    dis.Append(o.ToEnum<SourceLanguage>());
                else if(o.Kind == OperandKind.ExecutionModel)
                    dis.Append(o.ToEnum<ExecutionModel>());
                else if(o.Kind == OperandKind.AddressingModel)
                    dis.Append(o.ToEnum<AddressingModel>());
                else if(o.Kind == OperandKind.MemoryModel)
                    dis.Append(o.ToEnum<MemoryModel>());
                else if(o.Kind == OperandKind.ExecutionMode)
                    dis.Append(o.ToEnum<ExecutionMode>());
                else if(o.Kind == OperandKind.StorageClass)
                    dis.Append(o.ToEnum<StorageClass>());
                else if(o.Kind == OperandKind.Dim)
                    dis.Append(o.ToEnum<Dim>());
                else if(o.Kind == OperandKind.SamplerAddressingMode)
                    dis.Append(o.ToEnum<SamplerAddressingMode>());
                else if(o.Kind == OperandKind.SamplerFilterMode)
                    dis.Append(o.ToEnum<SamplerFilterMode>());
                else if(o.Kind == OperandKind.ImageFormat)
                    dis.Append(o.ToEnum<ImageFormat>());
                else if(o.Kind == OperandKind.ImageChannelOrder)
                    dis.Append(o.ToEnum<ImageChannelOrder>());
                else if(o.Kind == OperandKind.ImageChannelDataType)
                    dis.Append(o.ToEnum<ImageChannelDataType>());
                else if(o.Kind == OperandKind.FPRoundingMode)
                    dis.Append(o.ToEnum<FPRoundingMode>());
                else if(o.Kind == OperandKind.LinkageType)
                    dis.Append(o.ToEnum<LinkageType>());
                else if(o.Kind == OperandKind.AccessQualifier)
                    dis.Append(o.ToEnum<AccessQualifier>());
                else if(o.Kind == OperandKind.FunctionParameterAttribute)
                    dis.Append(o.ToEnum<FunctionParameterAttribute>());
                else if(o.Kind == OperandKind.Decoration)
                    dis.Append(o.ToEnum<Decoration>());
                else if(o.Kind == OperandKind.BuiltIn)
                    dis.Append(o.ToEnum<BuiltIn>());
                else if(o.Kind == OperandKind.Scope)
                    dis.Append(o.ToEnum<Scope>());
                else if(o.Kind == OperandKind.GroupOperation)
                    dis.Append(o.ToEnum<GroupOperation>());
                else if(o.Kind == OperandKind.KernelEnqueueFlags)
                    dis.Append(o.ToEnum<KernelEnqueueFlags>());
                else if(o.Kind == OperandKind.Capability)
                    dis.Append(o.ToEnum<Capability>());
                else if(o.Kind == OperandKind.RayQueryIntersection)
                    dis.Append(o.ToEnum<RayQueryIntersection>());
                else if(o.Kind == OperandKind.RayQueryCommittedIntersectionType)
                    dis.Append(o.ToEnum<RayQueryCommittedIntersectionType>());
                else if(o.Kind == OperandKind.RayQueryCandidateIntersectionType)
                    dis.Append(o.ToEnum<RayQueryCandidateIntersectionType>());
                else if(o.Kind == OperandKind.IdResultType)
                    dis.AppendInt(o.To<IdResultType>());
                else if(o.Kind == OperandKind.IdMemorySemantics)
                    dis.AppendInt(o.To<IdMemorySemantics>());
                else if(o.Kind == OperandKind.IdScope)
                    dis.AppendInt(o.To<IdScope>());
                else if(o.Kind == OperandKind.IdRef)
                    dis.Append(o.To<IdRef>());
                else if(o.Kind == OperandKind.LiteralInteger)
                    dis.AppendLiteral(o.To<LiteralInteger>());
                else if(o.Kind == OperandKind.LiteralString)
                    dis.AppendLiteral(o.To<LiteralString>(),true);
                else if(o.Kind == OperandKind.PairLiteralIntegerIdRef)
                    dis.Append(o.To<PairLiteralIntegerIdRef>());
                else if(o.Kind == OperandKind.PairIdRefLiteralInteger)
                    dis.Append(o.To<PairIdRefLiteralInteger>());
                else if(o.Kind == OperandKind.PairIdRefIdRef)
                    dis.Append(o.To<PairIdRefIdRef>());
            }
            dis.AppendLine();
        }
        return dis.ToString();
    }
}

// file static class WordsExtension
// {
//     public static OrderedEnumerator GetEnumerator(this Span<int> words)
//     {

//     }
// }