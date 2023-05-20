﻿// See https://aka.ms/new-console-template for more information
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


    // var bound = new Bound();
    var buffer = new WordBuffer();
    // // Capabilities

    buffer.AddOpCapability(Capability.Shader);
    var extInstImport = buffer.AddOpExtInstImport("GLSL.std.450");
    buffer.AddOpMemoryModel(AddressingModel.Logical, MemoryModel.GLSL450);


    // declarations

    Span<IdRef> c = stackalloc IdRef[10]; // This is for use in parameters


    var t_void = buffer.AddOpTypeVoid();

    var t_bool = buffer.AddOpTypeBool();

    var t_func = buffer.AddOpTypeFunction(t_void, Span<IdRef>.Empty);
    var t_float = buffer.AddOpTypeFloat(32);
    var t_uint = buffer.AddOpTypeInt(32,0);
    var t_int = buffer.AddOpTypeInt(32,1);
    var t_float4 = buffer.AddOpTypeVector(t_float, 4);
    var t_p_float4_func = buffer.AddOpTypePointer(StorageClass.Function, t_float4);
    var constant1 = buffer.AddOpConstant(t_float, 5f);
    var constant2 = buffer.AddOpConstant(t_float, 2f);
    var constant3 = buffer.AddOpConstant(t_uint, 5);
    var compositeType = buffer.AddOpConstantComposite(t_float4, stackalloc IdRef[] { constant1, constant1, constant2, constant1 });

    var t_array = buffer.AddOpTypeArray(t_float4, constant3);

    var t_struct = buffer.AddOpTypeStruct(stackalloc IdRef[] { t_uint, t_array, t_int });
    var t_struct2 = buffer.AddOpTypeStruct(stackalloc IdRef[] { t_struct, t_uint });

    var t_p_struct2 = buffer.AddOpTypePointer(StorageClass.Uniform, t_struct2);

    var v_struct2 = buffer.AddOpVariable(t_p_struct2, StorageClass.Uniform, null);

    var constant4 = buffer.AddOpConstant(t_int, 1);

    var t_p_uint = buffer.AddOpTypePointer(StorageClass.Uniform, t_uint);
    var constant5 = buffer.AddOpConstant(t_uint, 0);

    var t_p_output = buffer.AddOpTypePointer(StorageClass.Output, t_float4);
    var v_output = buffer.AddOpVariable(t_p_output, StorageClass.Output, null);

    var t_p_input = buffer.AddOpTypePointer(StorageClass.Input, t_float4);
    var v_input = buffer.AddOpVariable(t_p_input, StorageClass.Input, null);

    var constant6 = buffer.AddOpConstant(t_int, 0);
    var constant7 = buffer.AddOpConstant(t_int, 2);
    var t_p_float4_unif = buffer.AddOpTypePointer(StorageClass.Uniform, t_float4);

    var v_input_2 = buffer.AddOpVariable(t_p_input, StorageClass.Input, null);
    var t_p_func = buffer.AddOpTypePointer(StorageClass.Function, t_int);
    var constant8 = buffer.AddOpConstant(t_int, 4);
    var v_input_3 = buffer.AddOpVariable(t_p_input, StorageClass.Input, null);


    buffer.AddOpEntryPoint(ExecutionModel.Fragment, t_p_func, "main", stackalloc IdRef[] { v_output, v_input, v_input_2, v_input_3 });
    buffer.AddOpExecutionMode(t_p_func, ExecutionMode.OriginLowerLeft);


    buffer.AddOpDecorate(t_array, Decoration.ArrayStride, 16);
    buffer.AddOpMemberDecorate(t_struct, 0, Decoration.Offset, 0);
    buffer.AddOpMemberDecorate(t_struct, 1, Decoration.Offset, 16);
    buffer.AddOpMemberDecorate(t_struct, 2, Decoration.Offset, 96);
    buffer.AddOpMemberDecorate(t_struct2, 0, Decoration.Offset, 0);
    buffer.AddOpMemberDecorate(t_struct2, 1, Decoration.Offset, 112);
    buffer.AddOpDecorate(t_struct2, Decoration.Block);
    buffer.AddOpDecorate(v_struct2, Decoration.DescriptorSet, 0);
    buffer.AddOpDecorate(v_input_2, Decoration.NoPerspective);




    buffer.AddOpName(t_p_func, "main");
    buffer.AddOpName(t_struct, "S");
    buffer.AddOpMemberName(t_struct, 0, "b");
    buffer.AddOpMemberName(t_struct, 1, "v");
    buffer.AddOpMemberName(t_struct, 2, "i");


    buffer.AddOpFunction(t_void, FunctionControlMask.MaskNone, t_func);
    buffer.AddOpLabel();
    buffer.AddOpReturn();
    buffer.AddOpFunctionEnd();

    var list = new List<Instruction>(buffer.Count);

    foreach(var e in buffer)
    {
        list.Add(e);
    }

    var x = 0;
}


CreateShader();
