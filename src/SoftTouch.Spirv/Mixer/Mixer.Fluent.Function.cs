using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct FunctionBuilder
    {
        Mixer mixer;
        Instruction function;
        EntryPoint? entryPoint;
        VariableBuffer variableIds;
        public FunctionBuilder(Mixer mixer, string name) 
        { 
            this.mixer = mixer;
            variableIds = new();
        }
        public FunctionBuilder(Mixer mixer, EntryPoint entryPoint, string name)
        {
            this.mixer = mixer;
            this.entryPoint = entryPoint;
            variableIds = entryPoint.variableIds;
        }

        public FunctionBuilder Declare(string type, string name)
        {
            throw new NotImplementedException();
            return this;
        }

        public FunctionBuilder DeclareAssign(string type, string name, Func<Mixer,IdRef> function)
        {
            var result = function.Invoke(mixer);
            throw new NotImplementedException();
        }
        public FunctionBuilder Assign(string name, Func<Mixer, IdRef> function)
        {
            var result = function.Invoke(mixer);
            throw new NotImplementedException();
        }

        public FunctionBuilder CallFunction(string functionName, Span<IdRef> parameters)
        {
            throw new NotImplementedException();
            //var function = mixer.functions[functionName];
            //mixer.buffer.AddOpFunctionCall(function.Type, function.Id, parameters);
            return this;
        }
        
        public FunctionBuilder Return(IdRef? value = null)
        {
            if (value != null)
                mixer.buffer.AddOpReturnValue(value.Value);
            else
                mixer.buffer.AddOpReturn();
            return this;
        }
        public Mixer FunctionEnd()
        {
            mixer.buffer.AddOpFunctionEnd();
            if(entryPoint != null)
            {
                //mixer.buffer.AddOpEntryPoint()
            }
            return mixer;
        }
    }
}