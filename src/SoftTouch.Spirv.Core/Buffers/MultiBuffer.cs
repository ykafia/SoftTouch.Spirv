using SoftTouch.Spirv.Core.Parsing;
using System.Transactions;

namespace SoftTouch.Spirv.Core.Buffers;


/// <summary>
/// A spirv buffer composed of many different buffers for declarations and functions
/// </summary>
public sealed partial class MultiBuffer
{
    public int Bound { get; private set; }
    public int Length => Declarations.Length + Functions.BuffersLength;

    public WordBuffer Declarations { get; init; }
    public FunctionBufferCollection Functions { get; init; }

    public MultiBufferInstructions Instructions => new(this);

    public MultiBuffer()
    {
        Declarations = new();
        Functions = new();
    }

    public Instruction Add(MutRefInstruction instruction)
    {
        if (instruction.OpCode == SDSLOp.OpSDSLFunction)
        {
            var name = instruction.GetOperand<LiteralString>("functionName");
            var id = instruction.GetOperand<IdResult>("resultId").Value;
            Declarations.AddOpName(id, name);
            Span<int> words = stackalloc int[5];
            instruction.Words[..5].CopyTo(words);
            var funcInstruction = new MutRefInstruction(words);
            funcInstruction.OpCode = SDSLOp.OpFunction;
            return Functions.Insert(funcInstruction, name.Value);
        }
        else
        {
            return InstructionInfo.GetGroupOrder(instruction) switch
            {
                13 => Functions.Insert(instruction),
                _ => Declarations.Add(instruction)
            };
        }
    }
    public Instruction Duplicate(RefInstruction instruction)
    {
        var m = new MutRefInstruction(stackalloc int[instruction.WordCount]);
        m.OpCode = instruction.OpCode;
        m.WordCount = instruction.WordCount;
        instruction.Operands.CopyTo(m.Words[1..]);
        return Add(m);
    }

    public int GetNextId()
    {
        Bound += 1;
        return Bound;
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


    public void Dispose()
    {
        Declarations.Dispose();
        foreach (var function in Functions)
            function.Dispose();
    }

    public struct MultiBufferInstructions
    {
        MultiBuffer buffers;
        public MultiBufferInstructions(MultiBuffer buffers)
        {
            this.buffers = buffers;
        }

        public Enumerator GetEnumerator() => new(buffers);
        
        public ref struct Enumerator
        {
            MultiBuffer buffers;
            OrderedEnumerator declarationEnumerator;
            FunctionBufferCollection.FunctionsInstructions.Enumerator functionsEnumerator;
            bool started;
            bool declarationsFinished;
            public Enumerator(MultiBuffer buffers)
            {
                this.buffers = buffers;
                declarationEnumerator = buffers.Declarations.GetEnumerator();
                functionsEnumerator = buffers.Functions.Instructions.GetEnumerator();
                started = false;
                declarationsFinished = false;
            }

            public Instruction Current => !declarationsFinished ? declarationEnumerator.Current : functionsEnumerator.Current;

            public bool MoveNext()
            {
                if(!started)
                {
                    started = true;
                    if (declarationEnumerator.MoveNext())
                        return true;
                    else
                        declarationsFinished = true;
                    if (functionsEnumerator.MoveNext())
                        return true;
                    return false;
                }
                else
                {
                    if(!declarationsFinished)
                    {
                        if (declarationEnumerator.MoveNext())
                            return true;
                        else
                            declarationsFinished = true;
                    }
                    return functionsEnumerator.MoveNext();
                }
            }
        }
    }


    internal static int GetWordLength(Span<int> values) => values.Length;
    internal static int GetWordLength(Span<LiteralInteger> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<LiteralFloat> values) => values.Length * values[0].WordCount;
    internal static int GetWordLength(Span<IdRef> values) => values.Length;
    internal static int GetWordLength(Span<PairIdRefIdRef> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairIdRefLiteralInteger> values) => values.Length * 2;
    internal static int GetWordLength(Span<PairLiteralIntegerIdRef> values) => values.Length * 2;
}