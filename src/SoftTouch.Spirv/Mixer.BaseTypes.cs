using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;

public partial class Mixer
{
    public IdRef GetOrCreateBaseType(string type)
    {
        var matched = MatchesBaseType(type);
        if (matched is null) return -1;
        else
        {
            return matched switch
            {
                { BaseType: "sbyte", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(8, 1),
                { BaseType: "byte", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(8, 0),
                { BaseType: "short", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(16, 1),
                { BaseType: "ushort", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(16, 0),
                { BaseType: "int", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(32, 1),
                { BaseType: "uint", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(32, 0),
                { BaseType: "long", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(64, 1),
                { BaseType: "ulong", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeInt(64, 0),
                { BaseType: "half", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeFloat(16),
                { BaseType: "float", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeFloat(32),
                { BaseType: "double", IsScalar: true } => FindScalarType(type) ?? buffer.AddOpTypeFloat(64),
                { IsVector: true } =>  FindVectorType(matched.Value) ?? buffer.AddOpTypeVector(GetOrCreateBaseType(matched?.BaseType!), new(matched?.Row)),
                { IsMatrix: true } =>  FindMatrixType(matched.Value) ?? buffer.AddOpTypeMatrix(GetOrCreateBaseType(matched?.BaseType! + matched?.Row), new(matched?.Col)),
                _ => throw new NotImplementedException()
            };
        }
    }

    internal IdRef? FindMatrixType(in TypeMatch type)
    {
        var baseType = FindVectorType(type with {Col = null});
        if(baseType == null)
            return null;
        var enumerator = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeMatrix);

        while (enumerator.MoveNext())
        {
            if(enumerator.Current.Words[2] == baseType.Value.Value && enumerator.Current.Words[3] == type.Col)
                return enumerator.Current.ResultId;
        }
        return null;
    }
    internal IdRef? FindVectorType(in TypeMatch type)
    {
        var baseType = FindScalarType(type.BaseType);
        if(baseType == null)
            return null;
        var enumerator = new FilteredEnumerator<WordBuffer>(buffer, SDSLOp.OpTypeVector);

        while (enumerator.MoveNext())
        {
            if(enumerator.Current.Words[2] == baseType && enumerator.Current.Words[3] == type.Row)
                return enumerator.Current.ResultId;
        }
        return null;
    }

    internal IdRef? FindScalarType(string type)
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
        var enumerator = new FilteredEnumerator<WordBuffer>(buffer, filterData.Filter);

        while (enumerator.MoveNext())
        {
            if (
                (filterData.Width is null)
                || (filterData.Width is not null && filterData.Sign is null && enumerator.Current.Words[2] == filterData.Width)
                || (filterData.Width is not null && filterData.Sign is not null && enumerator.Current.Words[2] == filterData.Width && enumerator.Current.Words[3] == filterData.Sign)
            )
                return enumerator.Current.ResultId;
        }
        return null;
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
