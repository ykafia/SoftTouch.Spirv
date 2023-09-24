using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;

namespace SoftTouch.Spirv;

/// <summary>
/// A mixin that can be composed with others
/// </summary>
/// <param name="Name"></param>
/// <param name="Buffer"></param>
public record Composable(string Name, SortedWordBuffer Buffer)
{
    public InstructionEnumerator GetEnumerator() => new(Buffer); 
}