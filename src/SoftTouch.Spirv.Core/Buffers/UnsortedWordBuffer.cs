﻿using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Buffers;

public sealed class UnsortedWordBuffer : BufferBase<int>, ISpirvBuffer
{
    public static readonly SortedWordBuffer Empty = new ();


    public Span<int> InstructionSpan => InstructionMemory.Span;
    public Memory<int> InstructionMemory => HasHeader ? Memory[5..] : Memory;
    public bool HasHeader => Span[0] == Spv.Specification.MagicNumber;

    public int InstructionCount => new SpirvReader(Memory).Count;
    public bool IsEmpty => Span.IsEmpty;
    
    
    public Instruction this[int index]
    {
        get
        {
            var enumerator = GetEnumerator();
            int tmp = 0;
            while(enumerator.MoveNext() && tmp < index)
                tmp += 1;
            return enumerator.Current;
        }
    }

    public InstructionEnumerator GetEnumerator() => new(this);

    public UnsortedWordBuffer()
    {
        _owner = MemoryOwner<int>.Empty;
    }

    public UnsortedWordBuffer(WordBuffer buffer)
    {
        _owner = MemoryOwner<int>.Allocate(buffer.Length);
        buffer.Span.CopyTo(_owner.Span);
        Length = buffer.Length;
    }
}
