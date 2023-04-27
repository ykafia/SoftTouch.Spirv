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

    [Generator]
    public class SPVGenerator : ISourceGenerator
    {
        static JsonDocument spirvCore;
        static JsonDocument spirvGlsl;


        public void Initialize(GeneratorInitializationContext context)
        {
            // #if DEBUG
            //             if (!Debugger.IsAttached)
            //                 Debugger.Launch();
            // #endif
            var assembly = typeof(SPVGenerator).GetTypeInfo().Assembly;
            string resourceCoreName =
                assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("spirv.core.grammar.json"));

            string resourceGlslName =
                assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("extinst.glsl.std.450.grammar.json"));

            spirvCore = JsonDocument.Parse(new StreamReader(assembly.GetManifestResourceStream(resourceCoreName)).ReadToEnd());
            spirvGlsl = JsonDocument.Parse(new StreamReader(assembly.GetManifestResourceStream(resourceGlslName)).ReadToEnd());
        }


        public void Execute(GeneratorExecutionContext context)
        {
            var code = new CodeWriter();

            code
            .AppendLine("using static Spv.Specification;")
            .AppendLine("namespace SoftTouch.Spirv.Internals;")
            .AppendLine("")
            .AppendLine("public partial struct Instruction")
            .AppendLine("{")
            .Indent();

            var instructions = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().ToList();

            instructions.ForEach(x => CreateOperation(x, code));

           code
           .Dedent()
           .AppendLine("}");
            context.AddSource(
                "Instructions.gen.cs",
                code.ToString()
            );
        }

        public void CreateOperation(JsonElement op, CodeWriter code)
        {
            if (op.TryGetProperty("operands", out var operands))
            {
                var parameters = operands.EnumerateArray().Select(ConvertOperandToParameter).Where(x => x != null);
                //parameters = string.Join(", ", operands.EnumerateArray().Select(ConvertOperandToParameter).Where(x => x != null));

                var parameterNames = operands.EnumerateArray().Select(ConvertOperandToParameterName).Where(x => x != null).ToList();
                var paramsParameters = parameters.Where(x => x.Contains("params"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("params"));

                var opname = op.GetProperty("opname").GetString();
                code
                    .Append("public static Instruction ")
                    .Append(opname)
                    .Append('(')
                    .Append(string.Join(", ",normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(")")
                    .AppendLine("{")
                    .Indent()
                        .Append("var operands = new OperandArray(").Append(5).AppendLine(");");

                foreach (var parameter in parameterNames.Where(x => x != "resultId" && x != "resultType"))
                {
                    code.Append("operands.Add(").Append(parameter).AppendLine(");");
                }

                code
                        .AppendLine("return new ()")
                        .AppendLine("{")
                        .Indent()
                            .AppendLine("Info = new()")
                            .AppendLine("{")
                            .Indent()
                                .Append("OpCode = Op.").Append(opname).AppendLine(",")
                                .Append("Name = ").Append(opname)
                            .Dedent()
                            .AppendLine("},")
                            .AppendLine("Operands = operands,");
                if (parameterNames.Any(x => x.Contains("resultId")))
                    code.AppendLine("ResultId = resultId,");
                if (parameterNames.Any(x => x.Contains("resultType")))
                    code.AppendLine("ResultType = resultType,");
                code
                        .Dedent()
                    .AppendLine("};")
                    .Dedent()
                    .AppendLine("}")
                    .AppendLine("");
            }
        }


        public static string ConvertOperandToParameter(JsonElement element)
        {
            var kind = element.GetProperty("kind").GetString();
            var hasQuantifier = element.TryGetProperty("quantifier", out var quantifier);
            string getName() => FirstCharToUpperAsSpan(element.GetProperty("name").GetString().Replace("'", "").Replace(" ", ""));
            
            if (kind == "IdResult") return "int resultId";
            if (kind == "IdResultType") return "int resultType";
            if (kind == "IdRef" && !hasQuantifier) return $"int {getName()}";
            if (kind == "IdRef" && hasQuantifier && quantifier.GetString() == "*") return $"params int[] values";
            if (kind == "IdRef" && hasQuantifier && quantifier.GetString() == "?") return $"int? {getName()} = null";
            if (kind == "LiteralInteger") return $"int {getName()}";
            if (kind == "LiteralFloat") return $"float {getName()}";
            if (kind == "LiteralString") return $"string {getName()}";
            if (kind == "Dim") return "Dim dimension";
            if (kind == "ImageFormat") return "ImageFormat imageFormat";
            if (kind == "ExecutionMode") return "ExecutionMode executionMode";
            if (kind == "ExecutionModel") return "ExecutionModel executionModel";
            if (kind == "Capability") return "Capability capability";
            return null; //throw new NotImplementedException($"Operand {element.GetProperty("kind").GetString()} not recognized")

        }
        public static string ConvertOperandToParameterName(JsonElement element)
        {
            var kind = element.GetProperty("kind").GetString();
            var hasQuantifier = element.TryGetProperty("quantifier", out var quantifier);
            string getName() => FirstCharToUpperAsSpan(element.GetProperty("name").GetString());

            if (kind == "IdResult") return "resultId";
            if (kind == "IdResultType") return "resultType";
            if (kind == "IdRef" && !hasQuantifier) return $"{getName()}";
            if (kind == "IdRef" && hasQuantifier && quantifier.GetString() == "*") return $"values";
            if (kind == "IdRef" && hasQuantifier && quantifier.GetString() == "?") return $"{getName()}";
            if (kind == "LiteralInteger") return $"{getName()}";
            if (kind == "LiteralFloat") return $"{getName()}";
            if (kind == "LiteralString") return $"{getName()}";
            if (kind == "Dim") return "dimension";
            if (kind == "ImageFormat") return "imageFormat";
            if (kind == "ExecutionMode") return "executionMode";
            if (kind == "ExecutionModel") return "executionModel";
            if (kind == "Capability") return "capability";
            return null; //throw new NotImplementedException($"Operand {element.GetProperty("kind").GetString()} not recognized")

        }
        public static string FirstCharToUpperAsSpan(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var result = "";
            bool firstLetterHit = false;
            for(int i = 0; i < input.Length; i++)
            {

                if (char.IsLetterOrDigit(input[i]) || input[i] == '_')
                {
                    if (!firstLetterHit)
                    {
                        firstLetterHit = true;
                        result += char.ToLowerInvariant(input[i]);
                    }
                    else
                        result += input[i];
                }
                
            }
            if (result == "event") return "eventId";
            if (result == "string") return "value";
            if (result == "base") return "baseId";
            if (result == "object") return "objectId";
            if (result == "default") return "defaultId";
            return result;
        }
    }
}
