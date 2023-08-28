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


public record struct VariableInfo(string Name, IdRef Id);


public partial class Mixer
{
    public ref struct Variables
    {
        Mixer mixer;

        public VariableInfo this[string name]
        {
            get
            {
                var filtered = new FilteredEnumerator<WordBuffer>(mixer.buffer, SDSLOp.OpSDSLImportVariable);
                while (filtered.MoveNext())
                {
                    var variableName = filtered.Current.GetOperand<LiteralString>("variableName");
                    if (variableName.Value == name)
                        return new(variableName.Value, filtered.Current.ResultId ?? -1);
                }
                foreach (var m in mixer.mixins)
                {
                    var variableEnumerator = new FilteredEnumerator<SortedWordBuffer>(m.Buffer, SDSLOp.OpVariable);
                    while (variableEnumerator.MoveNext())
                    {
                        var nameEnumerator = new FilteredEnumerator<SortedWordBuffer>(m.Buffer, SDSLOp.OpName);
                        while (nameEnumerator.MoveNext())
                        {
                            if (
                                variableEnumerator.Current.ResultId == nameEnumerator.Current.Operands[0]
                                && nameEnumerator.Current.GetOperand<LiteralString>("name").Value == name
                            )
                            {

                                var typeToFind = variableEnumerator.Current.ResultType ?? -1;
                                foreach (var i in m.Instructions)
                                {
                                    if (i.ResultId == typeToFind)
                                    {
                                        var tmp = mixer.buffer.Duplicate(i);
                                        var result = mixer.buffer.AddOpSDSLImportVariable(
                                            name,
                                            m.Name,
                                            variableEnumerator.Current.ResultId
                                        );
                                        return new(
                                            name,
                                            result.ResultId ?? -1
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

        public Variables(Mixer mixer)
        {
            this.mixer = mixer;
        }
    }
}
