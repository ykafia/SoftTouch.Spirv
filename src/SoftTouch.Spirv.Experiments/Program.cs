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

    var data = MemoryOwner<int>.Allocate(shader.Length / 4, AllocationMode.Clear);
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
    var orders = InstructionInfo.Instance.OrderGroup;
    Span<int> values = stackalloc int[20];
    var start = values[..4];
    start.Fill(1);

    var middle = values[4..14];
    middle.Fill(2);
    
    var end = values[14..];
    end.Fill(4);

    Span<int> endCopy = stackalloc int[end.Length];
    end.CopyTo(endCopy);
    
    end.Clear();
    middle.CopyTo(values[end.Length..]);
    endCopy.CopyTo(values);
    var y = 1;


    // var bound = new Bound();
    // var buffer = new WordBuffer();

    // // Capabilities

    // buffer.AddOpCapability(Capability.Shader);
    // var extInstImport = buffer.AddOpExtInstImport("GLSL.std.450");
    // buffer.AddOpMemoryModel(AddressingModel.Logical,MemoryModel.GLSL450);
    // Span<IdRef> v = stackalloc IdRef[]{31,33,42,57};
    // buffer.AddOpEntryPoint(ExecutionModel.Fragment,4,"main",v);
    // buffer.AddOpExecutionMode(4,ExecutionMode.OriginLowerLeft);
    
    // //Debug

    // buffer.AddOpSource(SourceLanguage.GLSL,450,null,null);
    // buffer.AddOpName(4,"main");
    // buffer.AddOpName(9,"scale");
    // buffer.AddOpName(17,"S");
    // buffer.AddOpMemberName(17,0,"b");
    // buffer.AddOpMemberName(17,1,"v");
    // buffer.AddOpMemberName(17,2,"i");
    // buffer.AddOpName(18,"blockName");
    // buffer.AddOpMemberName(18,0,"s");
    // buffer.AddOpMemberName(18,1,"cond");
    // buffer.AddOpName(20,"");
    // buffer.AddOpName(31,"color");
    // buffer.AddOpName(33,"color1");
    // buffer.AddOpName(42,"color2");
    // buffer.AddOpName(48,"i");
    // buffer.AddOpName(57,"multiplier");

    // // Annotations

    // buffer.AddOpDecorate(15, Decoration.ArrayStride, 16);
    // buffer.AddOpMemberDecorate(17, 0, Decoration.Offset, 0);
    // buffer.AddOpMemberDecorate(17, 1, Decoration.Offset, 16);
    // buffer.AddOpMemberDecorate(17, 2, Decoration.Offset, 96);
    // buffer.AddOpMemberDecorate(18, 0, Decoration.Offset, 0);
    // buffer.AddOpMemberDecorate(18, 1, Decoration.Offset, 112);
    // buffer.AddOpDecorate(18, Decoration.Block);
    // buffer.AddOpDecorate(20, Decoration.DescriptorSet, 0);
    // buffer.AddOpDecorate(42, Decoration.NoPerspective);

    // // declarations

    // Span<IdRef> c = stackalloc IdRef[10]; // This is for use in parameters


    // var typeVoid = buffer.AddOpTypeVoid();
   
    // buffer.AddOpTypeFunction(2, Span<IdRef>.Empty);
    // buffer.AddOpTypeFloat(32);
    // buffer.AddOpTypeVector(6, 4);
    // buffer.AddOpTypePointer(StorageClass.Function, 7);
    // buffer.AddOpConstant(10, 6, 1f);
    // buffer.AddOpConstant(11, 6, 2f);

    // var composite = c.Slice(0,4);
    // composite[0] = 10;
    // composite[1] = 10;
    // composite[2] = 11;
    // composite[3] = 10;
    // buffer.AddOpConstantComposite(12, 7, c);
    // composite.Clear();

    
    // buffer.AddOpTypeInt(13, 32, 0);
    // buffer.AddOpConstant(14, 13, 5f);
    // buffer.AddOpTypeArray(15, 7, 14);
    // buffer.AddOpTypeInt(16, 32, 1);
    // var structParams = c.Slice(0,3);
    // structParams[0] = 13;
    // structParams[1] = 15;
    // structParams[2] = 16;
    // buffer.AddOpTypeStruct(17, structParams);
    // structParams.Clear();
    // structParams = c.Slice(0,2);
    // structParams[0] = 17;
    // structParams[1] = 13;
    // buffer.AddOpTypeStruct(18, structParams);
    // buffer.AddOpTypePointer(19, StorageClass.Uniform, 18);
    // buffer.AddOpVariable(20, 19, StorageClass.Uniform,null);
    // buffer.AddOpConstant(21, 16, 1f);
    // buffer.AddOpTypePointer(22, StorageClass.Uniform, 13);
    // buffer.AddOpTypeBool(25);
    // buffer.AddOpConstant(26, 13, 0f);
    // buffer.AddOpTypePointer(30, StorageClass.Output, 7);
    // buffer.AddOpVariable(31, 30, StorageClass.Output, null);
    // buffer.AddOpTypePointer(32, StorageClass.Input, 7);
    // buffer.AddOpVariable(33, 32, StorageClass.Input, null);
    // buffer.AddOpConstant(35, 16, 0f);
    // buffer.AddOpConstant(36, 16, 2f);
    // buffer.AddOpTypePointer(37, StorageClass.Uniform, 7);
    // buffer.AddOpVariable(42, 32, StorageClass.Input,null);
    // buffer.AddOpTypePointer(47, StorageClass.Function, 16);
    // buffer.AddOpConstant(55, 16, 4f);
    // buffer.AddOpVariable(57, 32, StorageClass.Input,null);

    var x = 0;
}


CreateShader();
