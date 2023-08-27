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
        ExpandableBuffer<int> variableIds;

        Instruction function;
        public EntryPoint(Mixer mixer, ExecutionModel executionModel, string name)
        {
            this.mixer = mixer;
            this.executionModel = executionModel;
            this.name = name;
            variableIds = new();
        }

        public EntryPoint WithInput(string type, string name)
        {
            var t_variable = mixer.GetOrCreateBaseType(type);
            var variable = mixer.buffer.AddOpVariable(t_variable.ResultId ?? -1, StorageClass.Input, null);
            mixer.buffer.AddOpName(variable.ResultId ?? -1, name);
            variableIds.Add(variable.ResultId ?? -1);
            return this;
        }
        public EntryPoint WithOutput(string type, string name)
        {
            var t_variable = mixer.GetOrCreateBaseType(type);
            var variable = mixer.buffer.AddOpVariable(t_variable.ResultId ?? -1, StorageClass.Output, null);
            mixer.buffer.AddOpName(variable.ResultId ?? -1, name);
            variableIds.Add(variable.ResultId ?? -1);

            return this;
        }

        public Function FunctionStart()
        {
            return new(this);
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
            

            return mixer;
        }
    }
}