//using Microsoft.CodeAnalysis;
//using System;
//using System.Linq;

//namespace SoftTouch.Spirv.Generators
//{

//    [Generator]
//    public class OperandGenerator : ISourceGenerator
//    {
//        public void Execute(GeneratorExecutionContext context)
//        {
//            var code = new CodeWriter();
//            code
//            .AppendLine("using CommunityToolkit.HighPerformance.Buffers;")
//            .AppendLine("using System.Numerics;")
//            .AppendLine("namespace SoftTouch.Spirv.Internals;")
//            .AppendLine("")
//            .Append("public partial class OperandArray")
//            .AppendLine("{")
//            .Indent();

//            Enumerable.Range(1, 17).ToList().ForEach(x => GenerateMethod(x, code));

//            code
//            .Dedent()
//            .AppendLine("}");


//            context.AddSource("OperandArray.g.cs", code.ToString());
//        }

//        public void Initialize(GeneratorInitializationContext context)
//        {
//        }

//        public static void GenerateMethod(int number, CodeWriter code)
//        {
//            if (number <= 16)
//            {
//                var parameters = string.Join(", ", Enumerable.Range(0, number).Select(x => "int operand" + x));
//                var names = string.Join(", ", Enumerable.Range(0, number).Select(x => "operand" + x));

//                code
//                .Append("public void Add(")
//                .Append(parameters)
//                .AppendLine(")")
//                .AppendLine("{")
//                .Indent()
//                .Append("Length += ").Append(number.ToString()).AppendLine(";")
//                .AppendLine("if(data.Length < Length)")
//                .AppendLine("{")
//                .Indent()
//                .AppendLine("data.Dispose();")
//                .AppendLine("data = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)Length));")
//                .Dedent()
//                .AppendLine("}")
//                .Append("stackalloc int[] {")
//                    .Append(names)
//                    .Append("}.CopyTo(this[(Length - ")
//                    .Append(number + 1)
//                    .AppendLine(")..(Length - 1)]);")
//                .Dedent()
//                .AppendLine("}");
//            }
//            else
//            {
//                var parameters = string.Join(", ", Enumerable.Range(0, 16).Select(x => "int operand" + x));
//                var names = string.Join(", ", Enumerable.Range(0, 16).Select(x => "operand" + x));

//                code
//                .Append("public void Add(")
//                .Append(parameters)
//                .AppendLine(", params int[] operands)")
//                .AppendLine("{")
//                .Indent()
//                .Append("Length += ").Append(number).AppendLine(";")
//                .AppendLine("if(data.Length < Length)")
//                .AppendLine("{")
//                .Indent()
//                .AppendLine("data.Dispose();")
//                .AppendLine("data = MemoryOwner<int>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)Length));")
//                .Dedent()
//                .AppendLine("}")
//                .Append("stackalloc int[] {")
//                    .Append(names)
//                    .Append("}.CopyTo(this[(Length - ")
//                    .Append(number + 1)
//                    .Append(")..(Length - ").Append(number - 15).AppendLine(")]);")
//                .Dedent()
//                .AppendLine("}");
//            }
//        }
//    }
//}
