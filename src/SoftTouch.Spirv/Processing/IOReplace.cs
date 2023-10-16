using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;
using static Spv.Specification;

namespace SoftTouch.Spirv.PostProcessing;

public struct SDSLVariableReplace : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        var userSemanticId = 0;
        foreach(var i in buffer.Declarations.UnorderedInstructions)
        {
            if(i.OpCode == SDSLOp.OpSDSLIOVariable)
            {

                var sclassv = i.GetOperand<LiteralInteger>("storageclass");
                var sclass = StorageClass.Private;
                if(sclassv != null)
                    sclass = (StorageClass)sclassv.Value.Words;
                var variable = buffer.Declarations.AddOpVariable(i.GetOperand<IdResultType>("resultType") ?? -1, sclass, i.GetOperand<IdRef>("initializer"));
                buffer.Declarations.AddOpName(variable, i.GetOperand<LiteralString>("name") ?? $"var{Guid.NewGuid()}");
                SetOpNop(i.Words.Span);
            }
            else if (i.OpCode == SDSLOp.OpSDSLVariable)
            {

                var sclassv = i.GetOperand<LiteralInteger>("storageclass");
                var sclass = StorageClass.Private;
                if (sclassv != null)
                    sclass = (StorageClass)sclassv.Value.Words;
                var variable = buffer.Declarations.AddOpVariable(i.GetOperand<IdResultType>("resultType") ?? -1, sclass, i.GetOperand<IdRef>("initializer"));
                buffer.Declarations.AddOpName(variable, i.GetOperand<LiteralString>("name") ?? $"var{Guid.NewGuid()}");
                SetOpNop(i.Words.Span);
            }
        }
        foreach(var (_,f) in buffer.Functions)
        {
            foreach (var i in f.UnorderedInstructions)
            {
                if (i.OpCode == SDSLOp.OpSDSLVariable)
                {

                    var sclassv = i.GetOperand<LiteralInteger>("storageclass");
                    var sclass = StorageClass.Private;
                    if (sclassv != null)
                        sclass = (StorageClass)sclassv.Value.Words;
                    var name = i.GetOperand<LiteralString>("name");
                    var resultType = i.GetOperand<IdResultType>("resultType") ?? -1;
                    var initializer = i.GetOperand<IdRef>("initializer");
                    var variable = f.AddOpVariable(resultType, sclass, initializer);
                    f.AddOpName(variable, name ?? $"var{Guid.NewGuid()}");
                    SetOpNop(i.Words.Span);
                }
            }
        }
    }
    static void SetOpNop(Span<int> words)
    {
        words[0] = words.Length << 16;
        words[1..].Clear();
    }
}