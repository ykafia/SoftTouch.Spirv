using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Reflection;


namespace SoftTouch.Spirv.Generators
{

    [Generator]
    public class SPVGenerator : ISourceGenerator
    {
        static JsonDocument spirvCore;
        static JsonDocument spirvGlsl;

        public void Execute(GeneratorExecutionContext context)
        {
            
            var instructions = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().Take(4);
            
            string generated = string.Join("\n",instructions.Select(CreateOperation));

            var baseDefinition = @$"
namespace SoftTouch.Spirv.Internals;

public partial struct Instruction
{{
    {generated}
}}";
            context.AddSource(
                "Instructions.gen.cs",
                baseDefinition
            );
        }

        public string CreateOperation(JsonElement op)
        {
            var parameters = "";
            if(op.TryGetProperty("operands", out var operands))
            {
                parameters = string.Join(", ",operands.EnumerateArray().Select(ConvertOperandToParameter).Where(x => x != null));
            }



            return @$"
    public static void {op.GetProperty("opname").GetString()}({parameters})
    {{

    }}
";
        }


        public static string ConvertOperandToParameter(JsonElement element)
        {
            var kind = element.GetProperty("kind").GetString();
            var getName = () => FirstCharToUpperAsSpan(element.GetProperty("name").GetString().Replace("'","").Replace(" ", ""));
            return kind switch
            {
                "IdResult" => "int resultId",
                "IdResultType" => "int resultType",
                "IdRef" => $"int {getName()}",
                "LiteralInteger" => $"int {getName()}",
                "LiteralFloat" => $"float {getName()}",
                "LiteralString" => $"string {getName()}",
                "Dim" => "Dim dimension",
                "ImageFormat" => "ImageFormat imageFormat",
                "ExecutionMode" => "ExecutionMode executionMode",
                "ExecutionModel" => "ExecutionModel executionModel",
                _ => null //throw new NotImplementedException($"Operand {element.GetProperty("kind").GetString()} not recognized")
            };
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

        public void Initialize(GeneratorInitializationContext context)
        {
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
    }
}
