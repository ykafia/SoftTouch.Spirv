// See https://aka.ms/new-console-template for more information
using SoftTouch.Spirv.Experiments;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance.Buffers;
// IInstruction nop = new OpNop();
// Console.WriteLine(nop.Id);


//var doc = JsonParser.Parse/*("{\"*/hello\" : \"world\"}");
//Console.WriteLine(doc.RootElement.GetProperty("hello").GetString());

var shader = File.ReadAllBytes("../../../../../shader.spv");

var data = MemoryOwner<int>.Allocate(shader.Length / 4);


var bytes = shader.AsSpan();

var ints = MemoryMarshal.Cast<byte,int>(bytes);

ints.CopyTo(data.Span);

var list = SpirvReader.ParseToList(data);

var x = 0;