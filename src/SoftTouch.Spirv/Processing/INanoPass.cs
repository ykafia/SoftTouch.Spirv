using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Processing;

/// <summary>
/// Nano pass for the mixin compiler
/// </summary>
public interface INanoPass
{
    void Apply(SpirvBuffer buffer);
}
