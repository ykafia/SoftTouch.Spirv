using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct Function
    {
        Mixer mixer;
        EntryPoint? entryPoint;
        public Function(Mixer mixer) 
        { 
            this.mixer = mixer; 
        }
        public Function(Mixer mixer, EntryPoint entrypoint)
        {
            this.mixer = mixer;
            this.entryPoint = entrypoint;
        }

        public Function Declare(string type, string name)
        {
            throw new NotImplementedException();
            return this;
        }

        public Function DeclareAssign(string type, string name, Func<Mixer,IdRef> function)
        {
            var result = function.Invoke(mixer);
            throw new NotImplementedException();
        }
        public Function OpenBlock()
        {
            mixer.buffer.AddOpLabel();
            return this;
        }
        public Function OpenBlock()
        {
            throw new NotImplementedException();
            return this;
        }
    }
}