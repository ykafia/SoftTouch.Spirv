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
                if(TryGet(name, out var instruction))
                    return instruction;
                throw new Exception($"Variable {name} not found");
            }
        }

        public readonly bool TryGet(string name, out Instruction instruction)
        {
            if (buffer.Functions.Current == null)
                throw new Exception("Not in function scope");
            var filtered = new LambdaFilteredEnumerator<WordBuffer>(buffer.Functions.Current, static (i) => i.OpCode == SDSLOp.OpSDSLVariable || i.OpCode == SDSLOp.OpSDSLFunctionParameter);
            while (filtered.MoveNext())
            {
                var vname = filtered.Current.GetOperand<LiteralString>("name");
                if (vname?.Value == name)
                {
                    instruction = filtered.Current;
                    return true;
                }
            }
            instruction = Instruction.Empty;
            return false;
        }
    }
}