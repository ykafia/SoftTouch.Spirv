using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Internals.Benchmarks;

[MemoryDiagnoser]
public class ParserBench
{
    public static byte[] shader = File.ReadAllBytes("C:\\Users\\kafia\\source\\repos\\SoftTouch.Spirv\\shader.spv");

    public ParserBench()
    {
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
        var list = SpirvReader.ParseToList(shader);
    }
}
