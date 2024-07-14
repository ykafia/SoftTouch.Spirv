using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.CompilerServices;

namespace SoftTouch.Spirv.Core;


public interface ISpirvElement
{
    public int WordCount { get; }
    public SpanOwner<int> AsSpanOwner();
}

public interface IWritableSpirvElement : ISpirvElement
{
    public void Write(scoped ref SpirvWriter writer);
}

