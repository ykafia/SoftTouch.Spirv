using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public interface IFromSpirv<T>
{
    static abstract T From(Span<int> words);
}
