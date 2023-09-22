using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;

public record Composable(string Name, SortedWordBuffer Buffer)
{
    public InstructionEnumerator GetEnumerator() => new(Buffer); 
}