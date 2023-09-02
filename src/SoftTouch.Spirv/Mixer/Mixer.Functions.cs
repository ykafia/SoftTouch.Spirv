using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using Spv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;


public record struct FunctionInfo(string Name, IdRef Id, IdResultType Type);


public partial class Mixer
{
    public ref struct FunctionFinder
    {
        Mixer mixer;

        public FunctionInfo this[string name]
        {
            get
            {
                var filtered = new FilteredEnumerator<WordBuffer>(mixer.buffer, SDSLOp.OpSDSLImportFunction);
                while (filtered.MoveNext())
                {
                    var functionName = filtered.Current.GetOperand<LiteralString>("functionName");
                    if (functionName.Value == name)
                        return new(functionName.Value, filtered.Current.ResultId ?? -1, filtered.Current.ResultType ?? -1);
                }
                foreach (var m in mixer.mixins)
                {
                    var functionEnumerator = new FilteredEnumerator<SortedWordBuffer>(m.Buffer, SDSLOp.OpFunction);
                    while (functionEnumerator.MoveNext())
                    {
                        var nameEnumerator = new FilteredEnumerator<SortedWordBuffer>(m.Buffer, SDSLOp.OpName);
                        while (nameEnumerator.MoveNext())
                        {
                            if (
                                functionEnumerator.Current.ResultId == nameEnumerator.Current.Operands[0]
                                && nameEnumerator.Current.GetOperand<LiteralString>("name").Value == name
                            )
                            {
                                var typeToFind = functionEnumerator.Current.ResultType ?? -1;
                                foreach (var i in m.Instructions)
                                {
                                    if (i.ResultId == typeToFind)
                                    {
                                        var tmp = mixer.buffer.Duplicate(i);
                                        var result = mixer.buffer.AddOpSDSLImportFunction(
                                            name,
                                            m.Name,
                                            functionEnumerator.Current.ResultId,
                                            tmp.ResultType
                                        );
                                        return new(name,
                                            result.ResultId ?? -1,
                                            functionEnumerator.Current.ResultType ?? -1
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
                return new();
            }
        }

        public FunctionFinder(Mixer mixer)
        {
            this.mixer = mixer;
        }
    }
}
