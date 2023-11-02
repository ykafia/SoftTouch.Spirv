using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Processing;
using static Spv.Specification;

namespace SoftTouch.Spirv.PostProcessing;

public struct IOVariableDecorator : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        foreach(var i in buffer.Declarations)
        {
            if(i.OpCode == SDSLOp.OpSDSLIOVariable)
            {
                var execution = (ExecutionModel)(i.GetOperand<LiteralInteger>("executionModel")?.Words ?? -1);
                var storage = (StorageClass)(i.GetOperand<LiteralInteger>("storageclass")?.Words ?? -1);
                var semantic = i.GetOperand<LiteralString>("semanticName")?.Value ?? throw new NotImplementedException();
                if (semantic == "SV_Position")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1, 
                        Decoration.BuiltIn, 
                        (storage,execution) switch
                        {
                            (StorageClass.Input, ExecutionModel.Fragment) => (int)BuiltIn.FragCoord,
                            (StorageClass.Input or StorageClass.Output, _) 
                                => (int)BuiltIn.Position,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_ClipDistance")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Input or StorageClass.Output, _)
                                => (int)BuiltIn.ClipDistance,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_CullDistance")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Input or StorageClass.Output, _)
                                => (int)BuiltIn.CullDistance,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_VertexID")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Input, ExecutionModel.Vertex)
                                => (int)BuiltIn.VertexIndex,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_InstanceID")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Input, ExecutionModel.Vertex)
                                => (int)BuiltIn.InstanceIndex,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_Depth" || semantic == "SV_DepthGreaterEqual" || semantic == "SV_DepthLessEqual")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Output,ExecutionModel.Fragment)
                                => (int)BuiltIn.FragDepth,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_IsFrontFace")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (StorageClass.Input, ExecutionModel.Fragment)
                                => (int)BuiltIn.FrontFacing,
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                else if (semantic == "SV_GroupThreadID")
                {
                    buffer.AddOpDecorate(
                        i.ResultId ?? -1,
                        Decoration.BuiltIn,
                        (storage, execution) switch
                        {
                            (
                                StorageClass.Input, 
                                ExecutionModel.GLCompute 
                                or ExecutionModel.MeshEXT
                                or ExecutionModel.MeshNV
                                or ExecutionModel.TaskEXT
                                or ExecutionModel.TaskNV
                            )
                                => (int)BuiltIn.LocalInvocationId,
                            _ => throw new NotImplementedException()
                        }
                    );
                }

            }
        }
    }
}