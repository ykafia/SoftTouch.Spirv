// See https://aka.ms/new-console-template for more information
using SoftTouch.Spirv.Experiments;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.CompilerServices;
using static Spv.Specification;
// IInstruction nop = new OpNop();
Console.WriteLine("Hello, world!");


//var doc = JsonParser.Parse/*("{\"*/hello\" : \"world\"}");
//Console.WriteLine(doc.RootElement.GetProperty("hello").GetString());

static void ParseShader()
{
    Console.WriteLine(Unsafe.SizeOf<Memory<int>>());

    InstructionInfo.GetInfo(Op.OpCapability);

    var shader = File.ReadAllBytes("../../shader.spv");

    var data = MemoryOwner<int>.Allocate(shader.Length / 4);
    var slice = data.Memory[..5];
    var slice2 = data.Memory[5..10];

    var bytes = shader.AsSpan();

    var ints = MemoryMarshal.Cast<byte, int>(bytes);

    ints.CopyTo(data.Span);

    var list = SpirvReader.ParseToList(data);

    var x = 0;
}


static void CreateShader()
{
    var buffer = new WordBuffer();

    var nop = buffer.AddOpNop();
    var capability = buffer.AddOpCapability(Capability.Shader);
    capability.Get("capability", out SpvEnum<Capability> c);
    Console.WriteLine((Capability)c);
    var extInstImport = buffer.AddOpExtInstImport("GLSL.std.450", 1);
    extInstImport.Get("name", out LiteralString name);
    var x = 0;
}


CreateShader();
