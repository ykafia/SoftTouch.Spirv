using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;
using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core.Benchmarks;

[MemoryDiagnoser]
public class ParserBench
{
    public MemoryOwner<int> shader;
    public List<OwnedInstruction> instructions;

    public ParserBench()
    {
        var bytes = File.ReadAllBytes("C:/Users/youness_kafia/Documents/dotnetProjs/SoftTouch.Spirv/shader.spv");
        shader = MemoryOwner<int>.Allocate(bytes.Length / 4, AllocationMode.Clear);
        MemoryMarshal.Cast<byte, int>(bytes.AsSpan()).CopyTo(shader.Span);
        var reader = new SpirvReader(bytes);
        instructions = new(reader.Count);
        InstructionInfo.GetInfo(Spv.Specification.Op.OpCapability);

    }


    [Benchmark]
    public void MemorySlice()
    {
        var slice = shader.Memory[5..];
    }
    [Benchmark]
    public void Count()
    {
        var reader = new SpirvReader(shader);
        var count = reader.Count;
    }

    [Benchmark]
    public void Parse()
    {
        var reader = new SpirvReader(shader);
        foreach (var i in reader)
        {
            
        }
    }
    [Benchmark]
    public void ParseToList()
    {
        SpirvReader.ParseToList(shader, instructions);
        instructions.Clear();
    }
}
