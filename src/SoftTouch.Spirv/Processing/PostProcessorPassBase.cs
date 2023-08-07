using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;


public interface IPostProcessorPassCreator
{
    public static abstract PostProcessorPassBase Create(SpirvBuffer buffer);
}


public abstract class PostProcessorPassBase
{
    public SpirvBuffer Buffer { get; init; }

    public PostProcessorPassBase(SpirvBuffer buffer)
    {
        Buffer = buffer;
    }

    public abstract void Apply();
}
