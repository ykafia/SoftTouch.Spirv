using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;

public partial class WordBuffer
{
    public void Add(SourceLanguage? value)
    {
        if (value is not null)
        {
            Expand(1);
            Add(Convert.ToInt32(value));
        }
    }

}
