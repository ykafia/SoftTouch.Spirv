using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using Spv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;


public record struct FunctionInfo(string Name,IdRef Id, IdResultType Type);


public partial class Mixer
{
    public ref struct Functions
    {

        Mixer mixer;
        public Functions(Mixer mixer)
        {
            this.mixer = mixer;
        }

        public FunctionInfo this[string name]
        {
            get
            {
                //foreach (var function in Span)
                //{
                //    if (function.Name == name)
                //        return function;
                //}
                throw new KeyNotFoundException($"{name} was not found.");
            }
        }

        public ref struct Enumerator
        {
            public Mixer mixer;
            bool started;
            bool mixinsFound;

            public Enumerator(Mixer mixer)
            {
                this.mixer = mixer;
                started = false;
            }
            public (IdRef, IdResultType) Current => 0;

            public bool MoveNext()
            {
                if(!started)
                {
                    if (mixer.mixins.Instructions.Count + mixer.buffer.InstructionCount == 0)
                        return false;
                    return true;
                }
                else
                {
                    var count = 
                }
                return false;
            }
        }
    }
}
