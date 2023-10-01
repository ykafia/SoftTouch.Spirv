using SoftTouch.Spirv.Core.Parsing;
using System.Transactions;

namespace SoftTouch.Spirv.Core.Buffers;



public sealed partial class MultiBuffer
{
    public MultiBufferLocalVariables LocalVariables => new(this);

    public ref struct MultiBufferLocalVariables
    {
        MultiBuffer buffer;
        public MultiBufferLocalVariables(MultiBuffer buffer)
        {
            this.buffer = buffer;
        }

        public Instruction this[string name]
        {
            get
            {
                if (buffer.Functions.Current == null)
                    throw new Exception("Not in function scope");
                var filtered = new LambdaFilteredEnumerator<WordBuffer>(buffer.Declarations, static (i) => i.OpCode == SDSLOp.OpName);
                while(filtered.MoveNext())
                {
                    if(filtered.Current.GetOperand<LiteralString>("name").Value == name)
                    {
                        var id = filtered.Current.Words.Span[1];
                        foreach(var i in buffer.Functions.Current.AsMemory())
                        {
                            if (i.ResultId == id)
                                return i;
                        }
                    }
                }
                throw new Exception($"Variable {name} does not exist");
            }
        }

        public readonly bool TryGet(string name, out Instruction instruction)
        {
            if (buffer.Functions.Current == null)
                throw new Exception("Not in function scope");
            var filtered = new LambdaFilteredEnumerator<WordBuffer>(buffer.Declarations, static (i) => i.OpCode == SDSLOp.OpName);
            while (filtered.MoveNext())
            {
                if (filtered.Current.GetOperand<LiteralString>("name").Value == name)
                {
                    var id = filtered.Current.Words.Span[1];
                    foreach (var i in buffer.Functions.Current.AsMemory())
                    {
                        if (i.ResultId == id)
                        {
                            instruction = i;
                            return true;
                        }
                    }
                }
            }
            instruction = Instruction.Empty;
            return false;
        }
    }
}