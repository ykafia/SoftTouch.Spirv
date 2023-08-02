using SoftTouch.Spirv.Core;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct EntryPoint
    {
        Mixer mixer;
        ExecutionModel executionModel;
        string name;
        ExpandableBuffer<int> variableIds;
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
            return mixer;
        }



        public ref struct Function
        {
            EntryPoint entryPoint;
            Mixer Mixer => entryPoint.mixer;
            WordBuffer Buffer => Mixer.buffer;

            ExpandableBuffer<(string, int)> variables;
            public Function(EntryPoint entryPoint)
            {
                variables = new();
                this.entryPoint = entryPoint;
                var t_void = Mixer.GetOrCreateBaseType("void");
                var t_func = Buffer.AddOpTypeFunction(t_void.ResultId ?? -1, Span<IdRef>.Empty);
                Buffer.AddOpFunction(t_void.ResultId ?? -1, FunctionControlMask.MaskNone, t_func.ResultId ?? -1);
                Buffer.AddOpLabel();
            }


            public Function Declare(string type, string name)
            {
                var t_variable = Mixer.GetOrCreateBaseType(type);
                var variable = Buffer.AddOpVariable(t_variable.ResultId ?? -1, StorageClass.Function, null);
                Buffer.AddOpName(variable.ResultId ?? -1, name);
                variables.Add((name, variable.ResultId ?? -1));
                return this;
            }

            public Function StoreInt(string name, int value)
            {
                var cst = Mixer.CreateConstant($"cst_{Buffer.Bound}", value);
                foreach((string n, int id) in variables)
                {
                    if(n == name)
                    {
                        Buffer.AddOpStore(cst.ResultId ?? -1, id, null);
                        return this;
                    }
                }
                return this;
            }


            public Function Return()
            {
                Buffer.AddOpReturn();
                return this;
            }

            public EntryPoint FunctionEnd()
            {
                Buffer.AddOpFunctionEnd();
                return entryPoint;
            }
        }
    }
}