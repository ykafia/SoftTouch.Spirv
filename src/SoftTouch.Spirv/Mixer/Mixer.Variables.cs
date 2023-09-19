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
    public enum VariableFinderMask : byte
    {
        Global,
        Function
    }
    public ref struct VariableFinder
    {
        Mixer mixer;
        bool io;

        public VariableInfo this[string name]
        {
            get
            {
                var selfFiltered = new FilteredEnumerator<WordBuffer>(mixer.buffer, SDSLOp.OpVariable);
                while (selfFiltered.MoveNext())
                {
                    var varName = FindVariableName(mixer, selfFiltered.Current.ResultId ?? -1);
                    if (varName == name && io == (selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Input || selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Output))
                        return new(varName, selfFiltered.Current.ResultId ?? -1);
                }
                var filtered = new FilteredEnumerator<WordBuffer>(mixer.buffer, SDSLOp.OpSDSLImportVariable);
                while (filtered.MoveNext())
                {
                    var variableName = filtered.Current.GetOperand<LiteralString>("variableName");
                    if (variableName.Value == name && io == (selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Input || selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Output))
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
                                variableEnumerator.Current.ResultId == nameEnumerator.Current.Operands.Span[0]
                                && nameEnumerator.Current.GetOperand<LiteralString>("name").Value == name
                                && io == (selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Input || selfFiltered.Current.Words.Span[2] == (int)Specification.StorageClass.Output)
                            )
                            {

                                var typeToFind = variableEnumerator.Current.ResultType ?? -1;
                                foreach (var i in m.Instructions)
                                {
                                    if (i.ResultId == typeToFind)
                                    {
                                        var tmp = mixer.buffer.Duplicate(i.AsRef());
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

        public static string FindVariableName(Mixer mixer, IdRef variable)
        {
            foreach(var e in mixer.buffer)
            {
                if(e.OpCode == SDSLOp.OpName && e.Words.Span[1] == variable)
                {
                    return e.AsRef().GetOperand<LiteralString>("name").Value;
                }
            }
            throw new Exception("variable has no name");
        }

        public VariableFinder(Mixer mixer, bool io = false)
        {
            this.mixer = mixer;
            this.io = io;
        }
    }
}
