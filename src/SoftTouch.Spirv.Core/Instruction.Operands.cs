using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SoftTouch.Spirv.Core;

public partial struct Instruction
{

    public bool Get<T>(string propertyName, out T result)
        where T : struct, IFromSpirv<T>
    {
        // First get mandatory word count
        int mandatory = 0;
        var operandInfos = InstructionInfo.GetInfo(OpCode);

        var missing = 0;
        var countOptional = 0;
        foreach (var e in operandInfos)
            if (e.Quantifier != OperandQuantifier.One)
                countOptional += 1;

        

        if (countOptional > 0)
        {
            foreach (var o in operandInfos)
            {
                if (o.GetWordSize() != -1 && o.Quantifier == OperandQuantifier.One)
                    mandatory += o.GetWordSize();
                else if (o.Kind == OperandKind.LiteralString && o.Quantifier == OperandQuantifier.One)
                {
                    var rest = Operands[(mandatory + 1)..];
                    for (int i = 0; i < rest.Length; i++)
                    {
                        var chars = MemoryMarshal.Cast<int, char>(rest[i..i]);
                        if (chars.Contains('\0'))
                        {
                            mandatory += i;
                            break;
                        }
                    }
                }
            }
            missing = CountOfWords - mandatory;
        }



        var spanCount = 0;
        Span<(int start, int length)> spans = stackalloc (int start, int length)[operandInfos.Count];
        var index = 0;

        var wid = 0;

        var lowerProperty = propertyName.ToLowerInvariant();
        var info = InstructionInfo.GetInfo(OpCode);
        foreach(var o in  operandInfos)
        {
            if (o.Kind != OperandKind.IdResult && o.Kind != OperandKind.IdResultType)
            {
                if (o.Kind == OperandKind.LiteralString && o.Quantifier == OperandQuantifier.One)
                {
                    var operands = Operands;
                    var restOperands = Operands[wid..];
                    var bytes = MemoryMarshal.Cast<int, byte>(Operands[wid..]);

                    var tmp = Encoding.UTF8.GetString(bytes);

                    var id = tmp.Length;
                    spans[index] = (wid, id);

                    if (o.Name == lowerProperty)
                    {
                        result = T.From(tmp);
                        return true;
                    }
                    index += 1;
                    wid += id + 1;
                }
                else if (o.GetWordSize() == 1 && o.Quantifier == OperandQuantifier.One)
                {
                    spans[index] = (wid, 1);
                    if (o.Name == lowerProperty)
                    {
                        result = T.From(Operands.Slice(wid, 1));
                        return true;
                    }
                    index += 1;
                    wid += 1;
                }
                else if (o.GetWordSize() == 2 && o.Quantifier == OperandQuantifier.One)
                {
                    spans[index] = (wid, 2);
                    if (o.Name == lowerProperty)
                    {
                        result = T.From(Operands.Slice(wid, 2));
                        return true;
                    }
                    index += 1;
                    wid += 1;
                }
                else if (o.GetWordSize() == 1 && o.Quantifier == OperandQuantifier.ZeroOrOne && missing == 0)
                {
                    spans[index] = (wid, 1);
                    if (o.Name == lowerProperty)
                    {
                        result = T.From(Operands.Slice(wid, 1));
                        return true;
                    }
                    index += 1;
                    wid += 1;
                }
                else if (o.GetWordSize() == 2 && o.Quantifier == OperandQuantifier.ZeroOrOne && missing == 0)
                {
                    spans[index] = (wid, 2);
                    if (o.Name == lowerProperty)
                    {
                        result = T.From(Operands.Slice(wid, 2));
                        return true;
                    }
                    index += 1;
                    wid += 1;
                }
            }
        }
        result = default;
        return false;
    }
}