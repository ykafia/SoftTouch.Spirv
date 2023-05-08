using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public struct SpirvUpdate
{
    public UpdateKind Kind { get; set; }
    public int Index { get; set; }
    public int ReplaceIndex { get; set; }
}
