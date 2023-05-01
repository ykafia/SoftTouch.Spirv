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

    public partial class SPVGenerator
    {

        public static void GenerateExtensions(GeneratorExecutionContext context)
        {

            var spec = context.Compilation.GetTypeByMetadataName("Spv.Specification");
            var types = spec.GetTypeMembers().Where(x => x.TypeKind == TypeKind.Enum).Select(x => x.Name).Where(x => x != null);
            var code = new CodeWriter();

            code
                .AppendLine("using static Spv.Specification;")
                .AppendLine("")
                .AppendLine("namespace SoftTouch.Spirv.Core;")
                .AppendLine("")
                .AppendLine("public static partial class LiteralExtensions")
                .AppendLine("{")
                .Indent();
            foreach (var type in types)
            {
                code.Append("public static int GetWordLength(this ").Append(type).AppendLine("? v) => v.HasValue? 1 : 0;");
                code.Append("public static int GetWordLength(this ").Append(type).AppendLine(" v) => 1;");
            }

            code
                .Dedent()
                .AppendLine("}");

            //context.AddSource("LiteralExtensions.gen.cs", code.ToString());

        }
    }
}
