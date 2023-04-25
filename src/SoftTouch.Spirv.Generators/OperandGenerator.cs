using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Generators
{

    [Generator]
    public class OperandGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var cases =
                string.Join(
                    "\n     ",
                    Enumerable.Range(1, 17).Select(GenerateMethod)
                );
            var file = @$"
using CommunityToolkit.HighPerformance.Buffers;
namespace SoftTouch.Spirv.Internals;

public partial class OperandArray
{{
    {cases}
}}

";

            context.AddSource("OperandArray.g.cs", file);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public static string GenerateMethod(int number)
        {
            if (number <= 16)
            {
                var parameters = string.Join(", ", Enumerable.Range(0, number).Select(x => "int operand" + x));
                var names = string.Join(", ", Enumerable.Range(0, number).Select(x => "operand" + x));
                return @$"
    public void Add({parameters})
    {{
        Length += {number};
        if(data.Length < Length)
        {{
            data.Dispose();
            data = MemoryOwner<int>.Allocate(data.Length * 2);
        }}
        stackalloc int[] {{ {names} }}.CopyTo(this[(Length - {number + 1})..(Length - 1)]);
    }}
";
            }
            else
            {
                var parameters = string.Join(", ", Enumerable.Range(0, 16).Select(x => "int operand" + x));
                var names = string.Join(", ", Enumerable.Range(0, 16).Select(x => "operand" + x));
                return @$"
    public void Add({parameters}, params int[] operands)
    {{
        Length += {number};
        if(data.Length < Length)
        {{
            data.Dispose();
            data = MemoryOwner<int>.Allocate(data.Length * 2);
        }}
        stackalloc int[] {{ {names} }}.CopyTo(this[(Length - {number + 1})..(Length - {number - 15})]);
        operands.AsSpan().CopyTo(this[(Length - {number - 15})..(Length - 1)]);
    }}
";
            }
        }
        public static string GenerateConstructors(int number)
        {
            if (number <= 16)
            {
                var parameters = string.Join(", ", Enumerable.Range(0, number).Select(x => "int operand" + x));
                var names = string.Join(", ", Enumerable.Range(0, number).Select(x => "operand" + x));
                return @$"
    public OperandArray({parameters})
    {{
        Length = {number};
        data = MemoryOwner<int>.Allocate(Length);
        stackalloc int[] {{ {names} }}.CopyTo(data.Slice(0,Length).Span);
    }}
";
            }
            else
            {
                var parameters = string.Join(", ", Enumerable.Range(0, 16).Select(x => "int operand" + x));
                var names = string.Join(", ", Enumerable.Range(0, 16).Select(x => "operand" + x));
                return @$"
    public OperandArray({parameters}, params int[] operands)
    {{
        Length = 15 + operands.Length;
        data = MemoryOwner<int>.Allocate(Length);
        stackalloc int[] {{ {names} }}.CopyTo(data.Slice(0,15).Span);
        operands.AsSpan().CopyTo(data.Slice(15,Length).Span);
        
    }}
";
            }
        }
    }
}
