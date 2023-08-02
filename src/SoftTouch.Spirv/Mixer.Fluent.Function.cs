using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct Function
    {
        Mixer mixer;
        public Function(Mixer mixer)
        {
            this.mixer = mixer;
        }

        public Function Inherit(string name)
        {
            mixer.Inherit(name);
            return this;
        }
        public Mixer FinishFunction()
        {
            return mixer;
        }
    }
}