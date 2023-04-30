using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public ref struct SpirvReader
{
    public static List<Instruction> ParseToList(byte[] byteCode)
    {
        var reader = new SpirvReader(byteCode);
        var list = new List<Instruction>(reader.GetInstructionCount());
        foreach(var instruction in reader)
        {
            list.Add(instruction.Allocate());
        }
        return list;
    }



    Span<int> words;
    public int Count => GetInstructionCount();
    public int WordCount => words.Length;

    public SpirvReader(byte[] byteCode)
    {
        var wordLength = byteCode.Length / 4;
        words = MemoryMarshal.Cast<byte, int>(byteCode.AsSpan());
        var header = SpirvHeader.Read(words[0..5]);
    }


    public InstructionEnumerator GetEnumerator() => new(words[5..]);

    public int GetInstructionCount()
    {
        var count = 0;
        var index = 5;
        while(index < words.Length) 
        {
            count += 1;
            index += words[index] >> 16;
        }
        return count;
    }

    public ref struct InstructionEnumerator
    {
        int index;
        int wordIndex;
        bool started;
        Span<int> instructionWords;

        public InstructionEnumerator(Span<int> words)
        {
            started = false;
            index = 0;
            wordIndex = 0;
            instructionWords = words;
        }

        public RefInstruction Current => ParseCurrentInstruction();

        public bool MoveNext()
        {
            if (!started)
            {
                started = true;
                return true;
            }
            else
            {
                index += 1;
                var sizeToStep = instructionWords[wordIndex] >> 16;
                wordIndex += sizeToStep;
                if (wordIndex >= instructionWords.Length)
                    return false;
                return true;
            }
            
        }


        public RefInstruction ParseCurrentInstruction()
        {
            var wordNumber = instructionWords[wordIndex] >> 16;
            return RefInstruction.Parse(instructionWords.Slice(wordIndex, wordNumber));
        }
    }
}
