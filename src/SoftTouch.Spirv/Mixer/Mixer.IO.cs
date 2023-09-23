using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{

    public IOVariablesFinder IOVariables => new(this);

    public ref struct IOVariablesFinder
    {
        Mixer mixer;

        public readonly int Count 
        { 
            get 
            {
                int result = 0;
                foreach (var i in this)
                    result += 1;
                return result;
            } 
        }
        public IOVariablesFinder(Mixer mixer)
        {
            this.mixer = mixer;
        }

        public Enumerator GetEnumerator() => new(this);

        public ref struct Enumerator
        {
            InstructionEnumerator enumerator;
            public Enumerator(IOVariablesFinder finder)
            {
                enumerator = new InstructionEnumerator(finder.mixer.buffer.Declarations);
            }

            public Instruction Current => enumerator.Current;

            public bool MoveNext()
            {
                while (enumerator.MoveNext())
                {
                    if (Current.OpCode == SDSLOp.OpVariable && (((StorageClass)Current.Words.Span[2]) == StorageClass.Input || ((StorageClass)Current.Words.Span[2]) == StorageClass.Output))
                        return true;
                }
                return false;
            }
        }
    }
}