﻿using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;

public partial class WordBuffer
{
    public static WordBuffer Parse(byte[] bytes)
    {
        WordBuffer buffer = new WordBuffer();
        buffer.buffer = MemoryOwner<int>.Allocate(bytes.Length / 4, AllocationMode.Clear);
        var ints = MemoryMarshal.Cast<byte, int>(bytes)[5..];
        ints.CopyTo(buffer.buffer.Span);
        return buffer;
    }
}
