using SoftTouch.Spirv.Core.Parsing;
using System.Collections;

namespace SoftTouch.Spirv.Core.Buffers;

/// <summary>
/// A collection of function buffers, usable through the MultiBuffer class
/// </summary>
public class SortedFunctionBufferCollection
{
    bool functionStarted;
    SortedList<string, SortedWordBuffer> buffers;
    public SortedWordBuffer? Current => functionStarted ? buffers.Values[^1] : null;

    public FunctionsInstructions Instructions => new(this);

    public int BuffersLength => buffers.Sum(static (x) => x.Value.Length);


    public SortedFunctionBufferCollection(FunctionBufferCollection functions)
    {
        buffers = new(functions.FunctionCount);
        foreach(var func in functions.buffers)
        {
            buffers.Add(func.Key, new(func.Value));
        }
    }

    public IEnumerator<SortedWordBuffer> GetEnumerator() => buffers.Values.GetEnumerator();
    
    public struct FunctionsInstructions
    {
        SortedFunctionBufferCollection buffers;
        public FunctionsInstructions(SortedFunctionBufferCollection buffers)
        {
            this.buffers = buffers;
        }


        public Enumerator GetEnumerator() => new(buffers);

        public ref struct Enumerator
        {
            IEnumerator<SortedWordBuffer> lastBuffer;
            InstructionEnumerator lastEnumerator;
            bool started;
            public Enumerator(SortedFunctionBufferCollection buffers)
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