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
            var nop = spirvCore.RootElement.GetProperty("instructions").EnumerateArray().First();
            context.AddSource(
                "Instructions.gen.cs",
                @$"
namespace SoftTouch.Spirv.Internals;

public class OpNop : IInstruction
{{
    public static int Op => {nop.GetProperty("opcode").GetInt32()};
    public static string OpName {{ get; }} = ""{nop.GetProperty("opname").GetString()}"";

    public int Id => Op;
    public string Name => OpName;

    public OperandArray? Operands {{get;}}

    public OpNop() {{}}

    public void Dispose()
    {{
        Operands?.Dispose();
    }}
}}
                "
            );
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
