// See https://aka.ms/new-console-template for more information
using SoftTouch.Spirv.Experiments;
using SoftTouch.Spirv.Core;
// IInstruction nop = new OpNop();
// Console.WriteLine(nop.Id);


//var doc = JsonParser.Parse/*("{\"*/hello\" : \"world\"}");
//Console.WriteLine(doc.RootElement.GetProperty("hello").GetString());

var shader = File.ReadAllBytes("../../../../../shader.spv");

var reader = new SpirvReader(shader);
Console.WriteLine("there are " + reader.GetInstructionCount() + " instructions");
var list = new List<Instruction>(64);

SpirvReader.ParseToList(shader);
var info = InstructionInfo.GetInfo(Spv.Specification.Op.OpAccessChain);

var x = 0;