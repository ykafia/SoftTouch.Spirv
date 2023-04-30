using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv.Core;


public interface ISpirvElement
{ 
    public void Write(scoped ref SpirvWriter writer);
}