using SoftTouch.Spirv.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv;

public abstract class SpvSnippet
{
    public Dictionary<int, Instruction> TypeMap { get; init; } = new();
}
