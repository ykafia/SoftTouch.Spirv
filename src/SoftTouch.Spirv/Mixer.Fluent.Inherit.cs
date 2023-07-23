using SoftTouch.Spirv.Core;

namespace SoftTouch.Spirv;


public partial class Mixer
{
    public ref struct Inheritance
    {
        Mixer mixer;
        public Inheritance(Mixer mixer)
        {
            this.mixer = mixer;
        }

        public Inheritance Inherit(string name)
        {
            mixer.Inherit(name);
            return this;
        }
        public Mixer FinishInherit()
        {
            var offset = 0;
            foreach(var m in mixer.mixins)
            {
                // We add the offset

                offset += m.Bound;

                // And remove part of the offset since Ids have already been offsetted when they were created with inherited mixins
                //var maxParents = 0;
                //foreach(var p in m.Parents.ToGraph())
                //{
                //    if(maxParents < p.Bound)
                //        maxParents = p.Bound;
                //}
                //offset -= maxParents;
            }
            mixer.buffer.SetBoundOffset(offset);
            return mixer;
        }
    }
}