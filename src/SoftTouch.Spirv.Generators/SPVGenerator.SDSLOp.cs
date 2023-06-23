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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace SoftTouch.Spirv.Generators
{
    public partial class SPVGenerator
    {
        public void CreateSDSLOp(GeneratorExecutionContext context)
        {
            var cu = SyntaxFactory.CompilationUnit();
            // cu.NormalizeWhitespace();
            var code = new CodeWriter();
            // TODO : syntax tree something
            var members = context
                .Compilation
                .SyntaxTrees
                .First(t => t.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Any(x => x.Identifier.Text == "Specification"))
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .First()
                .DescendantNodes()
                .OfType<EnumDeclarationSyntax>()
                .First(x => x.Identifier.Text == "Op")
                .DescendantNodes()
                .OfType<EnumMemberDeclarationSyntax>()
                .ToDictionary(x => x.Identifier.Text, x => int.Parse(x.EqualsValue.Value.GetText().ToString()));
            var lastnum = members.Last().Value;
            foreach(var e in spirvSDSL.RootElement.GetProperty("instructions").EnumerateArray().Select(x => x.GetProperty("opname").GetString()))
                members.Add(e, ++lastnum);

            code
                .AppendLine("namespace SoftTouch.Spirv.Core;")
                .AppendLine("")
                .AppendLine("public enum SDSLOp")
                .AppendLine("{")
                .Indent();
            foreach(var e in members)
                code.Append(e.Key).Append(" = ").Append(e.Value).AppendLine(",");
            code
                .Dedent()
                .AppendLine("}");
            
            
            context.AddSource("SDSLOp.gen.cs", code.ToString());
        }
    }
}
