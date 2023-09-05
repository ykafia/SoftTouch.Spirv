using CommunityToolkit.HighPerformance;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;

public partial class Mixer
{
    public MixinInstruction CreateTypePointer(ReadOnlyMemory<char> type, Spv.Specification.StorageClass storage)
    {
        var t = GetOrCreateBaseType(type[..^1]);
        return buffer.AddOpTypePointer(storage, t.ResultId ?? -1);
    }
    public MixinInstruction GetOrCreateBaseType(ReadOnlyMemory<char> type)
    {
        var matched = MatchesBaseType(type);
        if (matched is null) return Instruction.Empty;
        else
        {
            if (matched.Value.IsScalar)
            {
                var found = FindScalarType(type);
                if (!found.IsEmpty)
                    return found;
                else return matched.Value.BaseType.Span switch
                {
                    "void" => buffer.AddOpTypeVoid(),
                    "sbyte" => buffer.AddOpTypeInt(8, 1),
                    "byte" => buffer.AddOpTypeInt(8, 0),
                    "short" => buffer.AddOpTypeInt(16, 1),
                    "ushort" => buffer.AddOpTypeInt(16, 0),
                    "int" => buffer.AddOpTypeInt(32, 1),
                    "uint" => buffer.AddOpTypeInt(32, 0),
                    "long" => buffer.AddOpTypeInt(64, 1),
                    "ulong" => buffer.AddOpTypeInt(64, 0),
                    "half" => buffer.AddOpTypeFloat(16),
                    "float" => buffer.AddOpTypeFloat(32),
                    "double" => buffer.AddOpTypeFloat(64),
                    _ => throw new NotImplementedException()
                };
            }
            else if (matched.Value.IsVector)
            {
                var found = FindVectorType(matched.Value);
                if (!found.IsEmpty)
                    return found;
                else
                {
                    var b = GetOrCreateBaseType(matched.Value.BaseType);
                    if(b.MixinName == "")
                        return buffer.AddOpTypeVector(b.ResultId ?? -1, new(matched?.Row));
                    else
                    {
                        var imported = buffer.AddOpSDSLImportIdRef(b.MixinName, b.ResultId ?? -1);
                        return buffer.AddOpTypeVector(imported.ResultId ?? -1, new(matched?.Row));
                    }
                }
            }
            else if (matched.Value.IsMatrix)
            {
                var found = FindMatrixType(matched.Value);
                if (!found.IsEmpty)
                    return found;
                else 
                {
                    var b = GetOrCreateBaseType($"{matched.Value.BaseType.Span}{matched?.Row}".AsMemory());
                    if (b.MixinName == "")
                        return buffer.AddOpTypeMatrix(b.ResultId ?? -1, new(matched?.Row));
                    else
                    {
                        var imported = buffer.AddOpSDSLImportIdRef(b.MixinName, b.ResultId ?? -1);
                        return buffer.AddOpTypeMatrix(imported.ResultId ?? -1, new(matched?.Row));
                    }
                }
            }
            else
                throw new NotImplementedException();
            
        }
    }

    internal MixinInstruction FindMatrixType(in TypeMatch type)
    {
        var baseType = FindVectorType(type with {Col = null});
        if(baseType.IsEmpty)
            return Instruction.Empty;

        // var enumerator = new MixinFilteredInstructionEnumerator(mixins, SDSLOp.OpTypeMatrix);

        // while (enumerator.MoveNext())
        // {
        //     if (enumerator.Current.Words[2] == baseType.Words[2] && enumerator.Current.Words[3] == type.Col)
        //         return enumerator.Current;
        // }

        var self = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeMatrix);

        while (self.MoveNext())
        {
            if(self.Current.Words.Span[2] == baseType.Words[2] && self.Current.Words.Span[3] == type.Col)
                return self.Current;
        }
        return Instruction.Empty;
    }
    internal MixinInstruction FindVectorType(in TypeMatch type)
    {
        var baseType = FindScalarType(type.BaseType);
        if(baseType.IsEmpty)
            return baseType;

        // var enumerator = new MixinFilteredInstructionEnumerator(mixins, SDSLOp.OpTypeVector);

        // while (enumerator.MoveNext())
        // {
        //     if (enumerator.Current.Words[2] == baseType.Words[2] && enumerator.Current.Words[3] == type.Row)
        //         return enumerator.Current;
        // }

        var self = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeVector);

        while (self.MoveNext())
        {
            if (self.Current.Words.Span[2] == baseType.Words[2] && self.Current.Words.Span[3] == type.Row)
                return self.Current;
        }
        return Instruction.Empty;
    }

    internal MixinInstruction FindScalarType(ReadOnlyMemory<char> type)
    {
        (SDSLOp Filter, int? Width, int? Sign) filterData = type.Span switch
        {
            "void" => (SDSLOp.OpTypeVoid, null, null),
            "bool" => (SDSLOp.OpTypeBool, null, null),
            "byte" => (SDSLOp.OpTypeInt, 8, 0),
            "sbyte" => (SDSLOp.OpTypeInt, 8, 1),
            "ushort" => (SDSLOp.OpTypeInt, 16, 0),
            "short" => (SDSLOp.OpTypeInt, 16, 1),
            "int" => (SDSLOp.OpTypeInt, 32, 1),
            "uint" => (SDSLOp.OpTypeInt, 32, 0),
            "ulong" => (SDSLOp.OpTypeInt, 64, 0),
            "long" => (SDSLOp.OpTypeInt, 64, 1),
            "half" => (SDSLOp.OpTypeFloat, 16, null),
            "float" => (SDSLOp.OpTypeFloat, 32, null),
            "double" => (SDSLOp.OpTypeFloat, 64, null),
            _ => throw new Exception("Type not known")
        };
        var enumerator = new MixinFilteredInstructionEnumerator(mixins, filterData.Filter);

        // while (enumerator.MoveNext())
        // {
        //     if (
        //         (filterData.Width is null)
        //         || (filterData.Width is not null && filterData.Sign is null && enumerator.Current.Words[2] == filterData.Width)
        //         || (filterData.Width is not null && filterData.Sign is not null && enumerator.Current.Words[2] == filterData.Width && enumerator.Current.Words[3] == filterData.Sign)
        //     )
        //         return enumerator.Current;
        // }
        var self = new FilteredEnumerator<WordBuffer>(buffer, filterData.Filter);

        while (self.MoveNext())
        {
            if (
                (filterData.Width is null)
                || (filterData.Width is not null && filterData.Sign is null && self.Current.Words.Span[2] == filterData.Width)
                || (filterData.Width is not null && filterData.Sign is not null && self.Current.Words.Span[2] == filterData.Width && self.Current.Words.Span[3] == filterData.Sign)
            )
                return self.Current;
        }
        return Instruction.Empty;
    }

    static string[] types = { "bool", "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong", "half", "float", "double" };

    private static TypeMatch? MatchesBaseType(ReadOnlyMemory<char> type)
    {
        if (type.Span.StartsWith("void") && type.Span.EndsWith("void"))
            return new(type, null, null);
        foreach (var t in types)
        {
            var span = type;
            bool isPtr = false;
            if (span.Span.EndsWith("*"))
            {
                span = type[..^1];
                isPtr = true;
            }
            if (span.Span.StartsWith(t) && span.Span.EndsWith(t))
            {
                return new(span, null, null, isPtr);
            }
            else if (span.Span.StartsWith(t))
            {
                if (span.Length - t.Length == 1 && char.IsDigit(span.Span[^1]))
                {
                    var num = span.Span[^1] - '0';
                    if (num > 1 && num < 5)
                        return new(t.AsMemory(), num, null);
                }
                else if (span.Length - t.Length == 3 && char.IsDigit(span.Span[^1]) && char.IsDigit(span.Span[^3]) && span.Span[^2] == 'x')
                {
                    var num1 = span.Span[^3] - '0';
                    var num2 = span.Span[^1] - '0';
                    if (num1 > 1 && num1 < 5 && num2 > 1 && num2 < 5)
                        return new(t.AsMemory(), num1, num2, isPtr);
                }
            }
        }
        return null;
    }
}
internal record struct TypeMatch(ReadOnlyMemory<char> BaseType, int? Row, int? Col, bool IsPointer = false)
{
    public bool IsVoid => BaseType.Span.StartsWith("void") && BaseType.Span.EndsWith("void");
    public bool IsMatrix => Row != null && Col != null;
    public bool IsVector => Row != null && Col == null;
    public bool IsScalar => Row == null && Col == null;
}
