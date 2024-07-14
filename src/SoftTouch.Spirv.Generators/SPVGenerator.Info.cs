using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Security.Claims;

namespace SoftTouch.Spirv.Generators
{
    public partial class SPVGenerator
    {


        public void CreateInfo(GeneratorExecutionContext context)
        {

            GenerateKinds(context);

            var code = new CodeWriter();

            code
            .AppendLine("using static Spv.Specification;")
            .AppendLine("")
            .AppendLine("namespace SoftTouch.Spirv.Core;")
            .AppendLine("")
            .AppendLine("public partial class InstructionInfo")
            .AppendLine("{")
            .Indent()
            .AppendLine("static InstructionInfo()")
            .AppendLine("{")
            .Indent();


            foreach (var instruction in spirvCore.RootElement.GetProperty("instructions").EnumerateArray())
            {
                GenerateInfo(instruction, code);
            }
            foreach (var instruction in spirvSDSL.RootElement.GetProperty("instructions").EnumerateArray())
            {
                GenerateInfo(instruction, code);
            }
            code
            .AppendLine("Instance.InitOrder();")
            .Dedent()
            .AppendLine("}")
            .Dedent()
            .AppendLine("}");


            context.AddSource("InstructionInfo.gen.cs", code.ToString());
        }

        private void GenerateKinds(GeneratorExecutionContext context)
        {
            var code = new CodeWriter()
            .AppendLine("using static Spv.Specification;")
            .AppendLine("")
            .AppendLine("namespace SoftTouch.Spirv.Core;")
            .AppendLine("\n\n")
            .AppendLine("public enum OperandKind")
            .AppendLine("{")
            .Indent()
            .AppendLine("None = 0,");
            var kinds = spirvCore.RootElement.GetProperty("operand_kinds").EnumerateArray().Select(x => x.GetProperty("kind").GetString());
            foreach (var kind in kinds)
            {
                code.Append(kind).AppendLine(",");
            }
            code.Dedent().AppendLine("}");

            context.AddSource("OperandKind.gen.cs", code.ToString());

        }

        public void GenerateInfo(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();
            var spvClass = op.GetProperty("class").GetString();
            if (opname == "OpExtInst")
            {
                code.AppendLine("Instance.Register(SDSLOp.OpExtInst, OperandKind.IdResultType, OperandQuantifier.One, \"resultType\", \"GLSL\");");
                code.AppendLine("Instance.Register(SDSLOp.OpExtInst, OperandKind.IdResult, OperandQuantifier.One, \"resultId\", \"GLSL\");");
                code.AppendLine("Instance.Register(SDSLOp.OpExtInst, OperandKind.IdRef, OperandQuantifier.One, \"set\", \"GLSL\");");
                code.AppendLine("Instance.Register(SDSLOp.OpExtInst, OperandKind.LiteralInteger, OperandQuantifier.One, \"instruction\", \"GLSL\");");
                code.AppendLine("Instance.Register(SDSLOp.OpExtInst, OperandKind.IdRef, OperandQuantifier.ZeroOrMore, \"values\", \"GLSL\");");
            }
            else if (op.TryGetProperty("operands", out var operands))
            {
                foreach (var operand in operands.EnumerateArray())
                {
                    var hasKind = operand.TryGetProperty("kind", out var kindJson);
                    var hasQuant = operand.TryGetProperty("quantifier", out var quantifierJson);
                    var hasName = operand.TryGetProperty("name", out var nameJson);

                    if (hasKind)
                    {
                        var kind = kindJson.GetString();
                        if (!hasQuant)
                        {
                            code
                                .Append("Instance.Register(SDSLOp.")
                                .Append(opname)
                                .Append(", OperandKind.")
                                .Append(kindJson.GetString())
                                .Append(", OperandQuantifier.One, ")
                                .Append(!hasName ? $"\"{ConvertKindToName(kindJson.GetString())}\"" : $"\"{ConvertOperandName(nameJson.GetString())}\"")
                                .Append($", \"{spvClass}\"")
                                .AppendLine(");");
                        }
                        else
                        {
                            var quant = quantifierJson.GetString();
                            code
                                .Append("Instance.Register(SDSLOp.")
                                .Append(opname)
                                .Append(", OperandKind.")
                                .Append(kindJson.GetString())
                                .Append(", OperandQuantifier.")
                                .Append(ConvertQuantifier(quantifierJson.GetString()))
                                .Append(", ")
                                .Append(!hasName ? $"\"{ConvertNameQuantToName(kind, quant)}\"" : $"\"{ConvertNameQuantToName(nameJson.GetString(), quant)}\"")
                                .Append($", \"{spvClass}\"")
                                .AppendLine(");");
                        }
                    }
                }
            }
            else
            {
                code.Append("Instance.Register(SDSLOp.").Append(opname).AppendLine(", OperandKind.None, null, \"Debug\");");
            }
        }

        public static string ConvertNameQuantToName(string name, string quant)
        {
            return (name, quant) switch
            {
                (_, "*") => "values",
                ("event", _) => "eventId",
                ("string", _) => "value",
                ("base", _) => "baseId",
                ("object", _) => "objectId",
                ("default", _) => "defaultId",
                _ => name.Replace("'", "").ToLowerInvariant()
            };
        }

        public static string ConvertQuantifier(string quant)
        {
            if (quant == "*")
                return "ZeroOrMore";
            else if (quant == "?")
                return "ZeroOrOne";
            else return "One";
        }
    }
}
