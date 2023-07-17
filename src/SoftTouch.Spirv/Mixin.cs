using System.Runtime.CompilerServices;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;





public partial struct Mixin
{
    public static readonly Mixin Empty = new("", SortedWordBuffer.Empty);

    public string Name { get; init; }
    public int Bound { get; init; }

    internal SortedWordBuffer Buffer { get; }
    public MixinInstructions Instructions => new(this);
    public FullMixinInstructions FullInstructions => new(this);
    public MixinParents Parents => new(this);

    public bool IsEmpty => Buffer.IsEmpty;


    public Mixin(string name, SortedWordBuffer wordBuffer)
    {
        Name = name;
        Buffer = wordBuffer;
        foreach (var i in Instructions)
            if(i.ResultId != null && i.ResultId > Bound)
                Bound = i.ResultId.Value;
    }

    public string Disassemble()
    {
        var words = new WordBuffer();
        foreach(var e in FullInstructions)
        {
            words.Insert(e);
        }
        return new Core.Disassembler().Disassemble(new UnsortedWordBuffer(words));
    }


    public override string ToString()
    {
        var dis = new Core.Disassembler();
        return dis.Disassemble(Buffer.Memory);
    }
}