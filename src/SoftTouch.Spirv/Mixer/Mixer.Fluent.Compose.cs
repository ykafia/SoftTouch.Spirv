using System.ComponentModel.Design.Serialization;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;

namespace SoftTouch.Spirv;

public partial class Mixer
{
    public Mixer ComposeWith(string mixinName, string variableName)
    {
        // Foreach instruction in mixin to compose
        // Insert the instruction
        // If the instruction is an OpName for variables, create a new OpName instruction with the same name prefixed by variableName
        // Make sure to offset the Ids.
        throw new NotImplementedException();
        return this;
    }


}