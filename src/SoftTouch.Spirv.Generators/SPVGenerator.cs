﻿using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

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
            .AppendLine("namespace SoftTouch.Spirv.Core.Buffers;")
            .AppendLine("")
            .AppendLine("public static class WordBufferExtensions")
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
                "WordBufferExtensions.gen.cs",
                code.ToString()
            );
        }

        public void CreateOperation(JsonElement op, CodeWriter code)
        {
            var opname = op.GetProperty("opname").GetString();
            if (opname == "OpConstant")
            {
                code
                    .AppendLine("public static Instruction AddOpConstant<TBuffer, TValue>(this TBuffer buffer, IdResultType? resultType, TValue value) where TBuffer : IMutSpirvBuffer where TValue : ILiteralNumber")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var resultId = buffer.GetNextId();")
                        .AppendLine("var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpConstant, ..resultType.AsSpanOwner().Span, resultId, ..value.AsSpanOwner().Span]));")
                    .Dedent()
                    .AppendLine("}");

            }
            else if (opname == "OpSpecConstant")
            {
                code
                    .AppendLine("public static Instruction AddOpSpecConstant<TBuffer, TValue>(this TBuffer buffer, IdResultType? resultType, TValue value) where TBuffer : IMutSpirvBuffer where TValue : ILiteralNumber")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var resultId = buffer.GetNextId();")
                        .AppendLine("var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + value.WordCount;")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpSpecConstant;")
                        .AppendLine("mutInstruction.Add(resultType);")
                        .AppendLine("mutInstruction.Add(resultId);")
                        .AppendLine("mutInstruction.Add(value);")
                        .AppendLine("return buffer.Add(mutInstruction);")
                    .Dedent()
                    .AppendLine("}");
            }
            else if(opname.StartsWith("OpDecorate"))
            {
                code
                    .Append("public static Instruction Add").Append(opname).Append("<TBuffer>(this TBuffer buffer, IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);")
                        .AppendLine("return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpDecorate, target, (int)decoration, ..additional1.AsSpanOwner().Span, ..additional2.AsSpanOwner().Span, ..additionalString.AsSpanOwner().Span]));")
                    .Dedent()
                    .AppendLine("}");
            }
            else if(opname.StartsWith("OpMemberDecorate"))
            {
                code
                    .Append("public static Instruction Add").Append(opname).Append("<TBuffer>(this TBuffer buffer, IdRef structureType, LiteralInteger member, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var wordLength = 1 + buffer.GetWordLength(structureType) + buffer.GetWordLength(member) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);")
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);")
                        .AppendLine("mutInstruction.OpCode = SDSLOp.OpMemberDecorate;")
                        .AppendLine("mutInstruction.Add(structureType);")
                        .AppendLine("mutInstruction.Add(member);")
                        .AppendLine("mutInstruction.Add(decoration);")
                        .AppendLine("mutInstruction.Add(additional1);")
                        .AppendLine("mutInstruction.Add(additional2);")
                        .AppendLine("mutInstruction.Add(additionalString);")
                        .AppendLine("return buffer.Add(mutInstruction);")
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
                    .Append("public static Instruction Add")
                    .Append(opname)
                    .Append("<TBuffer>(this TBuffer buffer")
                    .Append(normalParameters.Count() + nullableParameters.Count() + paramsParameters.Count() == 0 ? "" : ", ")
                    .Append(string.Join(", ", normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(") where TBuffer : IMutSpirvBuffer")
                    .AppendLine("{")
                    .Indent();
                if(hasResultId)
                {
                    code.AppendLine("var resultId = buffer.GetNextId();");
                }
                code.Append("var wordLength = 1").Append(parameterNames.Any() ? " + " : "").Append(string.Join(" + ", parameterNames.Select(x => $"buffer.GetWordLength({x})"))).AppendLine(";");
                code.AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);");
                code.Append("mutInstruction.OpCode = SDSLOp.").Append(opname).AppendLine(";");
                
                foreach(var p in parameterNames)
                {
                    code.Append("mutInstruction.Add(").Append(p).AppendLine(");");
                }


                code
                    .AppendLine("return buffer.Add(mutInstruction);")
                    .Dedent().AppendLine("}");
            }
            else
            {
                code
                    .Append("public static Instruction Add")
                    .Append(opname)
                    .AppendLine("<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var mutInstruction = new MutRefInstruction(stackalloc int[1]);")
                        .Append("mutInstruction.OpCode = SDSLOp.").Append(opname).AppendLine(";")
                        .AppendLine("return buffer.Add(mutInstruction);")
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
                    .Append("public static Instruction AddGLSL")
                    .Append(opname)
                    .Append("<TBuffer>(this TBuffer buffer, ")
                    .Append("IdResultType resultType, ")
                    .Append(string.Join(", ", normalParameters))
                    .Append(nullableParameters.Count() == 0 ? "" : (normalParameters.Count() > 0 ? ", " : "") + string.Join(", ", nullableParameters))
                    .Append(paramsParameters.Count() == 0 ? "" : (normalParameters.Count() + nullableParameters.Count() > 0 ? ", " : "") + paramsParameters.First())
                    .AppendLine(") where TBuffer : IMutSpirvBuffer")
                    .AppendLine("{")
                    .Indent()
                        .AppendLine("var resultId = buffer.GetNextId();")
                        .Append("Span<IdRef> refs = stackalloc IdRef[]{").Append(string.Join(", ", other)).AppendLine("};")
                        .AppendLine("if(buffer is MultiBuffer mb)")
                        .Indent()
                            .Append("return mb.AddOpExtInst(")
                                .Append("set, ")
                                .Append(opcode)
                                .Append(", resultId, resultType ")
                                .AppendLine(", refs);")
                        .Dedent()
                        .AppendLine("else if (buffer is WordBuffer wb)")
                        .Indent()
                            .Append("return wb.AddOpExtInst(")
                                .Append("set, ")
                                .Append(opcode)
                                .Append(", resultId, resultType ")
                                .AppendLine(", refs);")
                        .Dedent()
                        .AppendLine("else return Instruction.Empty;")
                    .Dedent()
                    .AppendLine("}");
            }
        }
    }
}
