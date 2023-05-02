using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Parsing;

public interface IWriteSpirv
{
    public void WriteSpirv(scoped ref Span<int> words);
}
