using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;
using static Spv.Specification;

namespace SoftTouch.Spirv.PostProcessing;

public struct SDSLVariableReplace : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        var inputSemanticLocation = -1;
        var outputSemanticLocation = -1;
        foreach (var i in buffer.Declarations.UnorderedInstructions)
        {
            if (i.OpCode == SDSLOp.OpSDSLIOVariable)
            {

                var sclassv = i.GetOperand<LiteralInteger>("storageclass");
                var sclass = StorageClass.Private;
                if (sclassv != null)
                    sclass = (StorageClass)sclassv.Value.Words;

                var semantic = i.GetOperand<LiteralString>("semantic");
                var variable = buffer.Declarations.AddOpVariable(i.GetOperand<IdResultType>("resultType") ?? -1, sclass, i.GetOperand<IdRef>("initializer"));
                variable.Operands.Span[1] = i.ResultId ?? -1;
                if (sclass == StorageClass.Input)
                {
                    inputSemanticLocation += 1;
                    buffer.Declarations.AddOpDecorate(variable, Decoration.Location, inputSemanticLocation);
                }
                else if (sclass == StorageClass.Output)
                {
                    outputSemanticLocation += 1;
                    buffer.Declarations.AddOpDecorate(variable, Decoration.Location, outputSemanticLocation);
                }
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
                variable.Operands.Span[1] = i.ResultId ?? -1;
                buffer.Declarations.AddOpName(variable, i.GetOperand<LiteralString>("name") ?? $"var{Guid.NewGuid()}");
                SetOpNop(i.Words.Span);
            }
        }
        foreach (var (n, f) in buffer.Functions)
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
                    var resultType = i.ResultType ?? -1;
                    var initializer = i.GetOperand<IdRef>("initializer");
                    var variable = f.AddOpVariable(resultType, sclass, initializer);
                    variable.Operands.Span[1] = i.ResultId ?? -1;
                    buffer.Declarations.AddOpName(variable, name ?? $"var{Guid.NewGuid()}");
                    SetOpNop(i.Words.Span);
                    f.RecomputeLength();
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