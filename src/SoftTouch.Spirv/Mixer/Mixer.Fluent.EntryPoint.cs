using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public struct EntryPoint
    {
        public Mixer mixer;
        ExecutionModel executionModel;
        string name;
        public VariableBuffer variableIds = new();

        Instruction function;
        public EntryPoint(Mixer mixer, ExecutionModel executionModel, string name)
        {
            this.mixer = mixer;
            this.executionModel = executionModel;
            this.name = name;
            variableIds = new();
        }

        

        public FunctionBuilder FunctionStart()
        {
            return new(mixer,this, name);
        }

        public Mixer FinishEntryPoint()
        {
            mixer.buffer.AddOpEntryPoint(executionModel, function, name, Span<IdRef>.Empty);
            mixer.buffer.AddOpExecutionMode(
                function,
                ExecutionMode.OriginLowerLeft
            );
            mixer.buffer.AddOpCapability(Capability.Shader);
            mixer.buffer.AddOpExtInstImport("GLSL.std.450");
            mixer.buffer.AddOpMemoryModel(AddressingModel.Logical, MemoryModel.GLSL450);
            variableIds.Dispose();
            return mixer;
        }
    }
}