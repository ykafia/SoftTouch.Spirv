using System.Runtime.CompilerServices;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;


public ref struct MixinInstructions
{
    Mixin mixin;
    public MixinInstructions(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public RefInstruction this[int index]
    {
        get
        {
            var e = GetEnumerator();
            int tmp = 0;
            while (e.MoveNext() || tmp < index)
                tmp += 1;
            if (tmp < index) return RefInstruction.Empty;
            else return e.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => mixin.Buffer.GetEnumerator();
}


public partial struct Mixin
{
    public static readonly Mixin Empty = new("", SortedWordBuffer.Empty);

    public string Name { get; init; }
    public int Bound { get; init; }

    internal SortedWordBuffer Buffer { get; }
    public MixinInstructions Instructions => new(this);
    public MixinParents Parents => new(this);

    public bool IsEmpty => Buffer.IsEmpty;


    public Mixin(string name, SortedWordBuffer wordBuffer)
    {
        Name = name;
        Buffer = wordBuffer;
        foreach (var i in Instructions)
            Bound = i.ResultId ?? 0;
    }

    public override string ToString()
    {
        var dis = new Core.Disassembler();
        return dis.Disassemble(Buffer.Memory);
    }
}