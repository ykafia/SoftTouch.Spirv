// using Microsoft.CodeAnalysis;
// using System;
// using System.Linq;

// namespace SoftTouch.Spirv.Generators
// {

//     [Generator]
//     public class OperandGenerator : ISourceGenerator
//     {
//         public void Execute(GeneratorExecutionContext context)
//         {
//             var code = new CodeWriter();
//             var cases =
//                string.Join(
//                    "\n     ",
//                    Enumerable.Range(1, 17).Select(x => GenerateMethod(x, code))
//                );
//             code
//             .AppendLine("using CommunityToolkit.HighPerformance.Buffers;")
//             .AppendLine("namespace SoftTouch.Spirv.Internals;")
//             .AppendLine("")
//             .Append("public partial class OperandArray")
//             .AppendLine("{")
//             // .Indent()
//             // .AppendLine(cases)
//             // .Dedent()
//             .AppendLine("}");


//             context.AddSource("OperandArray.g.cs", code.ToString());
//         }

//         public void Initialize(GeneratorInitializationContext context)
//         {
//         }

//         public static string GenerateMethod(int number, CodeWriter code)
//         {
//             if (number <= 16)
//             {
//                 var parameters = string.Join(", ", Enumerable.Range(0, number).Select(x => "int operand" + x));
//                 var names = string.Join(", ", Enumerable.Range(0, number).Select(x => "operand" + x));

//                 code
//                 .Append("public void Add(")
//                 .Append(parameters)
//                 .AppendLine(")")
//                 .AppendLine("{")
//                 .Indent()
//                 .Append("Length += ").Append(number.ToString()).AppendLine(";")
//                 .AppendLine("if(data.Length < Length)")
//                 .AppendLine("{")
//                 .Indent()
//                 .AppendLine("data.Dispose();")
//                 .AppendLine("data = MemoryOwner<int>.Allocate(data.Length * 2);")
//                 .Dedent()
//                 .AppendLine("}")
//                 .Append("stackalloc int[] {")
//                     .Append(names)
//                     .Append("}.CopyTo(this[(Length - ")
//                     .Append(number + 1)
//                     .AppendLine(")..(Length - 1)]);")
//                 .Dedent()
//                 .AppendLine("}");
//                 return code.ToString();
//             }
//             else
//             {
//                 var parameters = string.Join(", ", Enumerable.Range(0, 16).Select(x => "int operand" + x));
//                 var names = string.Join(", ", Enumerable.Range(0, 16).Select(x => "operand" + x));

//                 code
//                 .Append("public void Add(")
//                 .Append(parameters)
//                 .AppendLine(", params int[] operands)")
//                 .AppendLine("{")
//                 .Indent()
//                 .Append("Length += ").Append(number).AppendLine(";")
//                 .AppendLine("if(data.Length < Length)")
//                 .Indent()
//                 .AppendLine("data.Dispose();")
//                 .AppendLine("data = MemoryOwner<int>.Allocate(data.Length * 2);")
//                 .Dedent()
//                 .AppendLine("}")
//                 .Append("stackalloc int[] {")
//                     .Append(names)
//                     .Append("}.CopyTo(this[(Length - ")
//                     .Append(number + 1)
//                     .Append(")..(Length - ").Append(number - 15).AppendLine(")]);")
//                 .Dedent()
//                 .AppendLine("}");
//                 return code.ToString();
//             }
//         }
//     }
// }
