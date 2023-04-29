using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Internals;

namespace SoftTouch.Spirv;

public class SpvFunction : SpvSnippet
{
    int count = 0;

    Dictionary<int, int> InstructionMap = new();
    Dictionary<string, MemoryOwner<Instruction>> Functions;
}
