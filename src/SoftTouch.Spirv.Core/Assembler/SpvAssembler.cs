using SoftTouch.Spirv.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core.Assembler;

public class SpvAssembler : WordBuffer
{
    WordBuffer updateBuffer;

    List<SpirvUpdate> updateList;

    public SpvAssembler()
    {
        updateBuffer = new();
        updateList = new();
    }

}
