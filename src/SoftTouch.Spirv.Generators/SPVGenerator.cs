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
    public partial class SPVGenerator : ISourceGenerator
    {
        JsonDocument spirvCore;
        JsonDocument spirvGlsl;

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
            .AppendLine("namespace SoftTouch.Spirv.Core;")
            .AppendLine("")
            .AppendLine("public partial class WordBuffer")
            .AppendLine("{")
            .Indent();

            var instructions = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().ToList();
            var glslInstruction = spirvGlsl.RootElement.GetProperty("instructions").EnumerateArray().ToList();

            instructions.ForEach(x => CreateOperation(x, code));
            glslInstruction.ForEach(x => CreateGlslOperation(x, code));

            code
           .Dedent()
           .AppendLine("}");
            context.AddSource(
                "WordBuffer.gen.cs",
                code.ToString()
            );


            code = new CodeWriter();

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

            foreach (var instruction in spirvCore.RootElement.GetProperty("instructions").EnumerateArray().ToList())
            {
                GenerateInfo(instruction, code);
            }
            code
            .Dedent()
            .AppendLine("}")
            .Dedent()
            .AppendLine("}");


            context.AddSource("InstructionInfo.gen.cs", code.ToString());
        }

        public void CreateOperation(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();
            if (opname == "OpConstant") return;
            if (opname == "OpSpecConstant") return;

            if (op.TryGetProperty("operands", out var operands))
            {
                var parameters = ConvertOperandsToParameters(op);
                var parameterNames = ConvertOperandsToParameterNames(op);

                var paramsParameters = parameters.Where(x => x.Contains("params"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("params"));

                code
                    .Append("public WordBuffer ")
                    .Append(opname)
                    .Append('(')
                    .Append(string.Join(", ", normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(")")
                    .AppendLine("{")
                    .Indent();

                code.Append("var wordLength = 1").Append(parameterNames.Any() ? " + " : "").Append(string.Join(" + ", parameterNames.Select(x => $"GetWordLength({x})"))).AppendLine(";");

                code.Append("var op = wordLength << 16 | (int)Op.").Append(opname).AppendLine(";");
                code.AppendLine("Add(op);");
                foreach(var p in parameterNames)
                {
                    code.Append("Add(").Append(p).AppendLine(");");
                }


                code
                    .AppendLine("return this;")
                    .Dedent().AppendLine("}");

                //if (parameterNames.Any(x => x.Contains("resultId")))
                //    code.AppendLine("Add(resultId)");
                //foreach (var parameter in parameterNames.Where(x => x != "resultId" && x != "resultType"))
                //{
                //    code.Append("Add(").Append(parameter).AppendLine(");");
                //}

                //code
                //        .AppendLine("return new()")
                //        .AppendLine("{")
                //        .Indent()
                //            .Append("OpCode = Op.").Append(opname).AppendLine(",")
                //            .AppendLine("Operands = operands,");
                //if (parameterNames.Any(x => x.Contains("resultId")))
                //    code.AppendLine("ResultId = resultId,");
                //if (parameterNames.Any(x => x.Contains("resultType")))
                //    code.AppendLine("ResultType = resultType,");
                //code
                //        .Dedent()
                //    .AppendLine("};")
                //    .Dedent()
                //    .AppendLine("}")
                //    .AppendLine("");
            }
            else
            {
                code
                    .Append("public WordBuffer ")
                    .Append(opname)
                    .AppendLine("()")
                    .AppendLine("{")
                    .Indent()
                        .Append("Add( 1 << 16 & (int)Op.").Append(opname).AppendLine(");")
                        .AppendLine("return this;")
                    .Dedent()
                    .AppendLine("}");
            }
        }

        public void CreateGlslOperation(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();
            var opcode = op.GetProperty("opcode").GetInt32();

            if (op.TryGetProperty("operands", out var operands))
            {
                var parameters = ConvertOperandsToParameters(op);
                parameters.Add("int set");

                var parameterNames = ConvertOperandsToParameterNames(op);
                parameterNames.Add("set");

                var paramsParameters = parameters.Where(x => x.Contains("params"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("params"));
                var other = parameterNames.Where(x => x != "resultType" && x != "resultId" && x != "set");


                code
                    .Append("public WordBuffer GLSL")
                    .Append(opname)
                    .Append('(')
                    .Append(string.Join(", ", normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(")")
                    .AppendLine("{")
                    .Indent()
                        .Append("OpExtInst(")
                            .Append("set, ")
                            .Append(opcode)
                            .Append(parameterNames.Any(x => x == "resultType") ? ", resultType, " : ", null")
                            .Append(parameterNames.Any(x => x == "resultId") ? ", resultId, " : ", null")
                            .Append(other.Count() > 0 ? ", " + string.Join(", ", other) : "").AppendLine(");")
                        .AppendLine("return this;")
                    .Dedent()
                    .AppendLine("}");

                
            }
        }
        

        public void GenerateInfo(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();

            if (op.TryGetProperty("operands", out var operands))
            {
                foreach (var operand in operands.EnumerateArray())
                {
                    var hasKind = operand.TryGetProperty("kind", out var kindJson);
                    var hasQuant = operand.TryGetProperty("quantifier", out var quantifierJson);
                    var hasName = operand.TryGetProperty("name", out var nameJson);

                    if (hasKind)
                    {
                        code
                            .Append("Instance.Register(Op.")
                            .Append(opname)
                            .Append(", OperandKind.")
                            .Append(kindJson.GetString())
                            .Append(", OperandQuantifier.")
                            .Append(!hasQuant ? "One" : ConvertQuantifier(quantifierJson.GetString()))
                            .Append(!hasName ? "" : ", \"" + ConvertOperandName(nameJson.GetString()) + "\"")
                            .AppendLine(");");
                    }
                }
            }
            else
            {
                code.Append("Instance.Register(Op.").Append(opname).AppendLine(", null, null);");
            }
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
