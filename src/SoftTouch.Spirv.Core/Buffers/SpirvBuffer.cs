using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Buffers;

public class SpirvBuffer : ExpandableBuffer<int>, ISpirvBuffer, IDisposable
{
    public Span<int> InstructionSpan => _owner.Span[5..Length];
    public Memory<int> InstructionMemory => _owner.Memory[5..Length];
    public RefHeader Header => new(_owner.Span[..5]);
    public RefInstructions Instructions => new(InstructionMemory);
    public bool HasHeader => true;

    public Instruction this[int index]
    {
        get
        {
            int id = 0;
            int wid = 5;
            while (id < index)
            {
                wid += Span[wid] >> 16;
                id++;
            }
            return new Instruction(this, Memory.Slice(wid, Span[wid] >> 16),index);
        }
    }

    public SpirvBuffer()
    {
        _owner = MemoryOwner<int>.Allocate(32,AllocationMode.Clear);
        var header = Header;
        header.MagicNumber = Spv.Specification.MagicNumber;
        header.VersionNumber = new(1,3);
        header.GeneratorMagicNumber = 42;
        Length = 5;
    }

    public RefInstructions.Enumerator GetEnumerator() => Instructions.GetEnumerator();

    public void Add(SortedWordBuffer buffer) => Add(buffer.Span);

    public void Replace(SpirvBuffer buffer, out bool dispose)
    {
        if(buffer.Length <= Length)
        {
            _owner.Span.Clear();
            buffer.Span.CopyTo(Span);
            Length = buffer.Length;
            dispose = true;
        }
        else 
        {
            var disp = _owner;
            _owner = buffer._owner;
            Length = buffer.Length;
            disp.Dispose();
            dispose = false;
        }
    }

    public void RecomputeBound()
    {
        int last = 0;
        foreach(var i in this)
        {
            last = i.ResultId ?? last;
        }
        var header = Header;
        header.Bound = last + 1;
    }

    public SortedWordBuffer ToSorted()
    {
        return new(this);
    }

    public override string ToString()
    {
        return Disassembler.Disassemble(this);
    }    
}
