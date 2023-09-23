using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Runtime.InteropServices;

namespace SoftTouch.Spirv.Core.Buffers;

public sealed partial class WordBuffer : ExpandableBuffer<int>, ISpirvBuffer
{
    public Bound Bound { get; private set; }
    public int InstructionCount => new SpirvReader(Memory).Count;

    public Span<int> InstructionSpan => Span;

    public Memory<int> InstructionMemory => Memory;

    public bool HasHeader => false;

    public Memory<int> this[Range range] => Memory[range];

    public Instruction this[int index]
    {
        get
        {
            int id = 0;
            int wid = 0;
            while (id < index)
            {
                wid += Span[wid] >> 16;
                id++;
            }
            return new Instruction(this, Memory[wid..(wid+Span[wid] >> 16)],index);
        }
    }
    public WordBuffer()
    {
        Bound = new();
        _owner = MemoryOwner<int>.Allocate(32, AllocationMode.Clear);
    }

    public WordBuffer(int initialCapacity = 32, int offset = 0)
    {
        Bound = new() { Offset = offset };
        _owner = MemoryOwner<int>.Allocate(initialCapacity, AllocationMode.Clear);
    }

    internal WordBuffer(Span<int> words)
    {
        _owner = MemoryOwner<int>.Allocate(words.Length, AllocationMode.Clear);
        Length = words.Length;
        words.CopyTo(Span);
        Bound = new();
    }

    public OrderedEnumerator GetEnumerator() => new(this);


    public int GetNextId()
    {
        Bound = Bound with { Count = Bound.Count + 1 };
        return Bound.Count + Bound.Offset;
    }
    public void SetBoundOffset(int offset)
    {
        Bound = Bound with { Offset = offset };
    }




    public void Insert(Instruction instruction)
    {
        Insert(Length, instruction.Words.Span);
    }
    public void Insert(Span<int> instructions)
    {
        Insert(Length, instructions);
    }


    public Instruction Add(MutRefInstruction instruction)
    {
        Add(instruction.Words);
        return new(this, InstructionCount - 1);
    }

    public Instruction Duplicate(RefInstruction instruction)
    {
        var m = new MutRefInstruction(stackalloc int[instruction.WordCount]);
        m.OpCode = instruction.OpCode;
        m.WordCount = instruction.WordCount;
        instruction.Operands.CopyTo(m.Words[1..]);
        Add(m);
        return new(this, InstructionCount - 1);
    }


    public byte[] GenerateSpirv()
    {
        var output = new byte[Length * 4 + 5 * 4];
        var span = output.AsSpan();
        var ints = MemoryMarshal.Cast<byte, int>(span);
        var instructionWords = ints[5..];

        var header = new SpirvHeader(new SpirvVersion(1, 3), 0, Bound.Count + 1);
        header.WriteTo(ints[0..5]);
        var id = 0;
        var enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            var curr = enumerator.Current;
            curr.Words.Span.CopyTo(instructionWords.Slice(id, curr.Words.Length));
            id += curr.Words.Length;
        }
        return output;
    }


    internal static int GetWordLength<T>(T? value)
    {
        if (value is null) return 0;

        return value switch
        {
            LiteralInteger i => i.WordCount,
            LiteralFloat i => i.WordCount,
            int _ => 1,
            IdRef _ => 1,
            IdResultType _ => 1,
            string v => new LiteralString(v).WordLength,
            LiteralString v => v.WordLength,
            int[] a => a.Length,
            Enum _ => 1,
            _ => throw new NotImplementedException()
        };
    }

    public SpirvSpan AsSpan() => new(Span);
    public SpirvMemory AsMemory() => new(this);

    internal static int GetWordLength(Span<int> values) => values.Length;
    internal static int GetWordLength(Span<LiteralInteger> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<LiteralFloat> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<IdRef> values) => values.Length;
    internal static int GetWordLength(Span<PairIdRefIdRef> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairIdRefLiteralInteger> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairLiteralIntegerIdRef> values) => values.Length * 2;


    public override string ToString()
    {
        return Disassembler.Disassemble(this);
    }
}
