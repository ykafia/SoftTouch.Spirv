using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;

public partial class Mixer
{
    public RefInstruction GetOrCreateBaseType(string type)
    {
        var matched = MatchesBaseType(type);
        if (matched is null) return RefInstruction.Empty;
        else
        {
            if (matched.Value.IsScalar)
            {
                var found = FindScalarType(type);
                if (!found.IsEmpty)
                    return found;
                else return matched switch
                {
                    { BaseType: "sbyte" } => buffer.AddOpTypeInt(8, 1).AsRef(),
                    { BaseType: "byte" } => buffer.AddOpTypeInt(8, 0).AsRef(),
                    { BaseType: "short" } => buffer.AddOpTypeInt(16, 1).AsRef(),
                    { BaseType: "ushort" } => buffer.AddOpTypeInt(16, 0).AsRef(),
                    { BaseType: "int" } => buffer.AddOpTypeInt(32, 1).AsRef(),
                    { BaseType: "uint" } => buffer.AddOpTypeInt(32, 0).AsRef(),
                    { BaseType: "long" } => buffer.AddOpTypeInt(64, 1).AsRef(),
                    { BaseType: "ulong" } => buffer.AddOpTypeInt(64, 0).AsRef(),
                    { BaseType: "half" } => buffer.AddOpTypeFloat(16).AsRef(),
                    { BaseType: "float" } => buffer.AddOpTypeFloat(32).AsRef(),
                    { BaseType: "double" } => buffer.AddOpTypeFloat(64).AsRef(),
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
                    var b = GetOrCreateBaseType(matched?.BaseType!);
                    return buffer.AddOpTypeVector(b.ResultId ?? -1, new(matched?.Row)).AsRef();
                }
            }
            else if (matched.Value.IsMatrix)
            {
                var found = FindMatrixType(matched.Value);
                if (!found.IsEmpty)
                    return found;
                else return buffer.AddOpTypeMatrix(GetOrCreateBaseType(matched?.BaseType! + matched?.Row).ResultId ?? -1, new(matched?.Row)).AsRef();
            }
            else
                throw new NotImplementedException();
            
        }
    }

    internal RefInstruction FindMatrixType(in TypeMatch type)
    {
        var baseType = FindVectorType(type with {Col = null});
        if(baseType.IsEmpty)
            return RefInstruction.Empty;

        var enumerator = new MixinFilteredInstructionEnumerator(mixins, SDSLOp.OpTypeMatrix);

        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Words[2] == baseType.Words[2] && enumerator.Current.Words[3] == type.Col)
                return enumerator.Current;
        }

        var self = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeMatrix);

        while (self.MoveNext())
        {
            if(self.Current.Words[2] == baseType.Words[2] && self.Current.Words[3] == type.Col)
                return self.Current;
        }
        return RefInstruction.Empty;
    }
    internal RefInstruction FindVectorType(in TypeMatch type)
    {
        var baseType = FindScalarType(type.BaseType);
        if(baseType.IsEmpty)
            return baseType;

        var enumerator = new MixinFilteredInstructionEnumerator(mixins, SDSLOp.OpTypeVector);

        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Words[2] == baseType.Words[2] && enumerator.Current.Words[3] == type.Row)
                return enumerator.Current;
        }

        var self = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeVector);

        while (self.MoveNext())
        {
            if (self.Current.Words[2] == baseType.Words[2] && self.Current.Words[3] == type.Row)
                return self.Current;
        }
        return RefInstruction.Empty;
    }

    internal RefInstruction FindScalarType(string type)
    {
        (SDSLOp Filter, int? Width, int? Sign) filterData = type switch
        {
            "function" => (SDSLOp.OpTypeFunction, null, null),
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

        while (enumerator.MoveNext())
        {
            if (
                (filterData.Width is null)
                || (filterData.Width is not null && filterData.Sign is null && enumerator.Current.Words[2] == filterData.Width)
                || (filterData.Width is not null && filterData.Sign is not null && enumerator.Current.Words[2] == filterData.Width && enumerator.Current.Words[3] == filterData.Sign)
            )
                return enumerator.Current;
        }
        var self = new FilteredEnumerator<WordBuffer>(buffer, filterData.Filter);

        while (self.MoveNext())
        {
            if (
                (filterData.Width is null)
                || (filterData.Width is not null && filterData.Sign is null && self.Current.Words[2] == filterData.Width)
                || (filterData.Width is not null && filterData.Sign is not null && self.Current.Words[2] == filterData.Width && self.Current.Words[3] == filterData.Sign)
            )
                return self.Current;
        }
        return RefInstruction.Empty;
    }

    static string[] types = { "bool", "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong", "half", "float", "double" };

    private static TypeMatch? MatchesBaseType(string type)
    {
        if(type == null)
            return null;
        if (type == "void")
            return new(type, null, null);
        foreach (var t in types)
        {
            if (type == t)
                return new(type, null, null);
            else if (type.StartsWith(t))
            {
                if (type.Length - t.Length == 1 && char.IsDigit(type[^1]))
                {
                    var num = type[^1] - '0';
                    if (num > 1 && num < 5)
                        return new(t, num, null);
                }
                if (type.Length - t.Length == 3 && char.IsDigit(type[^1]) && char.IsDigit(type[^3]) && type[^2] == 'x')
                {
                    var num1 = type[^3] - '0';
                    var num2 = type[^1] - '0';
                    if (num1 > 1 && num1 < 5 && num2 > 1 && num2 < 5)
                        return new(t, num1, num2);
                }
            }
        }
        return null;
    }
}
internal record struct TypeMatch(string BaseType, int? Row, int? Col)
{
    public bool IsMatrix => Row != null && Col != null;
    public bool IsVector => Row != null && Col == null;
    public bool IsScalar => Row == null && Col == null;
}
