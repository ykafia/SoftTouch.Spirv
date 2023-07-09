using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    // public Mixin? Module { get; protected set; }
    MixinGraph mixins;
    public string Name { get; init; }
    WordBuffer buffer;

    public Mixer(string name)
    {
        Name = name;
        buffer = new();
        buffer.AddOpSDSLMixinName(Name);
        mixins = new();
    }

    public Mixin Build()
    {
        var length = buffer.BufferLength;
        var boundOffset = 0;
        foreach (var m in mixins)
        {
            length += m.Buffer.Span.Length;
            boundOffset += m.Bound;
        }
        var final = new WordBuffer(length);
        var bufferEnum = buffer.GetEnumerator();
        var mixinsEnum = mixins.Instructions.GetEnumerator();
        
        while(bufferEnum.MoveNext())
        {
            if (mixinsEnum.MoveNext())
            {
                while (InstructionInfo.GetGroupOrder(mixinsEnum.Current) <= InstructionInfo.GetGroupOrder(bufferEnum.Current))
                {
                    final.Insert(mixinsEnum.Current);
                    if (!mixinsEnum.MoveNext())
                        break;
                }
            }
        }
        while(mixinsEnum.MoveNext())
            final.Insert(mixinsEnum.Current)
            
        

        // var notEmpty = bufferEnum.MoveNext();
        // foreach(var instruction in mixins.Instructions)
        // {

        // if(notEmpty)
        // {
        //     while(
        //         // InstructionInfo.GetGroupOrder(bufferEnum.Current.OpCode) < InstructionInfo.GetGroupOrder(instruction.OpCode)
        //         &&
        //         bufferEnum.MoveNext() 
        //         )
        //     {
        //         final.Insert(bufferEnum.Current.AsRef(boundOffset));
        //     }
        // }
        // else
        // final.Insert(instruction);
        // }
        foreach (var instruction in buffer)
            final.Insert(instruction.AsRef(boundOffset));
        MixinSourceProvider.Register(new(Name, new(final)));
        return MixinSourceProvider.Get(Name);
    }

    public Mixer Inherit(string mixin)
    {
        mixins.Add(mixin);
        buffer.AddOpSDSLMixinInherit(mixin);
        return this;
    }

    public Mixer WithType(string type)
    {
        GetOrCreateBaseType(type);
        return this;
    }

    public override string ToString()
    {
        var dis = new Disassembler();
        return dis.Disassemble(buffer);
    }
}