// See https://aka.ms/new-console-template for more information
using SoftTouch.Spirv.Experiments;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;
// IInstruction nop = new OpNop();
// Console.WriteLine(nop.Id);


//var doc = JsonParser.Parse/*("{\"*/hello\" : \"world\"}");
//Console.WriteLine(doc.RootElement.GetProperty("hello").GetString());

var shader = File.ReadAllBytes("../../../../../shader.spv");

var bytes = shader.AsSpan();

var ints = MemoryMarshal.Cast<byte,int>(bytes);

var x = 0;