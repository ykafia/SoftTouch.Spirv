using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;



public ref struct MixinTypes
{
    Mixin mixin;
    public MixinTypes(Mixin mixin)
    {
        this.mixin = mixin;
    }

    public TypeEnumerator GetEnumerator() => new(mixin.Instructions.GetEnumerator());

    public ref struct TypeEnumerator
    {
        InstructionEnumerator enumerator;

        public TypeEnumerator(InstructionEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        public Instruction Current => enumerator.Current;
        
        public bool MoveNext()
        {
            var result = true;
            while(
                enumerator.Current.OpCode switch 
                {
                    SDSLOp.OpTypeVoid
                    or SDSLOp.OpTypeBool
                    or SDSLOp.OpTypeInt
                    or SDSLOp.OpTypeFloat
                    or SDSLOp.OpTypeVector
                    or SDSLOp.OpTypeMatrix
                    or SDSLOp.OpTypeImage
                    or SDSLOp.OpTypeSampler
                    or SDSLOp.OpTypeSampledImage
                    or SDSLOp.OpTypeArray
                    or SDSLOp.OpTypeRuntimeArray
                    or SDSLOp.OpTypeStruct
                    or SDSLOp.OpTypeOpaque
                    or SDSLOp.OpTypePointer
                    or SDSLOp.OpTypeFunction
                    or SDSLOp.OpTypeEvent
                    or SDSLOp.OpTypeDeviceEvent
                    or SDSLOp.OpTypeReserveId
                    or SDSLOp.OpTypeQueue
                    or SDSLOp.OpTypePipe
                    or SDSLOp.OpTypeForwardPointer => true,
                    _ => false
                }
            )
            {
                result = enumerator.MoveNext();
            }
            return result;
        }
    }
}
