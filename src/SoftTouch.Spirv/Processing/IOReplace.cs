using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;
using static Spv.Specification;

namespace SoftTouch.Spirv.PostProcessing;

public struct IOReplace : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        var userSemanticId = 0;
        foreach(var i in buffer.Declarations.UnorderedInstructions)
        {
            if(i.OpCode == Core.SDSLOp.OpSDSLIOVariable)
            {
                var variable = buffer.Declarations.AddOpVariable(i.GetOperand<IdResultType>("resultType"), (StorageClass)i.Words.Span[3], i.Words.Length == 5 ? i.Words.Span[4] : null);
                buffer.Declarations.AddOpName(variable, i.GetOperand<LiteralString>("name"));
                SetOpNop(i.Words.Span);
            }
        }
    }
    static void SetOpNop(Span<int> words)
    {
        words[0] = words.Length << 16;
        words[1..].Clear();
    }
}