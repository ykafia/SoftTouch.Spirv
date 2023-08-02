using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv.Core.Parsing;


public ref struct WordBufferReader
{
    WordBuffer buffer;
    MemoryOwner<int> data => buffer.Buffer;
}