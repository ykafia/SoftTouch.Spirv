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

            CreateInfo(context);

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
        }

        public void CreateOperation(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();
            if (opname == "OpConstant")
            {
                code
                    .AppendLine("public Instruction AddOpConstant(IdResultType? resultType, IdResult? resultId, LiteralInteger value)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var op = wordLength << 16 | (int)Op.OpConstant;")
                        .AppendLine("Add(op);")
                        .AppendLine("Add(resultType);")
                        .AppendLine("Add(resultId);")
                        .AppendLine("Add(value);")
                        .AppendLine("return new(this, Count-1);")
                    .Dedent()
                    .AppendLine("}");

                code
                    .AppendLine("public Instruction AddOpConstant(IdResultType? resultType, IdResult? resultId, LiteralFloat value)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var op = wordLength << 16 | (int)Op.OpConstant;")
                        .AppendLine("Add(op);")
                        .AppendLine("Add(resultType);")
                        .AppendLine("Add(resultId);")
                        .AppendLine("Add(value);")
                        .AppendLine("return new(this, Count-1);")
                    .Dedent()
                    .AppendLine("}");
            }
            else if (opname == "OpSpecConstant")
            {
                code
                    .AppendLine("public Instruction AddOpSpecConstant(IdResultType? resultType, IdResult? resultId, LiteralInteger value)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var op = wordLength << 16 | (int)Op.OpSpecConstant;")
                        .AppendLine("Add(op);")
                        .AppendLine("Add(resultType);")
                        .AppendLine("Add(resultId);")
                        .AppendLine("Add(value);")
                        .AppendLine("return new(this, Count-1);")
                    .Dedent()
                    .AppendLine("}");

                code
                    .AppendLine("public Instruction AddOpSpecConstant(IdResultType? resultType, IdResult? resultId, LiteralFloat value)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var op = wordLength << 16 | (int)Op.OpSpecConstant;")
                        .AppendLine("Add(op);")
                        .AppendLine("Add(resultType);")
                        .AppendLine("Add(resultId);")
                        .AppendLine("Add(value);")
                        .AppendLine("return new(this, Count-1);")
                    .Dedent()
                    .AppendLine("}");
            }

            else if (op.TryGetProperty("operands", out var operands))
            {
                var parameters = ConvertOperandsToParameters(op);
                var parameterNames = ConvertOperandsToParameterNames(op);

                var paramsParameters = parameters.Where(x => x.Contains("params"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("params"));

                code
                    .Append("public Instruction Add")
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
                    .AppendLine("return new(this, Count - 1);")
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
                    .Append("public Instruction Add")
                    .Append(opname)
                    .AppendLine("()")
                    .AppendLine("{")
                    .Indent()
                        .Append("Add( 1 << 16 | (int)Op.").Append(opname).AppendLine(");")
                        .AppendLine("return new(this, Count - 1);")
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
                    .Append("public Instruction AddGLSL")
                    .Append(opname)
                    .Append('(')
                    .Append(string.Join(", ", normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(")")
                    .AppendLine("{")
                    .Indent()
                        .Append("AddOpExtInst(")
                            .Append("set, ")
                            .Append(opcode)
                            .Append(parameterNames.Any(x => x == "resultType") ? ", resultType, " : ", null")
                            .Append(parameterNames.Any(x => x == "resultId") ? ", resultId, " : ", null")
                            .Append(other.Count() > 0 ? ", " + string.Join(", ", other) : "").AppendLine(");")
                        .AppendLine("return new(this, Count - 1);")
                    .Dedent()
                    .AppendLine("}");

                
            }
        }

        
    }
}
