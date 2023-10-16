using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;


public static class Disassembler
{

    public static string Disassemble(Span<int> memory)
    {
        var words = MagicNumber == memory[0] ?
            memory[5..] : memory;
        
        var wbuff = new WordBuffer(words);
        return Disassemble(wbuff);
    }

    public static string Disassemble(Memory<int> memory)
    {
        var words = MagicNumber == memory.Span[0] ?
            memory.Span[5..] : memory.Span;
        
        var wbuff = new WordBuffer(words);
        return Disassemble(wbuff);
    }
    public static string Disassemble(UnsortedWordBuffer wbuff)
    {
        var dis = new DisWriter(new SpirvReader(wbuff.Memory, wbuff.Span[0] == MagicNumber).ComputeBound());

        foreach (var e in wbuff)
        {
            dis.Append(e.ResultId != null ? new IdResult(e.ResultId.Value) : null);
            dis.AppendLiteral(Enum.GetName(e.OpCode) ?? "Op.OpNop");
            foreach (var o in e)
            {
                Append(o, dis);
            }
            dis.AppendLine();
        }
        return dis.ToString();
    }

    public static string Disassemble(SortedWordBuffer wbuff)
    {
        var dis = new DisWriter(new SpirvReader(wbuff.Memory, wbuff.Span[0] == MagicNumber).ComputeBound());

        foreach (var e in wbuff)
        {
            dis.Append(e.ResultId != null ? new IdResult(e.ResultId.Value) : null);
            dis.AppendLiteral(Enum.GetName(e.OpCode) ?? "Op.OpNop");
            foreach (var o in e)
            {
                Append(o, dis); 
            }
            dis.AppendLine();
        }
        return dis.ToString();
    }
    public static string Disassemble(SpirvBuffer wbuff)
    {
        var dis = new DisWriter(new SpirvReader(wbuff.InstructionMemory).ComputeBound());

        foreach (var e in wbuff)
        {
            dis.Append(e.ResultId != null ? new IdResult(e.ResultId.Value) : null);
            dis.AppendLiteral(Enum.GetName(e.OpCode) ?? "Op.OpNop");
            foreach (var o in e)
            {
                Append(o, dis);
            }
            dis.AppendLine();
        }
        return dis.ToString();
    }

    public static string Disassemble(WordBuffer wbuff)
    {
        var dis = new DisWriter(new SpirvReader(wbuff.Memory, wbuff.Span[0] == MagicNumber).ComputeBound());
        
        foreach(var e in wbuff)
        {
            dis.Append(e.ResultId != null ? new IdResult(e.ResultId.Value) : null);
            dis.AppendLiteral(Enum.GetName(e.OpCode) ?? "Op.OpNop");
            foreach(var o in e)
            {
                Append(o, dis);
            }
            dis.AppendLine();
        }
        return dis.ToString();
    }

    public static void Append(in SpvOperand o, DisWriter dis)
    {
        if (o.Quantifier != OperandQuantifier.ZeroOrMore)
        {
            if (o.Kind == OperandKind.IdRef)
                dis.Append(o.To<IdRef>());
            else if (o.Kind == OperandKind.PackedVectorFormat)
                dis.Append(o.ToEnum<PackedVectorFormat>());
            else if (o.Kind == OperandKind.ImageOperands)
                dis.Append(o.ToEnum<ImageOperandsMask>());
            else if (o.Kind == OperandKind.FPFastMathMode)
                dis.Append(o.ToEnum<FPFastMathModeMask>());
            else if (o.Kind == OperandKind.SelectionControl)
                dis.Append(o.ToEnum<SelectionControlMask>());
            else if (o.Kind == OperandKind.LoopControl)
                dis.Append(o.ToEnum<LoopControlMask>());
            else if (o.Kind == OperandKind.FunctionControl)
                dis.Append(o.ToEnum<FunctionControlMask>());
            else if (o.Kind == OperandKind.MemorySemantics)
                dis.Append(o.ToEnum<MemorySemanticsMask>());
            else if (o.Kind == OperandKind.MemoryAccess)
                dis.Append(o.ToEnum<MemoryAccessMask>());
            else if (o.Kind == OperandKind.KernelProfilingInfo)
                dis.Append(o.ToEnum<KernelProfilingInfoMask>());
            else if (o.Kind == OperandKind.RayFlags)
                dis.Append(o.ToEnum<RayFlagsMask>());
            else if (o.Kind == OperandKind.FragmentShadingRate)
                dis.Append(o.ToEnum<FragmentShadingRateMask>());
            else if (o.Kind == OperandKind.SourceLanguage)
                dis.Append(o.ToEnum<SourceLanguage>());
            else if (o.Kind == OperandKind.ExecutionModel)
                dis.Append(o.ToEnum<ExecutionModel>());
            else if (o.Kind == OperandKind.AddressingModel)
                dis.Append(o.ToEnum<AddressingModel>());
            else if (o.Kind == OperandKind.MemoryModel)
                dis.Append(o.ToEnum<MemoryModel>());
            else if (o.Kind == OperandKind.ExecutionMode)
                dis.Append(o.ToEnum<ExecutionMode>());
            else if (o.Kind == OperandKind.StorageClass)
                dis.Append(o.ToEnum<StorageClass>());
            else if (o.Kind == OperandKind.Dim)
                dis.Append(o.ToEnum<Dim>());
            else if (o.Kind == OperandKind.SamplerAddressingMode)
                dis.Append(o.ToEnum<SamplerAddressingMode>());
            else if (o.Kind == OperandKind.SamplerFilterMode)
                dis.Append(o.ToEnum<SamplerFilterMode>());
            else if (o.Kind == OperandKind.ImageFormat)
                dis.Append(o.ToEnum<ImageFormat>());
            else if (o.Kind == OperandKind.ImageChannelOrder)
                dis.Append(o.ToEnum<ImageChannelOrder>());
            else if (o.Kind == OperandKind.ImageChannelDataType)
                dis.Append(o.ToEnum<ImageChannelDataType>());
            else if (o.Kind == OperandKind.FPRoundingMode)
                dis.Append(o.ToEnum<FPRoundingMode>());
            else if (o.Kind == OperandKind.LinkageType)
                dis.Append(o.ToEnum<LinkageType>());
            else if (o.Kind == OperandKind.AccessQualifier)
                dis.Append(o.ToEnum<AccessQualifier>());
            else if (o.Kind == OperandKind.FunctionParameterAttribute)
                dis.Append(o.ToEnum<FunctionParameterAttribute>());
            else if (o.Kind == OperandKind.Decoration)
                dis.Append(o.ToEnum<Decoration>());
            else if (o.Kind == OperandKind.BuiltIn)
                dis.Append(o.ToEnum<BuiltIn>());
            else if (o.Kind == OperandKind.Scope)
                dis.Append(o.ToEnum<Scope>());
            else if (o.Kind == OperandKind.GroupOperation)
                dis.Append(o.ToEnum<GroupOperation>());
            else if (o.Kind == OperandKind.KernelEnqueueFlags)
                dis.Append(o.ToEnum<KernelEnqueueFlags>());
            else if (o.Kind == OperandKind.Capability)
                dis.Append(o.ToEnum<Capability>());
            else if (o.Kind == OperandKind.RayQueryIntersection)
                dis.Append(o.ToEnum<RayQueryIntersection>());
            else if (o.Kind == OperandKind.RayQueryCommittedIntersectionType)
                dis.Append(o.ToEnum<RayQueryCommittedIntersectionType>());
            else if (o.Kind == OperandKind.RayQueryCandidateIntersectionType)
                dis.Append(o.ToEnum<RayQueryCandidateIntersectionType>());
            else if (o.Kind == OperandKind.IdResultType)
                dis.Append(o.To<IdResultType>());
            else if (o.Kind == OperandKind.IdMemorySemantics)
                dis.AppendInt(o.To<IdMemorySemantics>());
            else if (o.Kind == OperandKind.IdScope)
                dis.AppendInt(o.To<IdScope>());
            else if (o.Kind == OperandKind.IdRef)
                dis.Append(o.To<IdRef>());
            else if (o.Kind == OperandKind.LiteralInteger)
                dis.AppendLiteral(o.To<LiteralInteger>());
            else if (o.Kind == OperandKind.LiteralString)
                dis.AppendLiteral(o.To<LiteralString>(), true);
            else if (o.Kind == OperandKind.PairLiteralIntegerIdRef)
                dis.Append(o.To<PairLiteralIntegerIdRef>());
            else if (o.Kind == OperandKind.PairIdRefLiteralInteger)
                dis.Append(o.To<PairIdRefLiteralInteger>());
            else if (o.Kind == OperandKind.PairIdRefIdRef)
                dis.Append(o.To<PairIdRefIdRef>());
            else if (o.Kind == OperandKind.LiteralContextDependentNumber)
                dis.AppendLiteral(o.To<LiteralInteger>());
        }
        else
        {
            if (o.Kind == OperandKind.IdRef)
                foreach (var e in o.Words)
                    dis.Append(new IdRef(e));
            else if (o.Kind == OperandKind.PackedVectorFormat)
                foreach (var e in o.Words)
                    dis.Append((PackedVectorFormat)e);
            else if (o.Kind == OperandKind.ImageOperands)
                foreach (var e in o.Words)
                    dis.Append((ImageOperandsMask)e);
            else if (o.Kind == OperandKind.FPFastMathMode)
                foreach (var e in o.Words)
                    dis.Append((FPFastMathModeMask)e);
            else if (o.Kind == OperandKind.SelectionControl)
                foreach (var e in o.Words)
                    dis.Append((SelectionControlMask)e);
            else if (o.Kind == OperandKind.LoopControl)
                foreach (var e in o.Words)
                    dis.Append((LoopControlMask)e);
            else if (o.Kind == OperandKind.FunctionControl)
                foreach (var e in o.Words)
                    dis.Append((FunctionControlMask)e);
            else if (o.Kind == OperandKind.MemorySemantics)
                foreach (var e in o.Words)
                    dis.Append((MemorySemanticsMask)e);
            else if (o.Kind == OperandKind.MemoryAccess)
                foreach (var e in o.Words)
                    dis.Append((MemoryAccessMask)e);
            else if (o.Kind == OperandKind.KernelProfilingInfo)
                foreach (var e in o.Words)
                    dis.Append((KernelProfilingInfoMask)e);
            else if (o.Kind == OperandKind.RayFlags)
                foreach (var e in o.Words)
                    dis.Append((RayFlagsMask)e);
            else if (o.Kind == OperandKind.FragmentShadingRate)
                foreach (var e in o.Words)
                    dis.Append((FragmentShadingRateMask)e);
            else if (o.Kind == OperandKind.SourceLanguage)
                foreach (var e in o.Words)
                    dis.Append((SourceLanguage)e);
            else if (o.Kind == OperandKind.ExecutionModel)
                foreach (var e in o.Words)
                    dis.Append((ExecutionModel)e);
            else if (o.Kind == OperandKind.AddressingModel)
                foreach (var e in o.Words)
                    dis.Append((AddressingModel)e);
            else if (o.Kind == OperandKind.MemoryModel)
                foreach (var e in o.Words)
                    dis.Append((MemoryModel)e);
            else if (o.Kind == OperandKind.ExecutionMode)
                foreach (var e in o.Words)
                    dis.Append((ExecutionMode)e);
            else if (o.Kind == OperandKind.StorageClass)
                foreach (var e in o.Words)
                    dis.Append((StorageClass)e);
            else if (o.Kind == OperandKind.Dim)
                foreach (var e in o.Words)
                    dis.Append((Dim)e);
            else if (o.Kind == OperandKind.SamplerAddressingMode)
                foreach (var e in o.Words)
                    dis.Append((SamplerAddressingMode)e);
            else if (o.Kind == OperandKind.SamplerFilterMode)
                foreach (var e in o.Words)
                    dis.Append((SamplerFilterMode)e);
            else if (o.Kind == OperandKind.ImageFormat)
                foreach (var e in o.Words)
                    dis.Append((ImageFormat)e);
            else if (o.Kind == OperandKind.ImageChannelOrder)
                foreach (var e in o.Words)
                    dis.Append((ImageChannelOrder)e);
            else if (o.Kind == OperandKind.ImageChannelDataType)
                foreach (var e in o.Words)
                    dis.Append((ImageChannelDataType)e);
            else if (o.Kind == OperandKind.FPRoundingMode)
                foreach (var e in o.Words)
                    dis.Append((FPRoundingMode)e);
            else if (o.Kind == OperandKind.LinkageType)
                foreach (var e in o.Words)
                    dis.Append((LinkageType)e);
            else if (o.Kind == OperandKind.AccessQualifier)
                foreach (var e in o.Words)
                    dis.Append((AccessQualifier)e);
            else if (o.Kind == OperandKind.FunctionParameterAttribute)
                foreach (var e in o.Words)
                    dis.Append((FunctionParameterAttribute)e);
            else if (o.Kind == OperandKind.Decoration)
                foreach (var e in o.Words)
                    dis.Append((Decoration)e);
            else if (o.Kind == OperandKind.BuiltIn)
                foreach (var e in o.Words)
                    dis.Append((BuiltIn)e);
            else if (o.Kind == OperandKind.Scope)
                foreach (var e in o.Words)
                    dis.Append((Scope)e);
            else if (o.Kind == OperandKind.GroupOperation)
                foreach (var e in o.Words)
                    dis.Append((GroupOperation)e);
            else if (o.Kind == OperandKind.KernelEnqueueFlags)
                foreach (var e in o.Words)
                    dis.Append((KernelEnqueueFlags)e);
            else if (o.Kind == OperandKind.Capability)
                foreach (var e in o.Words)
                    dis.Append((Capability)e);
            else if (o.Kind == OperandKind.RayQueryIntersection)
                foreach (var e in o.Words)
                    dis.Append((RayQueryIntersection)e);
            else if (o.Kind == OperandKind.RayQueryCommittedIntersectionType)
                foreach (var e in o.Words)
                    dis.Append((RayQueryCommittedIntersectionType)e);
            else if (o.Kind == OperandKind.RayQueryCandidateIntersectionType)
                foreach (var e in o.Words)
                    dis.Append((RayQueryCandidateIntersectionType)e);
            else if (o.Kind == OperandKind.IdResultType)
                foreach (var e in o.Words)
                    dis.Append((IdResultType)e);
            else if (o.Kind == OperandKind.IdMemorySemantics)
                foreach (var e in o.Words)
                    dis.AppendInt((IdMemorySemantics)e);
            else if (o.Kind == OperandKind.IdScope)
                foreach (var e in o.Words)
                    dis.AppendInt((IdScope)e);
            else if (o.Kind == OperandKind.IdRef)
                foreach (var e in o.Words)
                    dis.Append((IdRef)e);
            else if (o.Kind == OperandKind.LiteralInteger)
                foreach (var e in o.Words)
                    dis.AppendInt(e);
            else if (o.Kind == OperandKind.PairLiteralIntegerIdRef)
                for(int i = 0; i <  o.Words.Length; i+=2)
                    dis.Append(new PairLiteralIntegerIdRef((o.Words[i], o.Words[i+1])));
            else if (o.Kind == OperandKind.PairIdRefLiteralInteger)
                for (int i = 0; i < o.Words.Length; i += 2)
                    dis.Append(new PairIdRefLiteralInteger((o.Words[i], o.Words[i + 1])));
            else if (o.Kind == OperandKind.PairIdRefIdRef)
                for (int i = 0; i < o.Words.Length; i += 2)
                    dis.Append(new PairIdRefIdRef((o.Words[i], o.Words[i + 1])));
            else if (o.Kind == OperandKind.LiteralContextDependentNumber)
                dis.AppendLiteral(o.To<LiteralInteger>());
        }
    }
}

// file static class WordsExtension
// {
//     public static OrderedEnumerator GetEnumerator(this Span<int> words)
//     {

//     }
// }