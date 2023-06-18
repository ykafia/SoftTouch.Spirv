using SoftTouch.Spirv.Core;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.Spirv;


public struct OffsettedBuffer
{
    readonly MemoryOwner<int> buffer;
    public int BufferLength { get; private set; }

    public readonly Span<int> Span => buffer.Span;
    internal OffsettedBuffer(int offset, WordBuffer wbuff)
    {
        buffer = MemoryOwner<int>.Allocate(wbuff.BufferLength, AllocationMode.Clear);
        BufferLength = 0;
        foreach (var i in wbuff)
        {
            Add(i.Words, offset);
        }
    }
    internal void Add(Span<int> words, int offset = 0)
    {
        var destination = Span[(BufferLength - 1)..(BufferLength - 1 + words.Length)];
        words.CopyTo(destination);
        var instruction = RefInstruction.ParseRef(destination);
        if(offset > 0)
            instruction.Offset(offset);
        BufferLength += words.Length;
    }
}

public static class WordBufferExtensions
{
    public static OffsettedBuffer ToOffsettedBuffer(this WordBuffer buffer, int offset)
    {
        var result = new OffsettedBuffer(offset, buffer);
        return result;
    }
}