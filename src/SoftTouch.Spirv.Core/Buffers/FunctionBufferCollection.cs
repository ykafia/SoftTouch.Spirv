using System.Collections;

namespace SoftTouch.Spirv.Core.Buffers;


public class FunctionBufferCollection
{
    bool functionStarted;
    List<WordBuffer> buffers;
    public WordBuffer? Current => functionStarted ? buffers[^1] : null;

    public FunctionBufferCollection()
    {
        functionStarted = false;
        buffers = new();
    }

    public List<WordBuffer>.Enumerator GetEnumerator() => buffers.GetEnumerator();


    public void Insert(MutRefInstruction instruction)
    {
        if(!functionStarted)
        {
            buffers.Add(new());
            functionStarted = true;
        }
        Current?.Add(instruction);
        if(instruction.OpCode == SDSLOp.OpFunctionEnd)
        {
            functionStarted = false;
        }
    }
        
}