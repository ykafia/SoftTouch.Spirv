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
        public void CreateSDSLOp(GeneratorExecutionContext context)
        {
            var code = new CodeWriter();
            // TODO : syntax tree something
            // context.Compilation.SyntaxTrees.Where(tree => tree.GetRoot());
        }
    }
}
