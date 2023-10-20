using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using SoftTouch.Spirv.Core;
using SoftTouch.Spirv.Core.Buffers;
using SoftTouch.Spirv.Core.Parsing;
using static Spv.Specification;

namespace SoftTouch.Spirv;


public ref partial struct FunctionBuilder
{

    private IdRef? glslSet = null;

    public void EnsureGlslSet()
    {
        var exists = false;
        if(glslSet != null && glslSet.Value > 0)
            return;
        else if(glslSet == null)
        {
            foreach (var i in mixer.Buffer.Declarations.UnorderedInstructions)
            {
                if (i.OpCode == SDSLOp.OpExtInstImport)
                {
                    var name = i.GetOperand<LiteralString>("name") ?? "";
                    if (name.Value == "GLSL.std.450")
                        exists = true;
                }
            }
            if (!exists)
            {
                glslSet = mixer.Buffer.AddOpExtInstImport("GLSL.std.450");
            }
        }
    }

    public Instruction Round(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLRound(x, glslSet ?? -1);
    }
    public Instruction RoundEven(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLRoundEven(x, glslSet ?? -1);
    }
    public Instruction Trunc(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLTrunc(x, glslSet ?? -1);
    }
    public Instruction FAbs(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFAbs(x, glslSet ?? -1);
    }
    public Instruction SAbs(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSAbs(x, glslSet ?? -1);
    }
    public Instruction FSign(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFSign(x, glslSet ?? -1);
    }
    public Instruction SSign(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSSign(x, glslSet ?? -1);
    }
    public Instruction Floor(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFloor(x, glslSet ?? -1);
    }
    public Instruction Ceil(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLCeil(x, glslSet ?? -1);
    }
    public Instruction Fract(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFract(x, glslSet ?? -1);
    }
    public Instruction Radians(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLRadians(x, glslSet ?? -1);
    }
    public Instruction Degrees(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLDegrees(x, glslSet ?? -1);
    }
    public Instruction Sin(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSin(x, glslSet ?? -1);
    }
    public Instruction Cos(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLCos(x, glslSet ?? -1);
    }
    public Instruction Tan(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLTan(x, glslSet ?? -1);
    }
    public Instruction Asin(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAsin(x, glslSet ?? -1);
    }
    public Instruction Acos(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAcos(x, glslSet ?? -1);
    }
    public Instruction Atan(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAtan(x, glslSet ?? -1);
    }
    public Instruction Sinh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSinh(x, glslSet ?? -1);
    }
    public Instruction Cosh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLCosh(x, glslSet ?? -1);
    }
    public Instruction Tanh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLTanh(x, glslSet ?? -1);
    }
    public Instruction Asinh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAsinh(x, glslSet ?? -1);
    }
    public Instruction Acosh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAcosh(x, glslSet ?? -1);
    }
    public Instruction Atanh(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAtanh(x, glslSet ?? -1);
    }
    public Instruction Atan2(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLAtan2(x, y, glslSet ?? -1);
    }
    public Instruction Pow(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPow(x, y, glslSet ?? -1);
    }
    public Instruction Exp(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLExp(x, glslSet ?? -1);
    }
    public Instruction Log(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLLog(x, glslSet ?? -1);
    }
    public Instruction Exp2(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLExp2(x, glslSet ?? -1);
    }
    public Instruction Log2(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLLog2(x, glslSet ?? -1);
    }
    public Instruction Sqrt(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSqrt(x, glslSet ?? -1);
    }
    public Instruction InverseSqrt(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLInverseSqrt(x, glslSet ?? -1);
    }
    public Instruction Determinant(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLDeterminant(x, glslSet ?? -1);
    }
    public Instruction MatrixInverse(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLMatrixInverse(x, glslSet ?? -1);
    }
    public Instruction Modf(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLModf(x, y, glslSet ?? -1);
    }
    public Instruction ModfStruct(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLModfStruct(x, glslSet ?? -1);
    }
    public Instruction FMin(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFMin(x, y, glslSet ?? -1);
    }
    public Instruction UMin(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUMin(x, y, glslSet ?? -1);
    }
    public Instruction SMin(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSMin(x, y, glslSet ?? -1);
    }
    public Instruction FMax(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFMax(x, y, glslSet ?? -1);
    }
    public Instruction UMax(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUMax(x, y, glslSet ?? -1);
    }
    public Instruction SMax(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSMax(x, y, glslSet ?? -1);
    }
    public Instruction FClamp(IdRef x, IdRef minVal, IdRef maxVal)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFClamp(x, minVal, maxVal, glslSet ?? -1);
    }
    public Instruction UClamp(IdRef x, IdRef minVal, IdRef maxVal)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUClamp(x, minVal, maxVal, glslSet ?? -1);
    }
    public Instruction SClamp(IdRef x, IdRef minVal, IdRef maxVal)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSClamp(x, minVal, maxVal, glslSet ?? -1);
    }
    public Instruction FMix(IdRef x, IdRef y, IdRef a)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFMix(x, y, a, glslSet ?? -1);
    }
    public Instruction IMix(IdRef x, IdRef y, IdRef a)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLIMix(x, y, a, glslSet ?? -1);
    }
    public Instruction Step(IdRef edge, IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLStep(edge, x, glslSet ?? -1);
    }
    public Instruction SmoothStep(IdRef edge0, IdRef edge1, IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLSmoothStep(edge0, edge1, x, glslSet ?? -1);
    }
    public Instruction Fma(IdRef a, IdRef b, IdRef c)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFma(a, b, c, glslSet ?? -1);
    }
    public Instruction Frexp(IdRef x, IdRef exp)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFrexp(x, exp, glslSet ?? -1);
    }
    public Instruction FrexpStruct(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFrexpStruct(x, glslSet ?? -1);
    }
    public Instruction Ldexp(IdRef x, IdRef exp)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLLdexp(x, exp, glslSet ?? -1);
    }
    public Instruction PackSnorm4x8(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackSnorm4x8(x, glslSet ?? -1);
    }
    public Instruction PackUnorm4x8(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackUnorm4x8(x, glslSet ?? -1);
    }
    public Instruction PackSnorm2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackSnorm2x16(x, glslSet ?? -1);
    }
    public Instruction PackUnorm2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackUnorm2x16(x, glslSet ?? -1);
    }
    public Instruction PackHalf2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackHalf2x16(x, glslSet ?? -1);
    }
    public Instruction PackDouble2x32(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLPackDouble2x32(x, glslSet ?? -1);
    }
    public Instruction UnpackSnorm2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackSnorm2x16(x, glslSet ?? -1);
    }
    public Instruction UnpackUnorm2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackUnorm2x16(x, glslSet ?? -1);
    }
    public Instruction UnpackHalf2x16(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackHalf2x16(x, glslSet ?? -1);
    }
    public Instruction UnpackSnorm4x8(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackSnorm4x8(x, glslSet ?? -1);
    }
    public Instruction UnpackUnorm4x8(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackUnorm4x8(x, glslSet ?? -1);
    }
    public Instruction UnpackDouble2x32(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLUnpackDouble2x32(x, glslSet ?? -1);
    }
    public Instruction Length(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLLength(x, glslSet ?? -1);
    }
    public Instruction Distance(IdRef p0, IdRef p1)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLDistance(p0, p1, glslSet ?? -1);
    }
    public Instruction Cross(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLCross(x, y, glslSet ?? -1);
    }
    public Instruction Normalize(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLNormalize(x, glslSet ?? -1);
    }
    public Instruction FaceForward(IdRef n, IdRef i, IdRef nref)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFaceForward(n, i, nref, glslSet ?? -1);
    }
    public Instruction Reflect(IdRef i, IdRef n)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLReflect(i, n, glslSet ?? -1);
    }
    public Instruction Refract(IdRef i, IdRef n, IdRef eta)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLRefract(i, n, eta, glslSet ?? -1);
    }
    public Instruction FindILsb(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFindILsb(x, glslSet ?? -1);
    }
    public Instruction FindSMsb(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFindSMsb(x, glslSet ?? -1);
    }
    public Instruction FindUMsb(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLFindUMsb(x, glslSet ?? -1);
    }
    public Instruction InterpolateAtCentroid(IdRef x)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLInterpolateAtCentroid(x, glslSet ?? -1);
    }
    public Instruction InterpolateAtSample(IdRef interpolant, IdRef sample)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLInterpolateAtSample(interpolant, sample, glslSet ?? -1);
    }
    public Instruction InterpolateAtOffset(IdRef interpolant, IdRef offset)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLInterpolateAtOffset(interpolant, offset, glslSet ?? -1);
    }
    public Instruction NMin(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLNMin(x, y, glslSet ?? -1);
    }
    public Instruction NMax(IdRef x, IdRef y)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLNMax(x, y, glslSet ?? -1);
    }
    public Instruction NClamp(IdRef x, IdRef minVal, IdRef maxVal)
    {
        EnsureGlslSet();
        return mixer.Buffer.AddGLSLNClamp(x, minVal, maxVal, glslSet ?? -1);
    }
}