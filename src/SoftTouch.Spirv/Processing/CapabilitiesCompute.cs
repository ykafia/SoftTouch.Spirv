﻿using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv.Processing;

public struct CapabilitiesCompute : INanoPass
{
    public void Apply(MultiBuffer buffer)
    {
        throw new NotImplementedException("Needs to finish checking the spec");
    }

    public static void AddCapabilities(Instruction instruction)
    {
        if(instruction.OpCode == SDSLOp.OpEntryPoint)
        {
            if(instruction.GetOperand<LiteralInteger>("executionModel").Words == (int)ExecutionModel.Geometry)
            {
                //Add capability geometry
            }
            else if (instruction.GetOperand<LiteralInteger>("executionModel").Words == (int)ExecutionModel.TessellationControl)
            {
                //Add capability tess

            }
            else if (instruction.GetOperand<LiteralInteger>("executionModel").Words == (int)ExecutionModel.TessellationEvaluation)
            {
                //Add capability tess
            }
        }
        else if(instruction.OpCode == SDSLOp.OpTypeFloat && instruction.Words.Span[2] == 16)
        {
            // Add capability Float16
        }
        else if (instruction.OpCode == SDSLOp.OpTypeFloat && instruction.Words.Span[2] == 64)
        {
            // Add capability Float64
        }
        else if (instruction.OpCode == SDSLOp.OpTypeInt && instruction.Words.Span[2] == 64)
        {
            // Add capability Float64
        }
        else if (instruction.OpCode == SDSLOp.OpTypeInt && instruction.Words.Span[2] == 16)
        {
            // Add capability Float64
        }
        else if (instruction.OpCode == SDSLOp.OpTypeInt && instruction.Words.Span[2] == 8)
        {
            // Add capability Float64
        }
        
        // TODO : Check if any atomic instructions operates on integers
        // else if (instruction.OpCode == SDSLOp.OpAtomic && instruction.Words.Span[2] == 64)
        // {
        //     // Add capability Float64
        // }


    }
}
