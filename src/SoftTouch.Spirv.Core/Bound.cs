using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;


public struct Bound
{
    public int Start { get; }
    public int End { get; private set; }

    public Bound(int start)
    {
        Start = start;
        End = start;
    }

    public int Next()
    {
        return ++End;
    }
}
