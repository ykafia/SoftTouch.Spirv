using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public interface ILiteralNumber
{
    public long Words { get; init; }
    public int WordCount { get; }
}
