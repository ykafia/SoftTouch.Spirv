using System.Text;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv.Core;

public static class SDSLDis
{
    public static string Disassemble(Memory<int> words)
    {
        var builder = new StringBuilder();
        SpirvReader.ParseToList(words, out var list);

        // Find the longest name
        var maxLength = 0;
        var maxId = 0;
        foreach (var i in list)
        {
            if ((i.OpCode == SDSLOp.OpName || i.OpCode == SDSLOp.OpMemberName) && i.GetOperand<LiteralString>("name")?.Value.Length > maxLength)
                maxLength = i.GetOperand<LiteralString>("name")?.Value.Length + 1?? maxLength;
            if (i.ResultId != null)
                maxId = i.ResultId ?? maxId;
        }
        if (maxLength == 0)
            maxLength = 1 + maxId.ToString().Length;
        
        foreach(var i in list)
        {
            if (i.ResultId != null)
            {
                var name = list.FindNameOfId(i.ResultId.Value);
                var padding = maxLength - name.Length;
                builder
                    .Append(new string(' ', padding))
                    .Append(name)
                    .Append(" = ");
            }
            else
                builder
                    .Append(new string(' ', maxLength + 3));
            builder.Append(i.OpCode.ToString()).Append(' ');
            foreach(var op in i)
            {
                if(op.Kind != OperandKind.IdResult)
                    builder
                    .Append(op.ValueToText(list))
                    .Append(' ');
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }
}

file static class DisassemblerExtensions
{
    public static string FindNameOfId(this List<Instruction> list, int id)
    {
        foreach(var e in list)
        {
            if ((e.OpCode == SDSLOp.OpName || e.OpCode == SDSLOp.OpMemberName) && e.Words.Span[1] == id)
                return $"%{e.GetOperand<LiteralString>("name")?.Value}";
        }
        return $"%{id}";
    }

    public static string ValueToText(this SpvOperand op, List<Instruction> list)
    {
        return op.Kind switch 
        {
            OperandKind.LiteralString => $"\"{op.To<LiteralString>().Value}\"",
            OperandKind.IdRef => list.FindNameOfId(op.Words[0]),
            OperandKind.IdResultType => list.FindNameOfId(op.Words[0]),
            OperandKind.PairLiteralIntegerIdRef => $"({op.Words[0]}, {list.FindNameOfId(op.Words[0])})",
            OperandKind.MemoryAccess => $"{op.ToEnum<Spv.Specification.MemoryAccessMask>()}",
            OperandKind.MemoryModel => $"{op.ToEnum<Spv.Specification.MemoryModel>()}",
            OperandKind.MemorySemantics => $"{op.ToEnum<Spv.Specification.MemorySemanticsMask>()}",
            OperandKind.AccessQualifier => $"{op.ToEnum<Spv.Specification.AccessQualifier>()}",
            OperandKind.AddressingModel => $"{op.ToEnum<Spv.Specification.AddressingModel>()}",
            OperandKind.BuiltIn => $"{op.ToEnum<Spv.Specification.BuiltIn>()}",
            OperandKind.Capability => $"{op.ToEnum<Spv.Specification.Capability>()}",
            OperandKind.Decoration => $"{op.ToEnum<Spv.Specification.Decoration>()}",
            OperandKind.Dim => $"{op.ToEnum<Spv.Specification.Dim>()}",
            OperandKind.ExecutionMode => $"{op.ToEnum<Spv.Specification.ExecutionMode>()}",
            OperandKind.ExecutionModel => $"{op.ToEnum<Spv.Specification.ExecutionModel>()}",
            OperandKind.FPFastMathMode => $"{op.ToEnum<Spv.Specification.FPFastMathModeMask>()}",
            OperandKind.FPRoundingMode => $"{op.ToEnum<Spv.Specification.FPRoundingMode>()}",
            OperandKind.FragmentShadingRate => $"{op.ToEnum<Spv.Specification.FragmentShadingRateMask>()}",
            OperandKind.FunctionControl => $"{op.ToEnum<Spv.Specification.FunctionControlMask>()}",
            OperandKind.FunctionParameterAttribute => $"{op.ToEnum<Spv.Specification.FunctionParameterAttribute>()}",
            OperandKind.GroupOperation => $"{op.ToEnum<Spv.Specification.GroupOperation>()}",
            OperandKind.ImageChannelDataType => $"{op.ToEnum<Spv.Specification.ImageChannelDataType>()}",
            OperandKind.ImageChannelOrder => $"{op.ToEnum<Spv.Specification.ImageChannelOrder>()}",
            OperandKind.ImageFormat => $"{op.ToEnum<Spv.Specification.ImageFormat>()}",
            OperandKind.ImageOperands => $"{op.ToEnum<Spv.Specification.ImageOperandsMask>()}",
            OperandKind.KernelEnqueueFlags => $"{op.ToEnum<Spv.Specification.KernelEnqueueFlags>()}",
            OperandKind.KernelProfilingInfo => $"{op.ToEnum<Spv.Specification.KernelProfilingInfoMask>()}",
            OperandKind.LinkageType => $"{op.ToEnum<Spv.Specification.LinkageType>()}",
            OperandKind.None => "",
            _ => $"%{op.Words[0]}"
        };
    }
}