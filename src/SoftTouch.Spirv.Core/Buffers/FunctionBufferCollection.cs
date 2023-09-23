using SoftTouch.Spirv.Core.Parsing;
using System.Collections;

namespace SoftTouch.Spirv.Core.Buffers;


public class FunctionBufferCollection
{
    bool functionStarted;
    SortedList<string, WordBuffer> buffers;
    public WordBuffer? Current => functionStarted ? buffers.Values[^1] : null;

    public FunctionsInstructions Instructions => new(this);

    public int BuffersLength => buffers.Sum(static (x) => x.Value.Length);


    public FunctionBufferCollection()
    {
        functionStarted = false;
        buffers = new();
    }

    public IEnumerator<WordBuffer> GetEnumerator() => buffers.Values.GetEnumerator();


    public Instruction Insert(MutRefInstruction instruction, string? functionName = null)
    {
        if(!functionStarted)
        {
            if (instruction.OpCode != SDSLOp.OpFunction || functionName == null)
                throw new Exception("A function should be started with SDSLOp.OpFunction");
            buffers.Add(functionName, new());
            functionStarted = true;
        }
        var result = Current?.Add(instruction);
        if(instruction.OpCode == SDSLOp.OpFunctionEnd)
        {
            functionStarted = false;
        }
        return result ?? throw new Exception("The instruction was not inserted");
    }

    public struct FunctionsInstructions
    {
        FunctionBufferCollection buffers;
        public FunctionsInstructions(FunctionBufferCollection buffers)
        {
            this.buffers = buffers;
        }


        public Enumerator GetEnumerator() => new(buffers);

        public ref struct Enumerator
        {
            IEnumerator<WordBuffer> lastBuffer;
            OrderedEnumerator lastEnumerator;
            bool started;
            public Enumerator(FunctionBufferCollection buffers)
            {
                lastBuffer = buffers.GetEnumerator();
                started = false;
            }

            public Instruction Current => lastEnumerator.Current;

            public bool MoveNext()
            {
                if (!started)
                {
                    started = true;
                    if (!lastBuffer.MoveNext())
                        return false;
                    lastEnumerator = lastBuffer.Current.GetEnumerator();
                    while (!lastEnumerator.MoveNext())
                    {
                        if (!lastBuffer.MoveNext())
                            return false;
                        lastEnumerator = lastBuffer.Current.GetEnumerator();
                    }
                    return true;
                }
                else
                {
                    if (lastEnumerator.MoveNext())
                        return true;
                    else
                    {
                        while (lastBuffer.MoveNext())
                        {
                            lastEnumerator = lastBuffer.Current.GetEnumerator();
                            if (lastEnumerator.MoveNext())
                                return true;
                        }
                    }
                    return false;
                }
            }
        }
    }

    
        
}