using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;


public struct Bound
{
    public int VirtualOffset { get; set; }
    public int Offset { get; }
    public int Count { get; private set; }

    public Bound(int virtualOffset = 0)
    {
        Offset = 1;
        VirtualOffset = virtualOffset;
        Count = 1;
    }

    public int Next()
    {
        return ++Count;
    }
}
