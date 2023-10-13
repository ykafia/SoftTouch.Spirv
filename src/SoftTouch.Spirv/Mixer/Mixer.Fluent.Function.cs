using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct FunctionBuilder
    {
        public record struct Variable(IdRef Id, bool IsConstant);
        public record struct Value(IdRef Id, IdResultType Type, bool IsConstant);
        public delegate Variable InitializerDelegate(Mixer mixer, ref FunctionBuilder functionBuilder);
        public delegate Value ValueDelegate(Mixer mixer, ref FunctionBuilder functionBuilder);

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
            function = mixer.buffer.AddOpSDSLFunction(t.ResultId ?? -1, FunctionControlMask.MaskNone, t_func, name);
            mixer.buffer.AddOpLabel();

        }
        public FunctionBuilder(Mixer mixer, EntryPoint entryPoint)
        {
            this.mixer = mixer;
            this.entryPoint = entryPoint;
            var t = mixer.GetOrCreateBaseType("void".AsMemory());
            var t_func = mixer.buffer.AddOpTypeFunction(t.ResultId ?? -1, Span<IdRef>.Empty);
            function = mixer.buffer.AddOpSDSLFunction(t.ResultId ?? -1, FunctionControlMask.MaskNone, t_func, entryPoint.Name);
            mixer.buffer.AddOpLabel();

        }

        public FunctionBuilder Declare(string type, string name)
        {
            var t = mixer.GetOrCreateBaseType(type.AsMemory());
            var p_t = mixer.buffer.AddOpTypePointer(StorageClass.Function, t);
            var v = mixer.buffer.AddOpVariable(p_t, StorageClass.Function, null);
            mixer.buffer.AddOpName(v, name);
            return this;
        }
        /// <summary>
        /// Declares a variable and assigns a value to it
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public FunctionBuilder DeclareAssign<T>(string name, T constant)
            where T : struct
        {
            var resultType = mixer.GetOrCreateBaseType<T>();
            var ptr = mixer.buffer.AddOpTypePointer(StorageClass.Function, resultType.ResultId ?? -1);
            var value = mixer.CreateConstant(constant);
            MixinInstruction variable;
            variable = mixer.buffer.AddOpVariable(ptr, StorageClass.Function, value.ResultId);
            mixer.buffer.AddOpName(variable, name);
            return this;
        }
        public FunctionBuilder Assign(string name, ValueDelegate initializer)
        {
            // TODO : If the value delegate is a constant no need to be loaded on a register
            var result = initializer.Invoke(mixer, ref this);
            if (mixer.LocalVariables.TryGet(name, out var local))
            {
                var load = mixer.buffer.AddOpLoad(result.Type, result.Id, null);
                mixer.buffer.AddOpStore(local, load, null);
            }
            else if (mixer.GlobalVariables.TryGet(name, out var global))
            {
                var load = mixer.buffer.AddOpLoad(result.Type, result.Id, null);
                mixer.buffer.AddOpStore(global, load, null);
            }
            return this;

        }
        public FunctionBuilder Assign(string name, IdRef value)
        {
            if (mixer.LocalVariables.TryGet(name, out var local))
                mixer.buffer.AddOpStore(local, value, null);
            else if (mixer.GlobalVariables.TryGet(name, out var global))
                mixer.buffer.AddOpStore(global, value, null);
            return this;
        }
        public FunctionBuilder AssignConstant<T>(string name, T constantValue)
            where T : struct
        {
            var constant = mixer.CreateConstant(constantValue);
            if (mixer.LocalVariables.TryGet(name, out var local))
                mixer.buffer.AddOpStore(local, constant, null);
            else if (mixer.GlobalVariables.TryGet(name, out var global))
                mixer.buffer.AddOpStore(global, constant, null);
            return this;
        }
        public FunctionBuilder AssignVariable(string destination, string source)
        {
            // TODO : If the value delegate is a constant no need to be loaded on a register
            
            Instruction src = Instruction.Empty;
            if(mixer.LocalVariables.TryGet(source,out var ls))
                src = ls;
            else if(mixer.GlobalVariables.TryGet(source, out var gs))
                src = gs;

            var srcType = mixer.FindType(src.Words.Span[1]);

            if (mixer.LocalVariables.TryGet(destination, out var local))
            {
                var load = mixer.buffer.AddOpLoad(srcType, src, null);
                mixer.buffer.AddOpStore(local, load, null);
            }
            else if (mixer.GlobalVariables.TryGet(destination, out var global))
            {
                var load = mixer.buffer.AddOpLoad(srcType, src, null);
                mixer.buffer.AddOpStore(global, load, null);
            }
            return this;
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
            var count = mixer.IOVariables.Count;
            Span<IdRef> idRefs = stackalloc IdRef[count];
            int index = 0;
            foreach (var i in mixer.IOVariables)
            {
                idRefs[index] = i;
                index += 1;
            }
            mixer.buffer.AddOpFunctionEnd();
            if (entryPoint != null)
            {
                mixer.buffer.AddOpEntryPoint(entryPoint.Value.ExecutionModel, function, entryPoint.Value.Name, idRefs);
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
    }
}