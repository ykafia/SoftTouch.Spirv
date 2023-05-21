using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv.Core;


public interface ISpirvElement
{ 
    public void Write(scoped ref SpirvWriter writer);
}

