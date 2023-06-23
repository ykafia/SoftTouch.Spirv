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
        JsonDocument spirvSDSL;

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
            
            string resourceSDSLName =
                assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("spirv.sdsl.grammar-ext.json"));

            spirvCore = JsonDocument.Parse(new StreamReader(assembly.GetManifestResourceStream(resourceCoreName)).ReadToEnd());
            spirvGlsl = JsonDocument.Parse(new StreamReader(assembly.GetManifestResourceStream(resourceGlslName)).ReadToEnd());
            spirvSDSL = JsonDocument.Parse(new StreamReader(assembly.GetManifestResourceStream(resourceSDSLName)).ReadToEnd());
        
        }


        public void Execute(GeneratorExecutionContext context)
        {

            CreateInfo(context);
            CreateSDSLOp(context);

            var code = new CodeWriter();

            code
            .AppendLine("using static Spv.Specification;")
            .AppendLine("namespace SoftTouch.Spirv.Core;")
            .AppendLine("")
            .AppendLine("public partial struct WordBuffer")
            .AppendLine("{")
            .Indent();

            var instructions = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().ToList();
            var sdslInstructions = spirvSDSL.RootElement.GetProperty("instructions").EnumerateArray().ToList();
            var glslInstruction = spirvGlsl.RootElement.GetProperty("instructions").EnumerateArray().ToList();

            instructions.ForEach(x => CreateOperation(x, code));
            sdslInstructions.ForEach(x => CreateOperation(x, code));
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
                    .AppendLine("public Instruction AddOpConstant<T>(IdResultType? resultType, T value) where T : ILiteralNumber")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var resultId = bound.Next();")
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpConstant;")
                        .AppendLine("mutInstruction.Add(resultType);")
                        .AppendLine("mutInstruction.Add(resultId);")
                        .AppendLine("mutInstruction.Add(value);")
                        .AppendLine("return Add(mutInstruction);")
                    .Dedent()
                    .AppendLine("}");

            }
            else if (opname == "OpSpecConstant")
            {
                code
                    .AppendLine("public Instruction AddOpSpecConstant<T>(IdResultType? resultType, T value) where T : ILiteralNumber")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var resultId = bound.Next();")
                        .AppendLine("var wordLength = 1 + GetWordLength(resultType) + GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpSpecConstant;")
                        .AppendLine("mutInstruction.Add(resultType);")
                        .AppendLine("mutInstruction.Add(resultId);")
                        .AppendLine("mutInstruction.Add(value);")
                        .AppendLine("return Add(mutInstruction);")
                    .Dedent()
                    .AppendLine("}");
            }
            else if(opname.StartsWith("OpDecorate"))
            {
                code
                    .Append("public Instruction Add").Append(opname).AppendLine("(IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(target) + GetWordLength(decoration) + GetWordLength(additional1) + GetWordLength(additional2) + GetWordLength(additionalString);")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpDecorate;")
                        .AppendLine("mutInstruction.Add(target);")
                        .AppendLine("mutInstruction.Add(decoration);")
                        .AppendLine("mutInstruction.Add(additional1);")
                        .AppendLine("mutInstruction.Add(additional2);")
                        .AppendLine("mutInstruction.Add(additionalString);")
                        .AppendLine("return Add(mutInstruction);")
                    .Dedent()
                    .AppendLine("}");
            }
            else if(opname.StartsWith("OpMemberDecorate"))
            {
                code
                    .Append("public Instruction Add").Append(opname).AppendLine("(IdRef structureType, LiteralInteger member, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null)")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + GetWordLength(structureType) + GetWordLength(member) + GetWordLength(decoration) + GetWordLength(additional1) + GetWordLength(additional2) + GetWordLength(additionalString);")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpMemberDecorate;")
                        .AppendLine("mutInstruction.Add(structureType);")
                        .AppendLine("mutInstruction.Add(member);")
                        .AppendLine("mutInstruction.Add(decoration);")
                        .AppendLine("mutInstruction.Add(additional1);")
                        .AppendLine("mutInstruction.Add(additional2);")
                        .AppendLine("mutInstruction.Add(additionalString);")
                        .AppendLine("return Add(mutInstruction);")
                    .Dedent()
                    .AppendLine("}");
            }

            else if (op.TryGetProperty("operands", out var operands))
            {
                var parameters = ConvertOperandsToParameters(op);
                var parameterNames = ConvertOperandsToParameterNames(op);
                var hasResultId = parameterNames.Contains("resultId") && opname != "OpExtInst";
                if(hasResultId)
                {
                    parameters.Remove(parameters.First(x => x.Contains("resultId")));
                }
                var paramsParameters = parameters.Where(x => x.Contains("Span"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("Span"));

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
                if(hasResultId)
                {
                    code.AppendLine("var resultId = bound.Next();");
                }
                code.Append("var wordLength = 1").Append(parameterNames.Any() ? " + " : "").Append(string.Join(" + ", parameterNames.Select(x => $"GetWordLength({x})"))).AppendLine(";");
                code.AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);");
                code.Append("mutInstruction.OpCode = SDSLOp.").Append(opname).AppendLine(";");
                
                foreach(var p in parameterNames)
                {
                    code.Append("mutInstruction.Add(").Append(p).AppendLine(");");
                }


                code
                    .AppendLine("return Add(mutInstruction);")
                    .Dedent().AppendLine("}");
            }
            else
            {
                code
                    .Append("public Instruction Add")
                    .Append(opname)
                    .AppendLine("()")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[1]);")
                        .Append("mutInstruction.OpCode = SDSLOp.").Append(opname).AppendLine(";")
                        .AppendLine("return Add(mutInstruction);")
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

                var hasResultId = parameterNames.Contains("resultId");

                if(hasResultId)
                {
                    parameters.Remove(parameters.First(x => x.Contains("resultId")));
                }

                var paramsParameters = parameters.Where(x => x.Contains("Span"));
                var nullableParameters = parameters.Where(x => x.Contains("?"));
                var normalParameters = parameters.Where(x => !x.Contains("?") && !x.Contains("Span"));
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
                        .AppendLine("var resultId = bound.Next();")
                        .Append("Span<IdRef> refs = stackalloc IdRef[]{").Append(string.Join(", ", other)).AppendLine("};")
                        .Append("return AddOpExtInst(")
                            .Append("set, ")
                            .Append(opcode)
                            .Append(parameterNames.Any(x => x == "resultId") ? ", resultId, " : ", null")
                            .Append(parameterNames.Any(x => x == "resultType") ? ", resultType, " : ", null")
                            .AppendLine(", refs);")
                    .Dedent()
                    .AppendLine("}");
            }
        }
    }
}
