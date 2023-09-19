using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct FunctionBuilder
    {

        public delegate IdRef InitializerDelegate(Mixer mixer, ref FunctionBuilder functionBuilder);

        Mixer mixer;
        Instruction function;
        EntryPoint? entryPoint;

        public FunctionBuilder(Mixer mixer, string returnType, string name, int parameterNumber, ParameterTypesDelegate parameterTypesDelegate)
        {
            this.mixer = mixer;
            Span<IdRef> parameters = stackalloc IdRef[parameterNumber];
            var p = new ParameterBuilder(mixer, parameters);
            parameterTypesDelegate.Invoke(ref p);
            var t = mixer.GetOrCreateBaseType(returnType.AsMemory());
            var t_func = mixer.buffer.AddOpTypeFunction(t.ResultId ?? -1, parameters);
            function = mixer.buffer.AddOpFunction(t.ResultId ?? -1, FunctionControlMask.MaskNone,t_func);
            mixer.buffer.AddOpName(function.ResultId ?? -1, name);
            mixer.buffer.AddOpLabel();

        }
        public FunctionBuilder(Mixer mixer, EntryPoint entryPoint)
        {
            this.mixer = mixer;
            this.entryPoint = entryPoint;
            var t = mixer.GetOrCreateBaseType("void".AsMemory());
            var t_func = mixer.buffer.AddOpTypeFunction(t.ResultId ?? -1, Span<IdRef>.Empty);
            function = mixer.buffer.AddOpFunction(t.ResultId ?? -1, FunctionControlMask.MaskNone, t_func);
            mixer.buffer.AddOpName(function.ResultId ?? -1, entryPoint.name);
            mixer.buffer.AddOpLabel();

        }

        public FunctionBuilder Declare(string type, string name)
        {
            throw new NotImplementedException();
            return this;
        }

        public FunctionBuilder DeclareAssign(string type, string name, InitializerDelegate initializer)
        {
            var resultType = mixer.GetOrCreateBaseType(type.AsMemory());
            var ptr = mixer.buffer.AddOpTypePointer(StorageClass.Function, resultType.ResultId ?? -1);
            var value = initializer.Invoke(mixer, ref this);
            var variable = mixer.buffer.AddOpVariable(ptr.ResultId ?? -1, StorageClass.Function, value);
            mixer.buffer.AddOpName(variable.ResultId ?? -1, name);
            return this;
        }
        public FunctionBuilder Assign(string name, InitializerDelegate initializer)
        {
            var result = initializer.Invoke(mixer, ref this);
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
            if (entryPoint != null)
            {
                //mixer.buffer.AddOpEntryPoint()
            }
            return mixer;
        }

        public delegate void ParameterTypesDelegate(ref ParameterBuilder typeIds);

        public ref struct ParameterBuilder
        {
            Span<IdRef> typeIds;
            Mixer mixer;
            int currentIndex;

            public ParameterBuilder(Mixer mixer, Span<IdRef> typeIds)
            {
                this.mixer = mixer;
                this.typeIds = typeIds;
                currentIndex = 0;
            }

            public ParameterBuilder With(string typeName)
            {
                if (currentIndex >= typeIds.Length)
                    throw new Exception("Too many arguments were given");
                var t = mixer.GetOrCreateBaseType(typeName.AsMemory());
                typeIds[currentIndex] = t.ResultId ?? -1;
                currentIndex += 1;
                return this;
            }
            public void Finish() { }
        }

        public ref struct VariableFinder
        {
            FunctionBuilder builder;

            public VariableFinder(FunctionBuilder builder)
            {
                this.builder = builder;
            }

            public ref struct Enumerator
            {
                RefInstruction function;
                OrderedEnumerator enumerator;
                bool inFunction;

                public Enumerator(VariableFinder finder)
                {
                    enumerator = finder.builder.mixer.buffer.GetEnumerator();
                    function = finder.builder.function.AsRef();
                    inFunction = false;
                }


                public RefInstruction Current => RefInstruction.Empty;


                public bool MoveNext()
                {
                    while(!inFunction && enumerator.MoveNext())
                    {
                        //if(
                        //    enumerator.Current.OpCode == function.OpCode 
                        //    && enumerator.Current)
                    }
                    return true;
                    
                }
            }
        }
    }
}