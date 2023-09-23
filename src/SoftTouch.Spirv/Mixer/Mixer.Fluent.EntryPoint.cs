using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public struct EntryPoint
    {
        public Mixer mixer;
        public ExecutionModel ExecutionModel { get; }
        public string Name { get; }

        Instruction function;
        public EntryPoint(Mixer mixer, ExecutionModel executionModel, string name)
        {
            this.mixer = mixer;
            ExecutionModel = executionModel;
            Name = name;
        }

        

        public FunctionBuilder FunctionStart()
        {
            return new(mixer,this);
        }

        public Mixer FinishEntryPoint()
        {
            mixer.buffer.AddOpEntryPoint(ExecutionModel, function, Name, Span<IdRef>.Empty);
            mixer.buffer.AddOpExecutionMode(
                function,
                ExecutionMode.OriginLowerLeft
            );
            mixer.buffer.AddOpCapability(Capability.Shader);
            mixer.buffer.AddOpExtInstImport("GLSL.std.450");
            mixer.buffer.AddOpMemoryModel(AddressingModel.Logical, MemoryModel.GLSL450);
            return mixer;
        }
    }
}