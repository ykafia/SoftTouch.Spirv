namespace SoftTouch.Spirv.Internals;


public interface ISpirvElement
{
    public void Write(scoped ref SpirvWriter writer);
}