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

namespace SoftTouch.Spirv.Generators
{
    public partial class SPVGenerator
    {
        public void CreateSDSLOp(GeneratorExecutionContext context)
        {
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
                .OfType<EnumMemberDeclarationSyntax>();
            var lastnum = int.Parse(members.Last().EqualsValue.Value.GetText().ToString());
            
            var content = string.Join("\n",
                new[]{lastnum}
            );
            context.AddSource("nodes.g.cs", content);
        }
    }
}
