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
            .AppendLine("namespace SoftTouch.Spirv.Internals;")
            .AppendLine("")
            .AppendLine("public partial struct Instruction")
            .AppendLine("{")
            .Indent();

            var instructions = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().Take(4).ToList();

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
            var parameters = "";
            if (op.TryGetProperty("operands", out var operands))
            {
                parameters = string.Join(", ", operands.EnumerateArray().Select(ConvertOperandToParameter).Where(x => x != null));
            }

            code
                .Append("public static Instruction ")
                .Append(op.GetProperty("opname").GetString())
                .Append('(')
                .Append(parameters)
                .AppendLine(")")
                .AppendLine("{")
                .Indent()
                .AppendLine("var z = 0;")
                .AppendLine("return new Instruction();")
                .Dedent()
                .AppendLine("}");
        }


        public static string ConvertOperandToParameter(JsonElement element)
        {
            var kind = element.GetProperty("kind").GetString();
            string getName() => FirstCharToUpperAsSpan(element.GetProperty("name").GetString().Replace("'", "").Replace(" ", ""));

            if (kind == "IdResult") return "int resultId";
            if (kind == "IdResultType") return "int resultType";
            if (kind == "IdRef") return $"int {getName()}";
            if (kind == "LiteralInteger") return $"int {getName()}";
            if (kind == "LiteralFloat") return $"float {getName()}";
            if (kind == "LiteralString") return $"string {getName()}";
            if (kind == "Dim") return "Dim dimension";
            if (kind == "ImageFormat") return "ImageFormat imageFormat";
            if (kind == "ExecutionMode") return "ExecutionMode executionMode";
            if (kind == "ExecutionModel") return "ExecutionModel executionModel";
            return null; //throw new NotImplementedException($"Operand {element.GetProperty("kind").GetString()} not recognized")

        }
        public static string FirstCharToUpperAsSpan(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            Span<char> destination = stackalloc char[1];
            input.AsSpan(0, 1).ToLowerInvariant(destination);
            return $"{destination.ToString()}{input.AsSpan(1).ToString()}";
        }
    }
}
