using static Spv.Specification;
namespace SoftTouch.Spirv.Core.Buffers;

public static class WordBufferExtensions
{
    public static Instruction AddOpNop<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpNop;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUndef<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUndef;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSourceContinued<TBuffer>(this TBuffer buffer, LiteralString continuedSource) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(continuedSource);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSourceContinued;
        mutInstruction.Add(continuedSource);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSource<TBuffer>(this TBuffer buffer, SourceLanguage sourcelanguage, LiteralInteger version, IdRef? file, LiteralString? source) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(sourcelanguage) + buffer.GetWordLength(version) + buffer.GetWordLength(file) + buffer.GetWordLength(source);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSource;
        mutInstruction.Add(sourcelanguage);
        mutInstruction.Add(version);
        mutInstruction.Add(file);
        mutInstruction.Add(source);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSourceExtension<TBuffer>(this TBuffer buffer, LiteralString extension) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(extension);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSourceExtension;
        mutInstruction.Add(extension);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpName<TBuffer>(this TBuffer buffer, IdRef target, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpName;
        mutInstruction.Add(target);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMemberName<TBuffer>(this TBuffer buffer, IdRef type, LiteralInteger member, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(type) + buffer.GetWordLength(member) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemberName;
        mutInstruction.Add(type);
        mutInstruction.Add(member);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpString<TBuffer>(this TBuffer buffer, LiteralString value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpString;
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLine<TBuffer>(this TBuffer buffer, IdRef file, LiteralInteger line, LiteralInteger column) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(file) + buffer.GetWordLength(line) + buffer.GetWordLength(column);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLine;
        mutInstruction.Add(file);
        mutInstruction.Add(line);
        mutInstruction.Add(column);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExtension<TBuffer>(this TBuffer buffer, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExtension;
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExtInstImport<TBuffer>(this TBuffer buffer, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExtInstImport;
        mutInstruction.Add(resultId);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExtInst<TBuffer>(this TBuffer buffer, IdRef set, LiteralInteger instruction, IdResult? resultId, IdResultType? resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(set) + buffer.GetWordLength(instruction) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExtInst;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(set);
        mutInstruction.Add(instruction);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMemoryModel<TBuffer>(this TBuffer buffer, AddressingModel addressingmodel, MemoryModel memorymodel) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(addressingmodel) + buffer.GetWordLength(memorymodel);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemoryModel;
        mutInstruction.Add(addressingmodel);
        mutInstruction.Add(memorymodel);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEntryPoint<TBuffer>(this TBuffer buffer, ExecutionModel executionmodel, IdRef entryPoint, LiteralString name, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(executionmodel) + buffer.GetWordLength(entryPoint) + buffer.GetWordLength(name) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEntryPoint;
        mutInstruction.Add(executionmodel);
        mutInstruction.Add(entryPoint);
        mutInstruction.Add(name);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExecutionMode<TBuffer>(this TBuffer buffer, IdRef entryPoint, ExecutionMode mode) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(entryPoint) + buffer.GetWordLength(mode);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExecutionMode;
        mutInstruction.Add(entryPoint);
        mutInstruction.Add(mode);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCapability<TBuffer>(this TBuffer buffer, Capability capability) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(capability);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCapability;
        mutInstruction.Add(capability);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeVoid<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeVoid;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeBool<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeBool;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeInt<TBuffer>(this TBuffer buffer, LiteralInteger width, LiteralInteger signedness) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(width) + buffer.GetWordLength(signedness);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeInt;
        mutInstruction.Add(resultId);
        mutInstruction.Add(width);
        mutInstruction.Add(signedness);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeFloat<TBuffer>(this TBuffer buffer, LiteralInteger width, FPEncoding? floatingPointEncoding) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(width) + buffer.GetWordLength(floatingPointEncoding);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeFloat;
        mutInstruction.Add(resultId);
        mutInstruction.Add(width);
        mutInstruction.Add(floatingPointEncoding);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeVector<TBuffer>(this TBuffer buffer, IdRef componentType, LiteralInteger componentCount) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(componentType) + buffer.GetWordLength(componentCount);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeVector;
        mutInstruction.Add(resultId);
        mutInstruction.Add(componentType);
        mutInstruction.Add(componentCount);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeMatrix<TBuffer>(this TBuffer buffer, IdRef columnType, LiteralInteger columnCount) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(columnType) + buffer.GetWordLength(columnCount);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeMatrix;
        mutInstruction.Add(resultId);
        mutInstruction.Add(columnType);
        mutInstruction.Add(columnCount);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeImage<TBuffer>(this TBuffer buffer, IdRef sampledType, Dim dim, LiteralInteger depth, LiteralInteger arrayed, LiteralInteger mS, LiteralInteger sampled, ImageFormat imageformat, AccessQualifier? accessqualifier) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledType) + buffer.GetWordLength(dim) + buffer.GetWordLength(depth) + buffer.GetWordLength(arrayed) + buffer.GetWordLength(mS) + buffer.GetWordLength(sampled) + buffer.GetWordLength(imageformat) + buffer.GetWordLength(accessqualifier);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeImage;
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledType);
        mutInstruction.Add(dim);
        mutInstruction.Add(depth);
        mutInstruction.Add(arrayed);
        mutInstruction.Add(mS);
        mutInstruction.Add(sampled);
        mutInstruction.Add(imageformat);
        mutInstruction.Add(accessqualifier);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeSampler<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeSampler;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeSampledImage<TBuffer>(this TBuffer buffer, IdRef imageType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(imageType);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeSampledImage;
        mutInstruction.Add(resultId);
        mutInstruction.Add(imageType);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeArray<TBuffer>(this TBuffer buffer, IdRef elementType, IdRef length) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(elementType) + buffer.GetWordLength(length);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeArray;
        mutInstruction.Add(resultId);
        mutInstruction.Add(elementType);
        mutInstruction.Add(length);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeRuntimeArray<TBuffer>(this TBuffer buffer, IdRef elementType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(elementType);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeRuntimeArray;
        mutInstruction.Add(resultId);
        mutInstruction.Add(elementType);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeStruct<TBuffer>(this TBuffer buffer, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeStruct;
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeOpaque<TBuffer>(this TBuffer buffer, LiteralString thenameoftheopaquetype) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(thenameoftheopaquetype);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeOpaque;
        mutInstruction.Add(resultId);
        mutInstruction.Add(thenameoftheopaquetype);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypePointer<TBuffer>(this TBuffer buffer, StorageClass storageclass, IdRef type) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(storageclass) + buffer.GetWordLength(type);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypePointer;
        mutInstruction.Add(resultId);
        mutInstruction.Add(storageclass);
        mutInstruction.Add(type);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeFunction<TBuffer>(this TBuffer buffer, IdRef returnType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(returnType) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeFunction;
        mutInstruction.Add(resultId);
        mutInstruction.Add(returnType);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeEvent<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeEvent;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeDeviceEvent<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeDeviceEvent;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeReserveId<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeReserveId;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeQueue<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeQueue;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypePipe<TBuffer>(this TBuffer buffer, AccessQualifier qualifier) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(qualifier);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypePipe;
        mutInstruction.Add(resultId);
        mutInstruction.Add(qualifier);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeForwardPointer<TBuffer>(this TBuffer buffer, IdRef pointerType, StorageClass storageclass) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointerType) + buffer.GetWordLength(storageclass);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeForwardPointer;
        mutInstruction.Add(pointerType);
        mutInstruction.Add(storageclass);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantTrue<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantTrue;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantFalse<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantFalse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstant<TBuffer, TValue>(this TBuffer buffer, IdResultType? resultType, TValue value) where TBuffer : IMutSpirvBuffer where TValue : ILiteralNumber
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + value.WordCount;
        return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpConstant, ..resultType.AsSpanOwner().Span, resultId, ..value.AsSpanOwner().Span]));
    }
    public static Instruction AddOpConstantComposite<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantComposite;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantSampler<TBuffer>(this TBuffer buffer, IdResultType resultType, SamplerAddressingMode sampleraddressingmode, LiteralInteger param, SamplerFilterMode samplerfiltermode) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampleraddressingmode) + buffer.GetWordLength(param) + buffer.GetWordLength(samplerfiltermode);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantSampler;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampleraddressingmode);
        mutInstruction.Add(param);
        mutInstruction.Add(samplerfiltermode);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantNull<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantNull;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantTrue<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantTrue;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantFalse<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantFalse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstant<TBuffer, TValue>(this TBuffer buffer, IdResultType? resultType, TValue value) where TBuffer : IMutSpirvBuffer where TValue : ILiteralNumber
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + value.WordCount;
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstant;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantComposite<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantComposite;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantOp<TBuffer>(this TBuffer buffer, IdResultType resultType, Op opcode) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(opcode);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantOp;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(opcode);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFunction<TBuffer>(this TBuffer buffer, IdResultType resultType, FunctionControlMask functioncontrol, IdRef functionType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(functioncontrol) + buffer.GetWordLength(functionType);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFunction;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(functioncontrol);
        mutInstruction.Add(functionType);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFunctionParameter<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFunctionParameter;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFunctionEnd<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpFunctionEnd;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFunctionCall<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef function, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(function) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFunctionCall;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(function);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVariable<TBuffer>(this TBuffer buffer, IdResultType resultType, StorageClass storageclass, IdRef? initializer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(storageclass) + buffer.GetWordLength(initializer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVariable;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(storageclass);
        mutInstruction.Add(initializer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageTexelPointer<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, IdRef sample) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(sample);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageTexelPointer;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(sample);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLoad<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, MemoryAccessMask? memoryaccess) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memoryaccess);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLoad;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memoryaccess);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpStore<TBuffer>(this TBuffer buffer, IdRef pointer, IdRef objectId, MemoryAccessMask? memoryaccess) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(objectId) + buffer.GetWordLength(memoryaccess);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpStore;
        mutInstruction.Add(pointer);
        mutInstruction.Add(objectId);
        mutInstruction.Add(memoryaccess);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCopyMemory<TBuffer>(this TBuffer buffer, IdRef target, IdRef source, MemoryAccessMask? memoryaccess, MemoryAccessMask? memoryaccess1) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(source) + buffer.GetWordLength(memoryaccess) + buffer.GetWordLength(memoryaccess1);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCopyMemory;
        mutInstruction.Add(target);
        mutInstruction.Add(source);
        mutInstruction.Add(memoryaccess);
        mutInstruction.Add(memoryaccess1);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCopyMemorySized<TBuffer>(this TBuffer buffer, IdRef target, IdRef source, IdRef size, MemoryAccessMask? memoryaccess, MemoryAccessMask? memoryaccess1) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(source) + buffer.GetWordLength(size) + buffer.GetWordLength(memoryaccess) + buffer.GetWordLength(memoryaccess1);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCopyMemorySized;
        mutInstruction.Add(target);
        mutInstruction.Add(source);
        mutInstruction.Add(size);
        mutInstruction.Add(memoryaccess);
        mutInstruction.Add(memoryaccess1);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAccessChain<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAccessChain;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpInBoundsAccessChain<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpInBoundsAccessChain;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrAccessChain<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef element, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(element) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrAccessChain;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(element);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArrayLength<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef structure, LiteralInteger arraymember) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(structure) + buffer.GetWordLength(arraymember);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArrayLength;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(structure);
        mutInstruction.Add(arraymember);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGenericPtrMemSemantics<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGenericPtrMemSemantics;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpInBoundsPtrAccessChain<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef element, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(element) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpInBoundsPtrAccessChain;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(element);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDecorate<TBuffer>(this TBuffer buffer, IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpDecorate, target, (int)decoration, ..additional1.AsSpanOwner().Span, ..additional2.AsSpanOwner().Span, ..additionalString.AsSpanOwner().Span]));
    }
    public static Instruction AddOpMemberDecorate<TBuffer>(this TBuffer buffer, IdRef structureType, LiteralInteger member, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(structureType) + buffer.GetWordLength(member) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemberDecorate;
        mutInstruction.Add(structureType);
        mutInstruction.Add(member);
        mutInstruction.Add(decoration);
        mutInstruction.Add(additional1);
        mutInstruction.Add(additional2);
        mutInstruction.Add(additionalString);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDecorationGroup<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDecorationGroup;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupDecorate<TBuffer>(this TBuffer buffer, IdRef decorationGroup, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(decorationGroup) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupDecorate;
        mutInstruction.Add(decorationGroup);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupMemberDecorate<TBuffer>(this TBuffer buffer, IdRef decorationGroup, Span<PairIdRefLiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(decorationGroup) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupMemberDecorate;
        mutInstruction.Add(decorationGroup);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVectorExtractDynamic<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector, IdRef index) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector) + buffer.GetWordLength(index);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVectorExtractDynamic;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        mutInstruction.Add(index);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVectorInsertDynamic<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector, IdRef component, IdRef index) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector) + buffer.GetWordLength(component) + buffer.GetWordLength(index);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVectorInsertDynamic;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        mutInstruction.Add(component);
        mutInstruction.Add(index);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVectorShuffle<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, Span<LiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVectorShuffle;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCompositeConstruct<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCompositeConstruct;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCompositeExtract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef composite, Span<LiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(composite) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCompositeExtract;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(composite);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCompositeInsert<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef objectId, IdRef composite, Span<LiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(objectId) + buffer.GetWordLength(composite) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCompositeInsert;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(objectId);
        mutInstruction.Add(composite);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCopyObject<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCopyObject;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTranspose<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef matrix) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(matrix);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTranspose;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(matrix);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSampledImage<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef sampler) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(sampler);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSampledImage;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(sampler);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleDrefImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleDrefImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleDrefExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleDrefExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleProjImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleProjImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleProjExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleProjExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleProjDrefImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleProjDrefImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleProjDrefExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleProjDrefExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageFetch<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageFetch;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageGather<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef component, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(component) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageGather;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(component);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageDrefGather<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageDrefGather;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageRead<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageRead;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageWrite<TBuffer>(this TBuffer buffer, IdRef image, IdRef coordinate, IdRef texel, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(texel) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageWrite;
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(texel);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImage<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImage;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQueryFormat<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQueryFormat;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQueryOrder<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQueryOrder;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQuerySizeLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef levelofDetail) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(levelofDetail);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQuerySizeLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(levelofDetail);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQuerySize<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQuerySize;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQueryLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQueryLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQueryLevels<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQueryLevels;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageQuerySamples<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageQuerySamples;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertFToU<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef floatValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(floatValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertFToU;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(floatValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertFToS<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef floatValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(floatValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertFToS;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(floatValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertSToF<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef signedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(signedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertSToF;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(signedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToF<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef unsignedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(unsignedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToF;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(unsignedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUConvert<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef unsignedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(unsignedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUConvert;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(unsignedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSConvert<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef signedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(signedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSConvert;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(signedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFConvert<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef floatValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(floatValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFConvert;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(floatValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpQuantizeToF16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpQuantizeToF16;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertPtrToU<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertPtrToU;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSatConvertSToU<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef signedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(signedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSatConvertSToU;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(signedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSatConvertUToS<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef unsignedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(unsignedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSatConvertUToS;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(unsignedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToPtr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef integerValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(integerValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToPtr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(integerValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrCastToGeneric<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrCastToGeneric;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGenericCastToPtr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGenericCastToPtr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGenericCastToPtrExplicit<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, StorageClass storage) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(storage);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGenericCastToPtrExplicit;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(storage);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitcast<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitcast;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSNegate<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSNegate;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFNegate<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFNegate;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpISub<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpISub;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFSub<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFSub;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIMul<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIMul;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFMul<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFMul;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUDiv<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUDiv;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDiv<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDiv;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFDiv<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFDiv;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUMod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUMod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSRem<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSRem;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSMod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSMod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFRem<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFRem;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFMod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFMod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVectorTimesScalar<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector, IdRef scalar) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector) + buffer.GetWordLength(scalar);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVectorTimesScalar;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        mutInstruction.Add(scalar);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMatrixTimesScalar<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef matrix, IdRef scalar) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(matrix) + buffer.GetWordLength(scalar);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMatrixTimesScalar;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(matrix);
        mutInstruction.Add(scalar);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVectorTimesMatrix<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector, IdRef matrix) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector) + buffer.GetWordLength(matrix);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVectorTimesMatrix;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        mutInstruction.Add(matrix);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMatrixTimesVector<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef matrix, IdRef vector) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(matrix) + buffer.GetWordLength(vector);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMatrixTimesVector;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(matrix);
        mutInstruction.Add(vector);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMatrixTimesMatrix<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef leftMatrix, IdRef rightMatrix) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(leftMatrix) + buffer.GetWordLength(rightMatrix);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMatrixTimesMatrix;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(leftMatrix);
        mutInstruction.Add(rightMatrix);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpOuterProduct<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpOuterProduct;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIAddCarry<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIAddCarry;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpISubBorrow<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpISubBorrow;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUMulExtended<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUMulExtended;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSMulExtended<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSMulExtended;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAny<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAny;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAll<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAll;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsNan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsNan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsInf<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsInf;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsFinite<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsFinite;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsNormal<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsNormal;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSignBitSet<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSignBitSet;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLessOrGreater<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x) + buffer.GetWordLength(y);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLessOrGreater;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        mutInstruction.Add(y);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpOrdered<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x) + buffer.GetWordLength(y);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpOrdered;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        mutInstruction.Add(y);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUnordered<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(x) + buffer.GetWordLength(y);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUnordered;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(x);
        mutInstruction.Add(y);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLogicalEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLogicalEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLogicalNotEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLogicalNotEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLogicalOr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLogicalOr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLogicalAnd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLogicalAnd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLogicalNot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLogicalNot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSelect<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef condition, IdRef object1, IdRef object2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(condition) + buffer.GetWordLength(object1) + buffer.GetWordLength(object2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSelect;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(condition);
        mutInstruction.Add(object1);
        mutInstruction.Add(object2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpINotEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpINotEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUGreaterThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUGreaterThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSGreaterThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSGreaterThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUGreaterThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUGreaterThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSGreaterThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSGreaterThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpULessThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpULessThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSLessThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSLessThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpULessThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpULessThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSLessThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSLessThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdNotEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdNotEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordNotEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordNotEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdLessThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdLessThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordLessThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordLessThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdGreaterThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdGreaterThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordGreaterThan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordGreaterThan;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdLessThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdLessThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordLessThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordLessThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFOrdGreaterThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFOrdGreaterThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFUnordGreaterThanEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFUnordGreaterThanEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpShiftRightLogical<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef shift) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(shift);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpShiftRightLogical;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(shift);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpShiftRightArithmetic<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef shift) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(shift);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpShiftRightArithmetic;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(shift);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpShiftLeftLogical<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef shift) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(shift);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpShiftLeftLogical;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(shift);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitwiseOr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitwiseOr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitwiseXor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitwiseXor;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitwiseAnd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitwiseAnd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpNot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpNot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitFieldInsert<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef insert, IdRef offset, IdRef count) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(insert) + buffer.GetWordLength(offset) + buffer.GetWordLength(count);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitFieldInsert;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(insert);
        mutInstruction.Add(offset);
        mutInstruction.Add(count);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitFieldSExtract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef offset, IdRef count) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(offset) + buffer.GetWordLength(count);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitFieldSExtract;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(offset);
        mutInstruction.Add(count);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitFieldUExtract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef offset, IdRef count) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(offset) + buffer.GetWordLength(count);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitFieldUExtract;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(offset);
        mutInstruction.Add(count);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitReverse<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitReverse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBitCount<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBitCount;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdx<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdx;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdy<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdy;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFwidth<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFwidth;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdxFine<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdxFine;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdyFine<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdyFine;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFwidthFine<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFwidthFine;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdxCoarse<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdxCoarse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDPdyCoarse<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDPdyCoarse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFwidthCoarse<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(p);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFwidthCoarse;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(p);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEmitVertex<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpEmitVertex;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEndPrimitive<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpEndPrimitive;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEmitStreamVertex<TBuffer>(this TBuffer buffer, IdRef stream) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(stream);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEmitStreamVertex;
        mutInstruction.Add(stream);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEndStreamPrimitive<TBuffer>(this TBuffer buffer, IdRef stream) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(stream);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEndStreamPrimitive;
        mutInstruction.Add(stream);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpControlBarrier<TBuffer>(this TBuffer buffer, IdScope execution, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpControlBarrier;
        mutInstruction.Add(execution);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMemoryBarrier<TBuffer>(this TBuffer buffer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemoryBarrier;
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicLoad<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicLoad;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicStore<TBuffer>(this TBuffer buffer, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicStore;
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicExchange<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicExchange;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicCompareExchange<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics equal, IdMemorySemantics unequal, IdRef value, IdRef comparator) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(equal) + buffer.GetWordLength(unequal) + buffer.GetWordLength(value) + buffer.GetWordLength(comparator);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicCompareExchange;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(equal);
        mutInstruction.Add(unequal);
        mutInstruction.Add(value);
        mutInstruction.Add(comparator);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicCompareExchangeWeak<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics equal, IdMemorySemantics unequal, IdRef value, IdRef comparator) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(equal) + buffer.GetWordLength(unequal) + buffer.GetWordLength(value) + buffer.GetWordLength(comparator);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicCompareExchangeWeak;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(equal);
        mutInstruction.Add(unequal);
        mutInstruction.Add(value);
        mutInstruction.Add(comparator);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicIIncrement<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicIIncrement;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicIDecrement<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicIDecrement;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicIAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicIAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicISub<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicISub;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicSMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicSMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicUMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicUMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicSMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicSMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicUMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicUMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicAnd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicAnd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicOr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicOr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicXor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicXor;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPhi<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<PairIdRefIdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPhi;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLoopMerge<TBuffer>(this TBuffer buffer, IdRef mergeBlock, IdRef continueTarget, LoopControlMask loopcontrol) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mergeBlock) + buffer.GetWordLength(continueTarget) + buffer.GetWordLength(loopcontrol);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLoopMerge;
        mutInstruction.Add(mergeBlock);
        mutInstruction.Add(continueTarget);
        mutInstruction.Add(loopcontrol);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSelectionMerge<TBuffer>(this TBuffer buffer, IdRef mergeBlock, SelectionControlMask selectioncontrol) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mergeBlock) + buffer.GetWordLength(selectioncontrol);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSelectionMerge;
        mutInstruction.Add(mergeBlock);
        mutInstruction.Add(selectioncontrol);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLabel<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLabel;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBranch<TBuffer>(this TBuffer buffer, IdRef targetLabel) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(targetLabel);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBranch;
        mutInstruction.Add(targetLabel);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBranchConditional<TBuffer>(this TBuffer buffer, IdRef condition, IdRef trueLabel, IdRef falseLabel, Span<LiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(condition) + buffer.GetWordLength(trueLabel) + buffer.GetWordLength(falseLabel) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBranchConditional;
        mutInstruction.Add(condition);
        mutInstruction.Add(trueLabel);
        mutInstruction.Add(falseLabel);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSwitch<TBuffer>(this TBuffer buffer, IdRef selector, IdRef defaultId, Span<PairLiteralIntegerIdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(selector) + buffer.GetWordLength(defaultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSwitch;
        mutInstruction.Add(selector);
        mutInstruction.Add(defaultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpKill<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpKill;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReturn<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpReturn;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReturnValue<TBuffer>(this TBuffer buffer, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReturnValue;
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUnreachable<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpUnreachable;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLifetimeStart<TBuffer>(this TBuffer buffer, IdRef pointer, LiteralInteger size) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(size);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLifetimeStart;
        mutInstruction.Add(pointer);
        mutInstruction.Add(size);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLifetimeStop<TBuffer>(this TBuffer buffer, IdRef pointer, LiteralInteger size) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(size);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLifetimeStop;
        mutInstruction.Add(pointer);
        mutInstruction.Add(size);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupAsyncCopy<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef destination, IdRef source, IdRef numElements, IdRef stride, IdRef eventId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(destination) + buffer.GetWordLength(source) + buffer.GetWordLength(numElements) + buffer.GetWordLength(stride) + buffer.GetWordLength(eventId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupAsyncCopy;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(destination);
        mutInstruction.Add(source);
        mutInstruction.Add(numElements);
        mutInstruction.Add(stride);
        mutInstruction.Add(eventId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupWaitEvents<TBuffer>(this TBuffer buffer, IdScope execution, IdRef numEvents, IdRef eventsList) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(numEvents) + buffer.GetWordLength(eventsList);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupWaitEvents;
        mutInstruction.Add(execution);
        mutInstruction.Add(numEvents);
        mutInstruction.Add(eventsList);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupAll<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupAll;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupAny<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupAny;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupBroadcast<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef localId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(localId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupBroadcast;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(localId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupIAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupIAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupUMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupUMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupSMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupSMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupUMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupUMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupSMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupSMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReadPipe<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef pointer, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(pointer) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReadPipe;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(pointer);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpWritePipe<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef pointer, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(pointer) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpWritePipe;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(pointer);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReservedReadPipe<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef reserveId, IdRef index, IdRef pointer, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(index) + buffer.GetWordLength(pointer) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReservedReadPipe;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(index);
        mutInstruction.Add(pointer);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReservedWritePipe<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef reserveId, IdRef index, IdRef pointer, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(index) + buffer.GetWordLength(pointer) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReservedWritePipe;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(index);
        mutInstruction.Add(pointer);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReserveReadPipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef numPackets, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(numPackets) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReserveReadPipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(numPackets);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReserveWritePipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef numPackets, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(numPackets) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReserveWritePipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(numPackets);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCommitReadPipe<TBuffer>(this TBuffer buffer, IdRef pipe, IdRef reserveId, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCommitReadPipe;
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCommitWritePipe<TBuffer>(this TBuffer buffer, IdRef pipe, IdRef reserveId, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCommitWritePipe;
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsValidReserveId<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef reserveId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(reserveId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsValidReserveId;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(reserveId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetNumPipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetNumPipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetMaxPipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipe, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipe) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetMaxPipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipe);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupReserveReadPipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef pipe, IdRef numPackets, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(pipe) + buffer.GetWordLength(numPackets) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupReserveReadPipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(pipe);
        mutInstruction.Add(numPackets);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupReserveWritePipePackets<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef pipe, IdRef numPackets, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(pipe) + buffer.GetWordLength(numPackets) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupReserveWritePipePackets;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(pipe);
        mutInstruction.Add(numPackets);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupCommitReadPipe<TBuffer>(this TBuffer buffer, IdScope execution, IdRef pipe, IdRef reserveId, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupCommitReadPipe;
        mutInstruction.Add(execution);
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupCommitWritePipe<TBuffer>(this TBuffer buffer, IdScope execution, IdRef pipe, IdRef reserveId, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(pipe) + buffer.GetWordLength(reserveId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupCommitWritePipe;
        mutInstruction.Add(execution);
        mutInstruction.Add(pipe);
        mutInstruction.Add(reserveId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEnqueueMarker<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef queue, IdRef numEvents, IdRef waitEvents, IdRef retEvent) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(queue) + buffer.GetWordLength(numEvents) + buffer.GetWordLength(waitEvents) + buffer.GetWordLength(retEvent);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEnqueueMarker;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(queue);
        mutInstruction.Add(numEvents);
        mutInstruction.Add(waitEvents);
        mutInstruction.Add(retEvent);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEnqueueKernel<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef queue, IdRef flags, IdRef nDRange, IdRef numEvents, IdRef waitEvents, IdRef retEvent, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(queue) + buffer.GetWordLength(flags) + buffer.GetWordLength(nDRange) + buffer.GetWordLength(numEvents) + buffer.GetWordLength(waitEvents) + buffer.GetWordLength(retEvent) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEnqueueKernel;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(queue);
        mutInstruction.Add(flags);
        mutInstruction.Add(nDRange);
        mutInstruction.Add(numEvents);
        mutInstruction.Add(waitEvents);
        mutInstruction.Add(retEvent);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelNDrangeSubGroupCount<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef nDRange, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(nDRange) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelNDrangeSubGroupCount;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(nDRange);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelNDrangeMaxSubGroupSize<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef nDRange, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(nDRange) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelNDrangeMaxSubGroupSize;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(nDRange);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelWorkGroupSize<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelWorkGroupSize;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelPreferredWorkGroupSizeMultiple<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelPreferredWorkGroupSizeMultiple;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRetainEvent<TBuffer>(this TBuffer buffer, IdRef eventId) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(eventId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRetainEvent;
        mutInstruction.Add(eventId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReleaseEvent<TBuffer>(this TBuffer buffer, IdRef eventId) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(eventId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReleaseEvent;
        mutInstruction.Add(eventId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCreateUserEvent<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCreateUserEvent;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsValidEvent<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef eventId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(eventId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsValidEvent;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(eventId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSetUserEventStatus<TBuffer>(this TBuffer buffer, IdRef eventId, IdRef status) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(eventId) + buffer.GetWordLength(status);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSetUserEventStatus;
        mutInstruction.Add(eventId);
        mutInstruction.Add(status);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCaptureEventProfilingInfo<TBuffer>(this TBuffer buffer, IdRef eventId, IdRef profilingInfo, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(eventId) + buffer.GetWordLength(profilingInfo) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCaptureEventProfilingInfo;
        mutInstruction.Add(eventId);
        mutInstruction.Add(profilingInfo);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetDefaultQueue<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetDefaultQueue;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBuildNDRange<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef globalWorkSize, IdRef localWorkSize, IdRef globalWorkOffset) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(globalWorkSize) + buffer.GetWordLength(localWorkSize) + buffer.GetWordLength(globalWorkOffset);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpBuildNDRange;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(globalWorkSize);
        mutInstruction.Add(localWorkSize);
        mutInstruction.Add(globalWorkOffset);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleDrefImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleDrefImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleDrefExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleDrefExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleProjImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleProjImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleProjExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleProjExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleProjDrefImplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleProjDrefImplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseSampleProjDrefExplicitLod<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseSampleProjDrefExplicitLod;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseFetch<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseFetch;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseGather<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef component, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(component) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseGather;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(component);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseDrefGather<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef dref, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(dref) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseDrefGather;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(dref);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseTexelsResident<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef residentCode) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(residentCode);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseTexelsResident;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(residentCode);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpNoLine<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpNoLine;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicFlagTestAndSet<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicFlagTestAndSet;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicFlagClear<TBuffer>(this TBuffer buffer, IdRef pointer, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicFlagClear;
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSparseRead<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSparseRead;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSizeOf<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSizeOf;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypePipeStorage<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypePipeStorage;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantPipeStorage<TBuffer>(this TBuffer buffer, IdResultType resultType, LiteralInteger packetSize, LiteralInteger packetAlignment, LiteralInteger capacity) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment) + buffer.GetWordLength(capacity);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantPipeStorage;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        mutInstruction.Add(capacity);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCreatePipeFromPipeStorage<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pipeStorage) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pipeStorage);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCreatePipeFromPipeStorage;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pipeStorage);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelLocalSizeForSubgroupCount<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef subgroupCount, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(subgroupCount) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelLocalSizeForSubgroupCount;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(subgroupCount);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGetKernelMaxNumSubgroups<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef invoke, IdRef param, IdRef paramSize, IdRef paramAlign) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(invoke) + buffer.GetWordLength(param) + buffer.GetWordLength(paramSize) + buffer.GetWordLength(paramAlign);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGetKernelMaxNumSubgroups;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(invoke);
        mutInstruction.Add(param);
        mutInstruction.Add(paramSize);
        mutInstruction.Add(paramAlign);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeNamedBarrier<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeNamedBarrier;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpNamedBarrierInitialize<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef subgroupCount) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(subgroupCount);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpNamedBarrierInitialize;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(subgroupCount);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMemoryNamedBarrier<TBuffer>(this TBuffer buffer, IdRef namedBarrier, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(namedBarrier) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemoryNamedBarrier;
        mutInstruction.Add(namedBarrier);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpModuleProcessed<TBuffer>(this TBuffer buffer, LiteralString process) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(process);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpModuleProcessed;
        mutInstruction.Add(process);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExecutionModeId<TBuffer>(this TBuffer buffer, IdRef entryPoint, ExecutionMode mode) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(entryPoint) + buffer.GetWordLength(mode);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExecutionModeId;
        mutInstruction.Add(entryPoint);
        mutInstruction.Add(mode);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDecorateId<TBuffer>(this TBuffer buffer, IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpDecorate, target, (int)decoration, ..additional1.AsSpanOwner().Span, ..additional2.AsSpanOwner().Span, ..additionalString.AsSpanOwner().Span]));
    }
    public static Instruction AddOpGroupNonUniformElect<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformElect;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformAll<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformAll;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformAny<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformAny;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformAllEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformAllEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBroadcast<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef id) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(id);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBroadcast;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(id);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBroadcastFirst<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBroadcastFirst;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBallot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBallot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformInverseBallot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformInverseBallot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBallotBitExtract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef index) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(index);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBallotBitExtract;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(index);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBallotBitCount<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBallotBitCount;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBallotFindLSB<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBallotFindLSB;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBallotFindMSB<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBallotFindMSB;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformShuffle<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef id) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(id);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformShuffle;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(id);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformShuffleXor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef mask) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(mask);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformShuffleXor;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(mask);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformShuffleUp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef delta) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(delta);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformShuffleUp;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(delta);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformShuffleDown<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef delta) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(delta);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformShuffleDown;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(delta);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformIAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformIAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformFAdd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformFAdd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformIMul<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformIMul;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformFMul<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformFMul;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformSMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformSMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformUMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformUMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformFMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformFMin;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformSMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformSMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformUMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformUMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformFMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformFMax;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBitwiseAnd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBitwiseAnd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBitwiseOr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBitwiseOr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformBitwiseXor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformBitwiseXor;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformLogicalAnd<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformLogicalAnd;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformLogicalOr<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformLogicalOr;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformLogicalXor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef value, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(value) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformLogicalXor;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(value);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformQuadBroadcast<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef index) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(index);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformQuadBroadcast;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(index);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformQuadSwap<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef direction) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(direction);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformQuadSwap;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(direction);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCopyLogical<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCopyLogical;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrNotEqual<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrNotEqual;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrDiff<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrDiff;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpColorAttachmentReadEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef attachment, IdRef? sample) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(attachment) + buffer.GetWordLength(sample);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpColorAttachmentReadEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(attachment);
        mutInstruction.Add(sample);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDepthAttachmentReadEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef? sample) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sample);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpDepthAttachmentReadEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sample);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpStencilAttachmentReadEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef? sample) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sample);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpStencilAttachmentReadEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sample);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTerminateInvocation<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpTerminateInvocation;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupBallotKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupBallotKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupFirstInvocationKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupFirstInvocationKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAllKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAllKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAnyKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAnyKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAllEqualKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAllEqualKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformRotateKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, IdRef value, IdRef delta, IdRef? clusterSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(value) + buffer.GetWordLength(delta) + buffer.GetWordLength(clusterSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformRotateKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(value);
        mutInstruction.Add(delta);
        mutInstruction.Add(clusterSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupReadInvocationKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value, IdRef index) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value) + buffer.GetWordLength(index);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupReadInvocationKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        mutInstruction.Add(index);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExtInstWithForwardRefsKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef set, LiteralInteger instruction, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(set) + buffer.GetWordLength(instruction) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExtInstWithForwardRefsKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(set);
        mutInstruction.Add(instruction);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTraceRayKHR<TBuffer>(this TBuffer buffer, IdRef accel, IdRef rayFlags, IdRef cullMask, IdRef sBTOffset, IdRef sBTStride, IdRef missIndex, IdRef rayOrigin, IdRef rayTmin, IdRef rayDirection, IdRef rayTmax, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(accel) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullMask) + buffer.GetWordLength(sBTOffset) + buffer.GetWordLength(sBTStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(rayOrigin) + buffer.GetWordLength(rayTmin) + buffer.GetWordLength(rayDirection) + buffer.GetWordLength(rayTmax) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTraceRayKHR;
        mutInstruction.Add(accel);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullMask);
        mutInstruction.Add(sBTOffset);
        mutInstruction.Add(sBTStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(rayOrigin);
        mutInstruction.Add(rayTmin);
        mutInstruction.Add(rayDirection);
        mutInstruction.Add(rayTmax);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExecuteCallableKHR<TBuffer>(this TBuffer buffer, IdRef sBTIndex, IdRef callableData) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(sBTIndex) + buffer.GetWordLength(callableData);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExecuteCallableKHR;
        mutInstruction.Add(sBTIndex);
        mutInstruction.Add(callableData);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToAccelerationStructureKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef accel) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(accel);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToAccelerationStructureKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(accel);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIgnoreIntersectionKHR<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpIgnoreIntersectionKHR;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTerminateRayKHR<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpTerminateRayKHR;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDotKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDotKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUDot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUDot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUDotKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUDotKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSUDot<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSUDot;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSUDotKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSUDotKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDotAccSat<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDotAccSat;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDotAccSatKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDotAccSatKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUDotAccSat<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUDotAccSat;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUDotAccSatKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUDotAccSatKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSUDotAccSat<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSUDotAccSat;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSUDotAccSatKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef vector1, IdRef vector2, IdRef accumulator, PackedVectorFormat? packedVectorFormat) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(vector1) + buffer.GetWordLength(vector2) + buffer.GetWordLength(accumulator) + buffer.GetWordLength(packedVectorFormat);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSUDotAccSatKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(vector1);
        mutInstruction.Add(vector2);
        mutInstruction.Add(accumulator);
        mutInstruction.Add(packedVectorFormat);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeCooperativeMatrixKHR<TBuffer>(this TBuffer buffer, IdRef componentType, IdScope scope, IdRef rows, IdRef columns, IdRef use) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(componentType) + buffer.GetWordLength(scope) + buffer.GetWordLength(rows) + buffer.GetWordLength(columns) + buffer.GetWordLength(use);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeCooperativeMatrixKHR;
        mutInstruction.Add(resultId);
        mutInstruction.Add(componentType);
        mutInstruction.Add(scope);
        mutInstruction.Add(rows);
        mutInstruction.Add(columns);
        mutInstruction.Add(use);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixLoadKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdRef memoryLayout, IdRef? stride, MemoryAccessMask? memoryOperand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memoryLayout) + buffer.GetWordLength(stride) + buffer.GetWordLength(memoryOperand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixLoadKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memoryLayout);
        mutInstruction.Add(stride);
        mutInstruction.Add(memoryOperand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixStoreKHR<TBuffer>(this TBuffer buffer, IdRef pointer, IdRef objectId, IdRef memoryLayout, IdRef? stride, MemoryAccessMask? memoryOperand) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(objectId) + buffer.GetWordLength(memoryLayout) + buffer.GetWordLength(stride) + buffer.GetWordLength(memoryOperand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixStoreKHR;
        mutInstruction.Add(pointer);
        mutInstruction.Add(objectId);
        mutInstruction.Add(memoryLayout);
        mutInstruction.Add(stride);
        mutInstruction.Add(memoryOperand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixMulAddKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, IdRef b, IdRef c, CooperativeMatrixOperandsMask? cooperativeMatrixOperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(b) + buffer.GetWordLength(c) + buffer.GetWordLength(cooperativeMatrixOperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixMulAddKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(b);
        mutInstruction.Add(c);
        mutInstruction.Add(cooperativeMatrixOperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixLengthKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef type) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(type);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixLengthKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(type);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantCompositeReplicateEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantCompositeReplicateEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantCompositeReplicateEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantCompositeReplicateEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCompositeConstructReplicateEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCompositeConstructReplicateEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeRayQueryKHR<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeRayQueryKHR;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryInitializeKHR<TBuffer>(this TBuffer buffer, IdRef rayQuery, IdRef accel, IdRef rayFlags, IdRef cullMask, IdRef rayOrigin, IdRef rayTMin, IdRef rayDirection, IdRef rayTMax) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(accel) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullMask) + buffer.GetWordLength(rayOrigin) + buffer.GetWordLength(rayTMin) + buffer.GetWordLength(rayDirection) + buffer.GetWordLength(rayTMax);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryInitializeKHR;
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(accel);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullMask);
        mutInstruction.Add(rayOrigin);
        mutInstruction.Add(rayTMin);
        mutInstruction.Add(rayDirection);
        mutInstruction.Add(rayTMax);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryTerminateKHR<TBuffer>(this TBuffer buffer, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryTerminateKHR;
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGenerateIntersectionKHR<TBuffer>(this TBuffer buffer, IdRef rayQuery, IdRef hitT) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(hitT);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGenerateIntersectionKHR;
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(hitT);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryConfirmIntersectionKHR<TBuffer>(this TBuffer buffer, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryConfirmIntersectionKHR;
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryProceedKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryProceedKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionTypeKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionTypeKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleWeightedQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef texture, IdRef coordinates, IdRef weights) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(texture) + buffer.GetWordLength(coordinates) + buffer.GetWordLength(weights);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleWeightedQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(texture);
        mutInstruction.Add(coordinates);
        mutInstruction.Add(weights);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBoxFilterQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef texture, IdRef coordinates, IdRef boxSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(texture) + buffer.GetWordLength(coordinates) + buffer.GetWordLength(boxSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBoxFilterQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(texture);
        mutInstruction.Add(coordinates);
        mutInstruction.Add(boxSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchSSDQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef target, IdRef targetCoordinates, IdRef reference, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(target) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(reference) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchSSDQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(target);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(reference);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchSADQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef target, IdRef targetCoordinates, IdRef reference, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(target) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(reference) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchSADQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(target);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(reference);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchWindowSSDQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef targetSampledImage, IdRef targetCoordinates, IdRef referenceSampledImage, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(targetSampledImage) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(referenceSampledImage) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchWindowSSDQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(targetSampledImage);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(referenceSampledImage);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchWindowSADQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef targetSampledImage, IdRef targetCoordinates, IdRef referenceSampledImage, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(targetSampledImage) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(referenceSampledImage) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchWindowSADQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(targetSampledImage);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(referenceSampledImage);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchGatherSSDQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef targetSampledImage, IdRef targetCoordinates, IdRef referenceSampledImage, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(targetSampledImage) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(referenceSampledImage) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchGatherSSDQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(targetSampledImage);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(referenceSampledImage);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageBlockMatchGatherSADQCOM<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef targetSampledImage, IdRef targetCoordinates, IdRef referenceSampledImage, IdRef referenceCoordinates, IdRef blockSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(targetSampledImage) + buffer.GetWordLength(targetCoordinates) + buffer.GetWordLength(referenceSampledImage) + buffer.GetWordLength(referenceCoordinates) + buffer.GetWordLength(blockSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageBlockMatchGatherSADQCOM;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(targetSampledImage);
        mutInstruction.Add(targetCoordinates);
        mutInstruction.Add(referenceSampledImage);
        mutInstruction.Add(referenceCoordinates);
        mutInstruction.Add(blockSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupIAddNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupIAddNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFAddNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFAddNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFMinNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFMinNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupUMinNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupUMinNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupSMinNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupSMinNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFMaxNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFMaxNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupUMaxNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupUMaxNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupSMaxNonUniformAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupSMaxNonUniformAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFragmentMaskFetchAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFragmentMaskFetchAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFragmentFetchAMD<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, IdRef fragmentIndex) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(fragmentIndex);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFragmentFetchAMD;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(fragmentIndex);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReadClockKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope scope) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(scope);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReadClockKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(scope);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFinalizeNodePayloadsAMDX<TBuffer>(this TBuffer buffer, IdRef payloadArray) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(payloadArray);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFinalizeNodePayloadsAMDX;
        mutInstruction.Add(payloadArray);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFinishWritingNodePayloadAMDX<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFinishWritingNodePayloadAMDX;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpInitializeNodePayloadsAMDX<TBuffer>(this TBuffer buffer, IdRef payloadArray, IdScope visibility, IdRef payloadCount, IdRef nodeIndex) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(payloadArray) + buffer.GetWordLength(visibility) + buffer.GetWordLength(payloadCount) + buffer.GetWordLength(nodeIndex);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpInitializeNodePayloadsAMDX;
        mutInstruction.Add(payloadArray);
        mutInstruction.Add(visibility);
        mutInstruction.Add(payloadCount);
        mutInstruction.Add(nodeIndex);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformQuadAllKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformQuadAllKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformQuadAnyKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef predicate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(predicate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformQuadAnyKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(predicate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordHitMotionNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef instanceId, IdRef primitiveId, IdRef geometryIndex, IdRef hitKind, IdRef sBTRecordOffset, IdRef sBTRecordStride, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef currentTime, IdRef hitObjectAttributes) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(primitiveId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(hitKind) + buffer.GetWordLength(sBTRecordOffset) + buffer.GetWordLength(sBTRecordStride) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(currentTime) + buffer.GetWordLength(hitObjectAttributes);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordHitMotionNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(primitiveId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(hitKind);
        mutInstruction.Add(sBTRecordOffset);
        mutInstruction.Add(sBTRecordStride);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(currentTime);
        mutInstruction.Add(hitObjectAttributes);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordHitWithIndexMotionNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef instanceId, IdRef primitiveId, IdRef geometryIndex, IdRef hitKind, IdRef sBTRecordIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef currentTime, IdRef hitObjectAttributes) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(primitiveId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(hitKind) + buffer.GetWordLength(sBTRecordIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(currentTime) + buffer.GetWordLength(hitObjectAttributes);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordHitWithIndexMotionNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(primitiveId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(hitKind);
        mutInstruction.Add(sBTRecordIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(currentTime);
        mutInstruction.Add(hitObjectAttributes);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordMissMotionNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef sBTIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef currentTime) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(sBTIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(currentTime);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordMissMotionNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(sBTIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(currentTime);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetWorldToObjectNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetWorldToObjectNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetObjectToWorldNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetObjectToWorldNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetObjectRayDirectionNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetObjectRayDirectionNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetObjectRayOriginNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetObjectRayOriginNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectTraceRayMotionNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef rayFlags, IdRef cullmask, IdRef sBTRecordOffset, IdRef sBTRecordStride, IdRef missIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef time, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullmask) + buffer.GetWordLength(sBTRecordOffset) + buffer.GetWordLength(sBTRecordStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(time) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectTraceRayMotionNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullmask);
        mutInstruction.Add(sBTRecordOffset);
        mutInstruction.Add(sBTRecordStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(time);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetShaderRecordBufferHandleNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetShaderRecordBufferHandleNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetShaderBindingTableRecordIndexNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetShaderBindingTableRecordIndexNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordEmptyNV<TBuffer>(this TBuffer buffer, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordEmptyNV;
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectTraceRayNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef rayFlags, IdRef cullmask, IdRef sBTRecordOffset, IdRef sBTRecordStride, IdRef missIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullmask) + buffer.GetWordLength(sBTRecordOffset) + buffer.GetWordLength(sBTRecordStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectTraceRayNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullmask);
        mutInstruction.Add(sBTRecordOffset);
        mutInstruction.Add(sBTRecordStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordHitNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef instanceId, IdRef primitiveId, IdRef geometryIndex, IdRef hitKind, IdRef sBTRecordOffset, IdRef sBTRecordStride, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef hitObjectAttributes) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(primitiveId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(hitKind) + buffer.GetWordLength(sBTRecordOffset) + buffer.GetWordLength(sBTRecordStride) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(hitObjectAttributes);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordHitNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(primitiveId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(hitKind);
        mutInstruction.Add(sBTRecordOffset);
        mutInstruction.Add(sBTRecordStride);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(hitObjectAttributes);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordHitWithIndexNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef accelerationStructure, IdRef instanceId, IdRef primitiveId, IdRef geometryIndex, IdRef hitKind, IdRef sBTRecordIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax, IdRef hitObjectAttributes) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(accelerationStructure) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(primitiveId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(hitKind) + buffer.GetWordLength(sBTRecordIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax) + buffer.GetWordLength(hitObjectAttributes);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordHitWithIndexNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(accelerationStructure);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(primitiveId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(hitKind);
        mutInstruction.Add(sBTRecordIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        mutInstruction.Add(hitObjectAttributes);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectRecordMissNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef sBTIndex, IdRef origin, IdRef tMin, IdRef direction, IdRef tMax) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(sBTIndex) + buffer.GetWordLength(origin) + buffer.GetWordLength(tMin) + buffer.GetWordLength(direction) + buffer.GetWordLength(tMax);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectRecordMissNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(sBTIndex);
        mutInstruction.Add(origin);
        mutInstruction.Add(tMin);
        mutInstruction.Add(direction);
        mutInstruction.Add(tMax);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectExecuteShaderNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectExecuteShaderNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetCurrentTimeNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetCurrentTimeNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetAttributesNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef hitObjectAttribute) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(hitObjectAttribute);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetAttributesNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(hitObjectAttribute);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetHitKindNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetHitKindNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetPrimitiveIndexNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetPrimitiveIndexNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetGeometryIndexNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetGeometryIndexNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetInstanceIdNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetInstanceIdNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetInstanceCustomIndexNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetInstanceCustomIndexNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetWorldRayDirectionNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetWorldRayDirectionNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetWorldRayOriginNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetWorldRayOriginNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetRayTMaxNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetRayTMaxNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectGetRayTMinNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectGetRayTMinNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectIsEmptyNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectIsEmptyNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectIsHitNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectIsHitNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpHitObjectIsMissNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hitObject) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hitObject);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpHitObjectIsMissNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hitObject);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReorderThreadWithHitObjectNV<TBuffer>(this TBuffer buffer, IdRef hitObject, IdRef? hint, IdRef? bits) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hitObject) + buffer.GetWordLength(hint) + buffer.GetWordLength(bits);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReorderThreadWithHitObjectNV;
        mutInstruction.Add(hitObject);
        mutInstruction.Add(hint);
        mutInstruction.Add(bits);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReorderThreadWithHintNV<TBuffer>(this TBuffer buffer, IdRef hint, IdRef bits) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(hint) + buffer.GetWordLength(bits);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReorderThreadWithHintNV;
        mutInstruction.Add(hint);
        mutInstruction.Add(bits);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeHitObjectNV<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeHitObjectNV;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpImageSampleFootprintNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sampledImage, IdRef coordinate, IdRef granularity, IdRef coarse, ImageOperandsMask? imageoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sampledImage) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(granularity) + buffer.GetWordLength(coarse) + buffer.GetWordLength(imageoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpImageSampleFootprintNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sampledImage);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(granularity);
        mutInstruction.Add(coarse);
        mutInstruction.Add(imageoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEmitMeshTasksEXT<TBuffer>(this TBuffer buffer, IdRef groupCountX, IdRef groupCountY, IdRef groupCountZ, IdRef? payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(groupCountX) + buffer.GetWordLength(groupCountY) + buffer.GetWordLength(groupCountZ) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpEmitMeshTasksEXT;
        mutInstruction.Add(groupCountX);
        mutInstruction.Add(groupCountY);
        mutInstruction.Add(groupCountZ);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSetMeshOutputsEXT<TBuffer>(this TBuffer buffer, IdRef vertexCount, IdRef primitiveCount) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(vertexCount) + buffer.GetWordLength(primitiveCount);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSetMeshOutputsEXT;
        mutInstruction.Add(vertexCount);
        mutInstruction.Add(primitiveCount);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupNonUniformPartitionNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupNonUniformPartitionNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpWritePackedPrimitiveIndices4x8NV<TBuffer>(this TBuffer buffer, IdRef indexOffset, IdRef packedIndices) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(indexOffset) + buffer.GetWordLength(packedIndices);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpWritePackedPrimitiveIndices4x8NV;
        mutInstruction.Add(indexOffset);
        mutInstruction.Add(packedIndices);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFetchMicroTriangleVertexPositionNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef accel, IdRef instanceId, IdRef geometryIndex, IdRef primitiveIndex, IdRef barycentric) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(accel) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(primitiveIndex) + buffer.GetWordLength(barycentric);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFetchMicroTriangleVertexPositionNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(accel);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(primitiveIndex);
        mutInstruction.Add(barycentric);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFetchMicroTriangleVertexBarycentricNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef accel, IdRef instanceId, IdRef geometryIndex, IdRef primitiveIndex, IdRef barycentric) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(accel) + buffer.GetWordLength(instanceId) + buffer.GetWordLength(geometryIndex) + buffer.GetWordLength(primitiveIndex) + buffer.GetWordLength(barycentric);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFetchMicroTriangleVertexBarycentricNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(accel);
        mutInstruction.Add(instanceId);
        mutInstruction.Add(geometryIndex);
        mutInstruction.Add(primitiveIndex);
        mutInstruction.Add(barycentric);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReportIntersectionKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hit, IdRef hitKind) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hit) + buffer.GetWordLength(hitKind);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReportIntersectionKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hit);
        mutInstruction.Add(hitKind);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReportIntersectionNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef hit, IdRef hitKind) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(hit) + buffer.GetWordLength(hitKind);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReportIntersectionNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(hit);
        mutInstruction.Add(hitKind);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIgnoreIntersectionNV<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpIgnoreIntersectionNV;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTerminateRayNV<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpTerminateRayNV;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTraceNV<TBuffer>(this TBuffer buffer, IdRef accel, IdRef rayFlags, IdRef cullMask, IdRef sBTOffset, IdRef sBTStride, IdRef missIndex, IdRef rayOrigin, IdRef rayTmin, IdRef rayDirection, IdRef rayTmax, IdRef payloadId) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(accel) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullMask) + buffer.GetWordLength(sBTOffset) + buffer.GetWordLength(sBTStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(rayOrigin) + buffer.GetWordLength(rayTmin) + buffer.GetWordLength(rayDirection) + buffer.GetWordLength(rayTmax) + buffer.GetWordLength(payloadId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTraceNV;
        mutInstruction.Add(accel);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullMask);
        mutInstruction.Add(sBTOffset);
        mutInstruction.Add(sBTStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(rayOrigin);
        mutInstruction.Add(rayTmin);
        mutInstruction.Add(rayDirection);
        mutInstruction.Add(rayTmax);
        mutInstruction.Add(payloadId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTraceMotionNV<TBuffer>(this TBuffer buffer, IdRef accel, IdRef rayFlags, IdRef cullMask, IdRef sBTOffset, IdRef sBTStride, IdRef missIndex, IdRef rayOrigin, IdRef rayTmin, IdRef rayDirection, IdRef rayTmax, IdRef time, IdRef payloadId) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(accel) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullMask) + buffer.GetWordLength(sBTOffset) + buffer.GetWordLength(sBTStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(rayOrigin) + buffer.GetWordLength(rayTmin) + buffer.GetWordLength(rayDirection) + buffer.GetWordLength(rayTmax) + buffer.GetWordLength(time) + buffer.GetWordLength(payloadId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTraceMotionNV;
        mutInstruction.Add(accel);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullMask);
        mutInstruction.Add(sBTOffset);
        mutInstruction.Add(sBTStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(rayOrigin);
        mutInstruction.Add(rayTmin);
        mutInstruction.Add(rayDirection);
        mutInstruction.Add(rayTmax);
        mutInstruction.Add(time);
        mutInstruction.Add(payloadId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTraceRayMotionNV<TBuffer>(this TBuffer buffer, IdRef accel, IdRef rayFlags, IdRef cullMask, IdRef sBTOffset, IdRef sBTStride, IdRef missIndex, IdRef rayOrigin, IdRef rayTmin, IdRef rayDirection, IdRef rayTmax, IdRef time, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(accel) + buffer.GetWordLength(rayFlags) + buffer.GetWordLength(cullMask) + buffer.GetWordLength(sBTOffset) + buffer.GetWordLength(sBTStride) + buffer.GetWordLength(missIndex) + buffer.GetWordLength(rayOrigin) + buffer.GetWordLength(rayTmin) + buffer.GetWordLength(rayDirection) + buffer.GetWordLength(rayTmax) + buffer.GetWordLength(time) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTraceRayMotionNV;
        mutInstruction.Add(accel);
        mutInstruction.Add(rayFlags);
        mutInstruction.Add(cullMask);
        mutInstruction.Add(sBTOffset);
        mutInstruction.Add(sBTStride);
        mutInstruction.Add(missIndex);
        mutInstruction.Add(rayOrigin);
        mutInstruction.Add(rayTmin);
        mutInstruction.Add(rayDirection);
        mutInstruction.Add(rayTmax);
        mutInstruction.Add(time);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionTriangleVertexPositionsKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionTriangleVertexPositionsKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAccelerationStructureKHR<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAccelerationStructureKHR;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAccelerationStructureNV<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAccelerationStructureNV;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExecuteCallableNV<TBuffer>(this TBuffer buffer, IdRef sBTIndex, IdRef callableDataId) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(sBTIndex) + buffer.GetWordLength(callableDataId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExecuteCallableNV;
        mutInstruction.Add(sBTIndex);
        mutInstruction.Add(callableDataId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeCooperativeMatrixNV<TBuffer>(this TBuffer buffer, IdRef componentType, IdScope execution, IdRef rows, IdRef columns) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(componentType) + buffer.GetWordLength(execution) + buffer.GetWordLength(rows) + buffer.GetWordLength(columns);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeCooperativeMatrixNV;
        mutInstruction.Add(resultId);
        mutInstruction.Add(componentType);
        mutInstruction.Add(execution);
        mutInstruction.Add(rows);
        mutInstruction.Add(columns);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixLoadNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdRef stride, IdRef columnMajor, MemoryAccessMask? memoryaccess) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(stride) + buffer.GetWordLength(columnMajor) + buffer.GetWordLength(memoryaccess);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixLoadNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(stride);
        mutInstruction.Add(columnMajor);
        mutInstruction.Add(memoryaccess);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixStoreNV<TBuffer>(this TBuffer buffer, IdRef pointer, IdRef objectId, IdRef stride, IdRef columnMajor, MemoryAccessMask? memoryaccess) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(pointer) + buffer.GetWordLength(objectId) + buffer.GetWordLength(stride) + buffer.GetWordLength(columnMajor) + buffer.GetWordLength(memoryaccess);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixStoreNV;
        mutInstruction.Add(pointer);
        mutInstruction.Add(objectId);
        mutInstruction.Add(stride);
        mutInstruction.Add(columnMajor);
        mutInstruction.Add(memoryaccess);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixMulAddNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, IdRef b, IdRef c) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(b) + buffer.GetWordLength(c);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixMulAddNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(b);
        mutInstruction.Add(c);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCooperativeMatrixLengthNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef type) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(type);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCooperativeMatrixLengthNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(type);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpBeginInvocationInterlockEXT<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpBeginInvocationInterlockEXT;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpEndInvocationInterlockEXT<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpEndInvocationInterlockEXT;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDemoteToHelperInvocation<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpDemoteToHelperInvocation;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDemoteToHelperInvocationEXT<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpDemoteToHelperInvocationEXT;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIsHelperInvocationEXT<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIsHelperInvocationEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToImageNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToImageNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToSamplerNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToSamplerNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertImageToUNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertImageToUNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertSamplerToUNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertSamplerToUNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertUToSampledImageNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertUToSampledImageNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertSampledImageToUNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertSampledImageToUNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSamplerImageAddressingModeNV<TBuffer>(this TBuffer buffer, LiteralInteger bitWidth) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(bitWidth);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSamplerImageAddressingModeNV;
        mutInstruction.Add(bitWidth);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRawAccessChainNV<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef baseId, IdRef bytestride, IdRef elementindex, IdRef byteoffset, RawAccessChainOperandsMask? rawaccesschainoperands) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(baseId) + buffer.GetWordLength(bytestride) + buffer.GetWordLength(elementindex) + buffer.GetWordLength(byteoffset) + buffer.GetWordLength(rawaccesschainoperands);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRawAccessChainNV;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(baseId);
        mutInstruction.Add(bytestride);
        mutInstruction.Add(elementindex);
        mutInstruction.Add(byteoffset);
        mutInstruction.Add(rawaccesschainoperands);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupShuffleINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef data, IdRef invocationId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(data) + buffer.GetWordLength(invocationId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupShuffleINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(data);
        mutInstruction.Add(invocationId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupShuffleDownINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef current, IdRef next, IdRef delta) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(current) + buffer.GetWordLength(next) + buffer.GetWordLength(delta);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupShuffleDownINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(current);
        mutInstruction.Add(next);
        mutInstruction.Add(delta);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupShuffleUpINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef previous, IdRef current, IdRef delta) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(previous) + buffer.GetWordLength(current) + buffer.GetWordLength(delta);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupShuffleUpINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(previous);
        mutInstruction.Add(current);
        mutInstruction.Add(delta);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupShuffleXorINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef data, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(data) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupShuffleXorINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(data);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupBlockReadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef ptr) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(ptr);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupBlockReadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(ptr);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupBlockWriteINTEL<TBuffer>(this TBuffer buffer, IdRef ptr, IdRef data) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(ptr) + buffer.GetWordLength(data);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupBlockWriteINTEL;
        mutInstruction.Add(ptr);
        mutInstruction.Add(data);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupImageBlockReadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupImageBlockReadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupImageBlockWriteINTEL<TBuffer>(this TBuffer buffer, IdRef image, IdRef coordinate, IdRef data) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(data);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupImageBlockWriteINTEL;
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(data);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupImageMediaBlockReadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef image, IdRef coordinate, IdRef width, IdRef height) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(width) + buffer.GetWordLength(height);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupImageMediaBlockReadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(width);
        mutInstruction.Add(height);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupImageMediaBlockWriteINTEL<TBuffer>(this TBuffer buffer, IdRef image, IdRef coordinate, IdRef width, IdRef height, IdRef data) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(image) + buffer.GetWordLength(coordinate) + buffer.GetWordLength(width) + buffer.GetWordLength(height) + buffer.GetWordLength(data);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupImageMediaBlockWriteINTEL;
        mutInstruction.Add(image);
        mutInstruction.Add(coordinate);
        mutInstruction.Add(width);
        mutInstruction.Add(height);
        mutInstruction.Add(data);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUCountLeadingZerosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUCountLeadingZerosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUCountTrailingZerosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUCountTrailingZerosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAbsISubINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAbsISubINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAbsUSubINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAbsUSubINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIAddSatINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIAddSatINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUAddSatINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUAddSatINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIAverageINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIAverageINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUAverageINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUAverageINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIAverageRoundedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIAverageRoundedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUAverageRoundedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUAverageRoundedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpISubSatINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpISubSatINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUSubSatINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUSubSatINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpIMul32x16INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpIMul32x16INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpUMul32x16INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef operand1, IdRef operand2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(operand1) + buffer.GetWordLength(operand2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpUMul32x16INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(operand1);
        mutInstruction.Add(operand2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantFunctionPointerINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef function) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(function);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantFunctionPointerINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(function);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFunctionPointerCallINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFunctionPointerCallINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAsmTargetINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, LiteralString asmtarget) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(asmtarget);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAsmTargetINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(asmtarget);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAsmINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef asmtype, IdRef target, LiteralString asminstructions, LiteralString constraints) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(asmtype) + buffer.GetWordLength(target) + buffer.GetWordLength(asminstructions) + buffer.GetWordLength(constraints);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAsmINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(asmtype);
        mutInstruction.Add(target);
        mutInstruction.Add(asminstructions);
        mutInstruction.Add(constraints);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAsmCallINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef asm, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(asm) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAsmCallINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(asm);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicFMinEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicFMinEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicFMaxEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicFMaxEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAssumeTrueKHR<TBuffer>(this TBuffer buffer, IdRef condition) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(condition);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAssumeTrueKHR;
        mutInstruction.Add(condition);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpExpectKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value, IdRef expectedValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(value) + buffer.GetWordLength(expectedValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpExpectKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(value);
        mutInstruction.Add(expectedValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpDecorateString<TBuffer>(this TBuffer buffer, IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpDecorate, target, (int)decoration, ..additional1.AsSpanOwner().Span, ..additional2.AsSpanOwner().Span, ..additionalString.AsSpanOwner().Span]));
    }
    public static Instruction AddOpDecorateStringGOOGLE<TBuffer>(this TBuffer buffer, IdRef target, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(target) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        return buffer.Add(new MutRefInstruction([wordLength << 16 | (int)SDSLOp.OpDecorate, target, (int)decoration, ..additional1.AsSpanOwner().Span, ..additional2.AsSpanOwner().Span, ..additionalString.AsSpanOwner().Span]));
    }
    public static Instruction AddOpMemberDecorateString<TBuffer>(this TBuffer buffer, IdRef structureType, LiteralInteger member, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(structureType) + buffer.GetWordLength(member) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemberDecorate;
        mutInstruction.Add(structureType);
        mutInstruction.Add(member);
        mutInstruction.Add(decoration);
        mutInstruction.Add(additional1);
        mutInstruction.Add(additional2);
        mutInstruction.Add(additionalString);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMemberDecorateStringGOOGLE<TBuffer>(this TBuffer buffer, IdRef structureType, LiteralInteger member, Decoration decoration, int? additional1 = null, int? additional2 = null, string? additionalString = null) where TBuffer : IMutSpirvBuffer{
        var wordLength = 1 + buffer.GetWordLength(structureType) + buffer.GetWordLength(member) + buffer.GetWordLength(decoration) + buffer.GetWordLength(additional1) + buffer.GetWordLength(additional2) + buffer.GetWordLength(additionalString);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMemberDecorate;
        mutInstruction.Add(structureType);
        mutInstruction.Add(member);
        mutInstruction.Add(decoration);
        mutInstruction.Add(additional1);
        mutInstruction.Add(additional2);
        mutInstruction.Add(additionalString);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVmeImageINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef imageType, IdRef sampler) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(imageType) + buffer.GetWordLength(sampler);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVmeImageINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(imageType);
        mutInstruction.Add(sampler);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeVmeImageINTEL<TBuffer>(this TBuffer buffer, IdRef imageType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(imageType);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeVmeImageINTEL;
        mutInstruction.Add(resultId);
        mutInstruction.Add(imageType);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImePayloadINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImePayloadINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcRefPayloadINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcRefPayloadINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcSicPayloadINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcSicPayloadINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcMcePayloadINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcMcePayloadINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcMceResultINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcMceResultINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImeResultINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImeResultINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImeResultSingleReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImeResultSingleReferenceStreamoutINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImeResultDualReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImeResultDualReferenceStreamoutINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImeSingleReferenceStreaminINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImeSingleReferenceStreaminINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcImeDualReferenceStreaminINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcImeDualReferenceStreaminINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcRefResultINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcRefResultINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeAvcSicResultINTEL<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeAvcSicResultINTEL;
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef referenceBasePenalty, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(referenceBasePenalty) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(referenceBasePenalty);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetInterShapePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedShapePenalty, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedShapePenalty) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetInterShapePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedShapePenalty);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetInterDirectionPenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef directionCost, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(directionCost) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetInterDirectionPenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(directionCost);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetMotionVectorCostFunctionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedCostCenterDelta, IdRef packedCostTable, IdRef costPrecision, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedCostCenterDelta) + buffer.GetWordLength(packedCostTable) + buffer.GetWordLength(costPrecision) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedCostCenterDelta);
        mutInstruction.Add(packedCostTable);
        mutInstruction.Add(costPrecision);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sliceType, IdRef qp) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sliceType) + buffer.GetWordLength(qp);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sliceType);
        mutInstruction.Add(qp);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetAcOnlyHaarINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetAcOnlyHaarINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef sourceFieldPolarity, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(sourceFieldPolarity) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(sourceFieldPolarity);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef referenceFieldPolarity, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(referenceFieldPolarity) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(referenceFieldPolarity);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef forwardReferenceFieldPolarity, IdRef backwardReferenceFieldPolarity, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(forwardReferenceFieldPolarity) + buffer.GetWordLength(backwardReferenceFieldPolarity) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(forwardReferenceFieldPolarity);
        mutInstruction.Add(backwardReferenceFieldPolarity);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToImePayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToImePayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToImeResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToImeResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToRefPayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToRefPayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToRefResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToRefResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToSicPayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToSicPayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceConvertToSicResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceConvertToSicResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetMotionVectorsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetMotionVectorsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterDistortionsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterDistortionsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetBestInterDistortionsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetBestInterDistortionsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterMajorShapeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterMajorShapeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterMinorShapeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterMinorShapeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterDirectionsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterDirectionsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterMotionVectorCountINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterMotionVectorCountINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterReferenceIdsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterReferenceIdsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedReferenceIds, IdRef packedReferenceParameterFieldPolarities, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedReferenceIds) + buffer.GetWordLength(packedReferenceParameterFieldPolarities) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedReferenceIds);
        mutInstruction.Add(packedReferenceParameterFieldPolarities);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeInitializeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcCoord, IdRef partitionMask, IdRef sADAdjustment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcCoord) + buffer.GetWordLength(partitionMask) + buffer.GetWordLength(sADAdjustment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeInitializeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcCoord);
        mutInstruction.Add(partitionMask);
        mutInstruction.Add(sADAdjustment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetSingleReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef refOffset, IdRef searchWindowConfig, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(refOffset) + buffer.GetWordLength(searchWindowConfig) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetSingleReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(refOffset);
        mutInstruction.Add(searchWindowConfig);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetDualReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef fwdRefOffset, IdRef bwdRefOffset, IdRef idSearchWindowConfig, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(fwdRefOffset) + buffer.GetWordLength(bwdRefOffset) + buffer.GetWordLength(idSearchWindowConfig) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetDualReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(fwdRefOffset);
        mutInstruction.Add(bwdRefOffset);
        mutInstruction.Add(idSearchWindowConfig);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeRefWindowSizeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef searchWindowConfig, IdRef dualRef) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(searchWindowConfig) + buffer.GetWordLength(dualRef);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeRefWindowSizeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(searchWindowConfig);
        mutInstruction.Add(dualRef);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeAdjustRefOffsetINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef refOffset, IdRef srcCoord, IdRef refWindowSize, IdRef imageSize) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(refOffset) + buffer.GetWordLength(srcCoord) + buffer.GetWordLength(refWindowSize) + buffer.GetWordLength(imageSize);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeAdjustRefOffsetINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(refOffset);
        mutInstruction.Add(srcCoord);
        mutInstruction.Add(refWindowSize);
        mutInstruction.Add(imageSize);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeConvertToMcePayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeConvertToMcePayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetMaxMotionVectorCountINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef maxMotionVectorCount, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(maxMotionVectorCount) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetMaxMotionVectorCountINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(maxMotionVectorCount);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetUnidirectionalMixDisableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef threshold, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(threshold) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(threshold);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeSetWeightedSadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedSadWeights, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedSadWeights) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeSetWeightedSadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedSadWeights);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithSingleReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithDualReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithDualReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload, IdRef streaminComponents) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload) + buffer.GetWordLength(streaminComponents);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        mutInstruction.Add(streaminComponents);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload, IdRef streaminComponents) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload) + buffer.GetWordLength(streaminComponents);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        mutInstruction.Add(streaminComponents);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload, IdRef streaminComponents) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload) + buffer.GetWordLength(streaminComponents);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        mutInstruction.Add(streaminComponents);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload, IdRef streaminComponents) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload) + buffer.GetWordLength(streaminComponents);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        mutInstruction.Add(streaminComponents);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeConvertToMceResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeConvertToMceResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetSingleReferenceStreaminINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetSingleReferenceStreaminINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetDualReferenceStreaminINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetDualReferenceStreaminINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeStripSingleReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeStripDualReferenceStreamoutINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeStripDualReferenceStreamoutINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape, IdRef direction) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape) + buffer.GetWordLength(direction);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        mutInstruction.Add(direction);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape, IdRef direction) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape) + buffer.GetWordLength(direction);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        mutInstruction.Add(direction);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload, IdRef majorShape, IdRef direction) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload) + buffer.GetWordLength(majorShape) + buffer.GetWordLength(direction);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        mutInstruction.Add(majorShape);
        mutInstruction.Add(direction);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetBorderReachedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef imageSelect, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(imageSelect) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetBorderReachedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(imageSelect);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetTruncatedSearchIndicationINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcFmeInitializeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcCoord, IdRef motionVectors, IdRef majorShapes, IdRef minorShapes, IdRef direction, IdRef pixelResolution, IdRef sadAdjustment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcCoord) + buffer.GetWordLength(motionVectors) + buffer.GetWordLength(majorShapes) + buffer.GetWordLength(minorShapes) + buffer.GetWordLength(direction) + buffer.GetWordLength(pixelResolution) + buffer.GetWordLength(sadAdjustment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcFmeInitializeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcCoord);
        mutInstruction.Add(motionVectors);
        mutInstruction.Add(majorShapes);
        mutInstruction.Add(minorShapes);
        mutInstruction.Add(direction);
        mutInstruction.Add(pixelResolution);
        mutInstruction.Add(sadAdjustment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcBmeInitializeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcCoord, IdRef motionVectors, IdRef majorShapes, IdRef minorShapes, IdRef direction, IdRef pixelResolution, IdRef bidirectionalWeight, IdRef sadAdjustment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcCoord) + buffer.GetWordLength(motionVectors) + buffer.GetWordLength(majorShapes) + buffer.GetWordLength(minorShapes) + buffer.GetWordLength(direction) + buffer.GetWordLength(pixelResolution) + buffer.GetWordLength(bidirectionalWeight) + buffer.GetWordLength(sadAdjustment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcBmeInitializeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcCoord);
        mutInstruction.Add(motionVectors);
        mutInstruction.Add(majorShapes);
        mutInstruction.Add(minorShapes);
        mutInstruction.Add(direction);
        mutInstruction.Add(pixelResolution);
        mutInstruction.Add(bidirectionalWeight);
        mutInstruction.Add(sadAdjustment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefConvertToMcePayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefConvertToMcePayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefSetBidirectionalMixDisableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefSetBidirectionalMixDisableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefSetBilinearFilterEnableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefSetBilinearFilterEnableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefEvaluateWithSingleReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefEvaluateWithDualReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefEvaluateWithDualReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefEvaluateWithMultiReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef packedReferenceIds, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(packedReferenceIds) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(packedReferenceIds);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef packedReferenceIds, IdRef packedReferenceFieldPolarities, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(packedReferenceIds) + buffer.GetWordLength(packedReferenceFieldPolarities) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(packedReferenceIds);
        mutInstruction.Add(packedReferenceFieldPolarities);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcRefConvertToMceResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcRefConvertToMceResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicInitializeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcCoord) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcCoord);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicInitializeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcCoord);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicConfigureSkcINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef skipBlockPartitionType, IdRef skipMotionVectorMask, IdRef motionVectors, IdRef bidirectionalWeight, IdRef sadAdjustment, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(skipBlockPartitionType) + buffer.GetWordLength(skipMotionVectorMask) + buffer.GetWordLength(motionVectors) + buffer.GetWordLength(bidirectionalWeight) + buffer.GetWordLength(sadAdjustment) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicConfigureSkcINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(skipBlockPartitionType);
        mutInstruction.Add(skipMotionVectorMask);
        mutInstruction.Add(motionVectors);
        mutInstruction.Add(bidirectionalWeight);
        mutInstruction.Add(sadAdjustment);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicConfigureIpeLumaINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef lumaIntraPartitionMask, IdRef intraNeighbourAvailabilty, IdRef leftEdgeLumaPixels, IdRef upperLeftCornerLumaPixel, IdRef upperEdgeLumaPixels, IdRef upperRightEdgeLumaPixels, IdRef sadAdjustment, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(lumaIntraPartitionMask) + buffer.GetWordLength(intraNeighbourAvailabilty) + buffer.GetWordLength(leftEdgeLumaPixels) + buffer.GetWordLength(upperLeftCornerLumaPixel) + buffer.GetWordLength(upperEdgeLumaPixels) + buffer.GetWordLength(upperRightEdgeLumaPixels) + buffer.GetWordLength(sadAdjustment) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicConfigureIpeLumaINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(lumaIntraPartitionMask);
        mutInstruction.Add(intraNeighbourAvailabilty);
        mutInstruction.Add(leftEdgeLumaPixels);
        mutInstruction.Add(upperLeftCornerLumaPixel);
        mutInstruction.Add(upperEdgeLumaPixels);
        mutInstruction.Add(upperRightEdgeLumaPixels);
        mutInstruction.Add(sadAdjustment);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicConfigureIpeLumaChromaINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef lumaIntraPartitionMask, IdRef intraNeighbourAvailabilty, IdRef leftEdgeLumaPixels, IdRef upperLeftCornerLumaPixel, IdRef upperEdgeLumaPixels, IdRef upperRightEdgeLumaPixels, IdRef leftEdgeChromaPixels, IdRef upperLeftCornerChromaPixel, IdRef upperEdgeChromaPixels, IdRef sadAdjustment, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(lumaIntraPartitionMask) + buffer.GetWordLength(intraNeighbourAvailabilty) + buffer.GetWordLength(leftEdgeLumaPixels) + buffer.GetWordLength(upperLeftCornerLumaPixel) + buffer.GetWordLength(upperEdgeLumaPixels) + buffer.GetWordLength(upperRightEdgeLumaPixels) + buffer.GetWordLength(leftEdgeChromaPixels) + buffer.GetWordLength(upperLeftCornerChromaPixel) + buffer.GetWordLength(upperEdgeChromaPixels) + buffer.GetWordLength(sadAdjustment) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicConfigureIpeLumaChromaINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(lumaIntraPartitionMask);
        mutInstruction.Add(intraNeighbourAvailabilty);
        mutInstruction.Add(leftEdgeLumaPixels);
        mutInstruction.Add(upperLeftCornerLumaPixel);
        mutInstruction.Add(upperEdgeLumaPixels);
        mutInstruction.Add(upperRightEdgeLumaPixels);
        mutInstruction.Add(leftEdgeChromaPixels);
        mutInstruction.Add(upperLeftCornerChromaPixel);
        mutInstruction.Add(upperEdgeChromaPixels);
        mutInstruction.Add(sadAdjustment);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetMotionVectorMaskINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef skipBlockPartitionType, IdRef direction) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(skipBlockPartitionType) + buffer.GetWordLength(direction);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetMotionVectorMaskINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(skipBlockPartitionType);
        mutInstruction.Add(direction);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicConvertToMcePayloadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicConvertToMcePayloadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedShapePenalty, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedShapePenalty) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedShapePenalty);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef lumaModePenalty, IdRef lumaPackedNeighborModes, IdRef lumaPackedNonDcPenalty, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(lumaModePenalty) + buffer.GetWordLength(lumaPackedNeighborModes) + buffer.GetWordLength(lumaPackedNonDcPenalty) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(lumaModePenalty);
        mutInstruction.Add(lumaPackedNeighborModes);
        mutInstruction.Add(lumaPackedNonDcPenalty);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef chromaModeBasePenalty, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(chromaModeBasePenalty) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(chromaModeBasePenalty);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetBilinearFilterEnableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetBilinearFilterEnableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetSkcForwardTransformEnableINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packedSadCoefficients, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packedSadCoefficients) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packedSadCoefficients);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef blockBasedSkipType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(blockBasedSkipType) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(blockBasedSkipType);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicEvaluateIpeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicEvaluateIpeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicEvaluateWithSingleReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef refImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(refImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(refImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicEvaluateWithDualReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef fwdRefImage, IdRef bwdRefImage, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(fwdRefImage) + buffer.GetWordLength(bwdRefImage) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicEvaluateWithDualReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(fwdRefImage);
        mutInstruction.Add(bwdRefImage);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicEvaluateWithMultiReferenceINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef packedReferenceIds, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(packedReferenceIds) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(packedReferenceIds);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef srcImage, IdRef packedReferenceIds, IdRef packedReferenceFieldPolarities, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(srcImage) + buffer.GetWordLength(packedReferenceIds) + buffer.GetWordLength(packedReferenceFieldPolarities) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(srcImage);
        mutInstruction.Add(packedReferenceIds);
        mutInstruction.Add(packedReferenceFieldPolarities);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicConvertToMceResultINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicConvertToMceResultINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetIpeLumaShapeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetIpeLumaShapeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetBestIpeLumaDistortionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetBestIpeChromaDistortionINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetPackedIpeLumaModesINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetPackedIpeLumaModesINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetIpeChromaModeINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetIpeChromaModeINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSubgroupAvcSicGetInterRawSadsINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef payload) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(payload);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSubgroupAvcSicGetInterRawSadsINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(payload);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpVariableLengthArrayINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef lenght) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(lenght);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpVariableLengthArrayINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(lenght);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSaveMemoryINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSaveMemoryINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRestoreMemoryINTEL<TBuffer>(this TBuffer buffer, IdRef ptr) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(ptr);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRestoreMemoryINTEL;
        mutInstruction.Add(ptr);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSinCosPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger fromSign, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(fromSign) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSinCosPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(fromSign);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCastINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCastINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCastFromIntINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger mout, LiteralInteger fromSign, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(mout) + buffer.GetWordLength(fromSign) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCastFromIntINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(mout);
        mutInstruction.Add(fromSign);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCastToIntINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCastToIntINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatAddINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatAddINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSubINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSubINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatMulINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatMulINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatDivINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatDivINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatGTINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatGTINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatGEINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatGEINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLTINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLTINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLEINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLEINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatEQINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatEQINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatRecipINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatRecipINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatRSqrtINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatRSqrtINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCbrtINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCbrtINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatHypotINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatHypotINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSqrtINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSqrtINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLogINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLogINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLog2INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLog2INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLog10INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLog10INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatLog1pINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatLog1pINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatExpINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatExpINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatExp2INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatExp2INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatExp10INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatExp10INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatExpm1INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatExpm1INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSinINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSinINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSinCosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSinCosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatSinPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatSinPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatCosPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatCosPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatASinINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatASinINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatASinPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatASinPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatACosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatACosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatACosPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatACosPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatATanINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatATanINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatATanPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatATanPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatATan2INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatATan2INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatPowINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatPowINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatPowRINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger m2, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(m2) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatPowRINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(m2);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpArbitraryFloatPowNINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, LiteralInteger m1, IdRef b, LiteralInteger mout, LiteralInteger enableSubnormals, LiteralInteger roundingMode, LiteralInteger roundingAccuracy) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(a) + buffer.GetWordLength(m1) + buffer.GetWordLength(b) + buffer.GetWordLength(mout) + buffer.GetWordLength(enableSubnormals) + buffer.GetWordLength(roundingMode) + buffer.GetWordLength(roundingAccuracy);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpArbitraryFloatPowNINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(a);
        mutInstruction.Add(m1);
        mutInstruction.Add(b);
        mutInstruction.Add(mout);
        mutInstruction.Add(enableSubnormals);
        mutInstruction.Add(roundingMode);
        mutInstruction.Add(roundingAccuracy);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpLoopControlINTEL<TBuffer>(this TBuffer buffer, Span<LiteralInteger> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpLoopControlINTEL;
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAliasDomainDeclINTEL<TBuffer>(this TBuffer buffer, IdRef? name) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAliasDomainDeclINTEL;
        mutInstruction.Add(resultId);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAliasScopeDeclINTEL<TBuffer>(this TBuffer buffer, IdRef aliasDomain, IdRef? name) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(aliasDomain) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAliasScopeDeclINTEL;
        mutInstruction.Add(resultId);
        mutInstruction.Add(aliasDomain);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAliasScopeListDeclINTEL<TBuffer>(this TBuffer buffer, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAliasScopeListDeclINTEL;
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedSqrtINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedSqrtINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedRecipINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedRecipINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedRsqrtINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedRsqrtINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedSinINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedSinINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedCosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedCosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedSinCosINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedSinCosINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedSinPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedSinPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedCosPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedCosPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedSinCosPiINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedSinCosPiINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedLogINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedLogINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFixedExpINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef inputType, IdRef input, LiteralInteger s, LiteralInteger i, LiteralInteger rI, LiteralInteger q, LiteralInteger o) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(inputType) + buffer.GetWordLength(input) + buffer.GetWordLength(s) + buffer.GetWordLength(i) + buffer.GetWordLength(rI) + buffer.GetWordLength(q) + buffer.GetWordLength(o);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFixedExpINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(inputType);
        mutInstruction.Add(input);
        mutInstruction.Add(s);
        mutInstruction.Add(i);
        mutInstruction.Add(rI);
        mutInstruction.Add(q);
        mutInstruction.Add(o);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpPtrCastToCrossWorkgroupINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpPtrCastToCrossWorkgroupINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCrossWorkgroupCastToPtrINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCrossWorkgroupCastToPtrINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpReadPipeBlockingINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpReadPipeBlockingINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpWritePipeBlockingINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef packetSize, IdRef packetAlignment) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(packetSize) + buffer.GetWordLength(packetAlignment);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpWritePipeBlockingINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(packetSize);
        mutInstruction.Add(packetAlignment);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpFPGARegINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef result, IdRef input) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(result) + buffer.GetWordLength(input);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpFPGARegINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(result);
        mutInstruction.Add(input);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetRayTMinKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetRayTMinKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetRayFlagsKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetRayFlagsKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionTKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionTKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionInstanceCustomIndexKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionInstanceCustomIndexKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionInstanceIdKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionInstanceIdKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionGeometryIndexKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionGeometryIndexKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionPrimitiveIndexKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionPrimitiveIndexKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionBarycentricsKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionBarycentricsKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionFrontFaceKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionFrontFaceKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionCandidateAABBOpaqueKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionCandidateAABBOpaqueKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionObjectRayDirectionKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionObjectRayDirectionKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionObjectRayOriginKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionObjectRayOriginKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetWorldRayDirectionKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetWorldRayDirectionKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetWorldRayOriginKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetWorldRayOriginKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionObjectToWorldKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionObjectToWorldKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpRayQueryGetIntersectionWorldToObjectKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef rayQuery, IdRef intersection) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(rayQuery) + buffer.GetWordLength(intersection);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpRayQueryGetIntersectionWorldToObjectKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(rayQuery);
        mutInstruction.Add(intersection);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpAtomicFAddEXT<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef pointer, IdScope memory, IdMemorySemantics semantics, IdRef value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(pointer) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics) + buffer.GetWordLength(value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpAtomicFAddEXT;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(pointer);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        mutInstruction.Add(value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeBufferSurfaceINTEL<TBuffer>(this TBuffer buffer, AccessQualifier accessQualifier) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(accessQualifier);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeBufferSurfaceINTEL;
        mutInstruction.Add(resultId);
        mutInstruction.Add(accessQualifier);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpTypeStructContinuedINTEL<TBuffer>(this TBuffer buffer, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpTypeStructContinuedINTEL;
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConstantCompositeContinuedINTEL<TBuffer>(this TBuffer buffer, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConstantCompositeContinuedINTEL;
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSpecConstantCompositeContinuedINTEL<TBuffer>(this TBuffer buffer, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSpecConstantCompositeContinuedINTEL;
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpCompositeConstructContinuedINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, Span<IdRef> values) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(values);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpCompositeConstructContinuedINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(values);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertFToBF16INTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef floatValue) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(floatValue);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertFToBF16INTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(floatValue);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpConvertBF16ToFINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef bFloat16Value) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(bFloat16Value);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpConvertBF16ToFINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(bFloat16Value);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpControlBarrierArriveINTEL<TBuffer>(this TBuffer buffer, IdScope execution, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpControlBarrierArriveINTEL;
        mutInstruction.Add(execution);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpControlBarrierWaitINTEL<TBuffer>(this TBuffer buffer, IdScope execution, IdScope memory, IdMemorySemantics semantics) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(execution) + buffer.GetWordLength(memory) + buffer.GetWordLength(semantics);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpControlBarrierWaitINTEL;
        mutInstruction.Add(execution);
        mutInstruction.Add(memory);
        mutInstruction.Add(semantics);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupIMulKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupIMulKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupFMulKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupFMulKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupBitwiseAndKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupBitwiseAndKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupBitwiseOrKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupBitwiseOrKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupBitwiseXorKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupBitwiseXorKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupLogicalAndKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupLogicalAndKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupLogicalOrKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupLogicalOrKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpGroupLogicalXorKHR<TBuffer>(this TBuffer buffer, IdResultType resultType, IdScope execution, GroupOperation operation, IdRef x) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(execution) + buffer.GetWordLength(operation) + buffer.GetWordLength(x);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpGroupLogicalXorKHR;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(execution);
        mutInstruction.Add(operation);
        mutInstruction.Add(x);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMaskedGatherINTEL<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef ptrVector, LiteralInteger alignment, IdRef mask, IdRef fillEmpty) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(ptrVector) + buffer.GetWordLength(alignment) + buffer.GetWordLength(mask) + buffer.GetWordLength(fillEmpty);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMaskedGatherINTEL;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(ptrVector);
        mutInstruction.Add(alignment);
        mutInstruction.Add(mask);
        mutInstruction.Add(fillEmpty);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpMaskedScatterINTEL<TBuffer>(this TBuffer buffer, IdRef inputVector, IdRef ptrVector, LiteralInteger alignment, IdRef mask) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(inputVector) + buffer.GetWordLength(ptrVector) + buffer.GetWordLength(alignment) + buffer.GetWordLength(mask);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpMaskedScatterINTEL;
        mutInstruction.Add(inputVector);
        mutInstruction.Add(ptrVector);
        mutInstruction.Add(alignment);
        mutInstruction.Add(mask);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLMixinName<TBuffer>(this TBuffer buffer, LiteralString mixinName) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mixinName);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLMixinName;
        mutInstruction.Add(mixinName);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLMixinEnd<TBuffer>(this TBuffer buffer) where TBuffer : IMutSpirvBuffer
    {
        var mutInstruction = new MutRefInstruction(stackalloc int[1]);
        mutInstruction.OpCode = SDSLOp.OpSDSLMixinEnd;
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLMixinOffset<TBuffer>(this TBuffer buffer, LiteralInteger mixinName) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mixinName);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLMixinOffset;
        mutInstruction.Add(mixinName);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLMixinInherit<TBuffer>(this TBuffer buffer, LiteralString mixinName) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mixinName);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLMixinInherit;
        mutInstruction.Add(mixinName);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLCompose<TBuffer>(this TBuffer buffer, LiteralString mixin, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(mixin) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLCompose;
        mutInstruction.Add(mixin);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLStage<TBuffer>(this TBuffer buffer, IdRef stagedElement) where TBuffer : IMutSpirvBuffer
    {
        var wordLength = 1 + buffer.GetWordLength(stagedElement);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLStage;
        mutInstruction.Add(stagedElement);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLImportFunction<TBuffer>(this TBuffer buffer, LiteralString functionName, LiteralString mixinName, LiteralInteger id, LiteralInteger typeId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(functionName) + buffer.GetWordLength(mixinName) + buffer.GetWordLength(id) + buffer.GetWordLength(typeId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLImportFunction;
        mutInstruction.Add(resultId);
        mutInstruction.Add(functionName);
        mutInstruction.Add(mixinName);
        mutInstruction.Add(id);
        mutInstruction.Add(typeId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLImportVariable<TBuffer>(this TBuffer buffer, LiteralString variableName, LiteralString mixinName, LiteralInteger id) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(variableName) + buffer.GetWordLength(mixinName) + buffer.GetWordLength(id);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLImportVariable;
        mutInstruction.Add(resultId);
        mutInstruction.Add(variableName);
        mutInstruction.Add(mixinName);
        mutInstruction.Add(id);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLImportIdRef<TBuffer>(this TBuffer buffer, LiteralString mixinName, LiteralInteger id) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(mixinName) + buffer.GetWordLength(id);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLImportIdRef;
        mutInstruction.Add(resultId);
        mutInstruction.Add(mixinName);
        mutInstruction.Add(id);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLMixinVariable<TBuffer>(this TBuffer buffer, IdRef mixinId, IdRef variableId) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultId) + buffer.GetWordLength(mixinId) + buffer.GetWordLength(variableId);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLMixinVariable;
        mutInstruction.Add(resultId);
        mutInstruction.Add(mixinId);
        mutInstruction.Add(variableId);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLVariable<TBuffer>(this TBuffer buffer, IdResultType resultType, StorageClass storageclass, LiteralString name, IdRef? initializer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(storageclass) + buffer.GetWordLength(name) + buffer.GetWordLength(initializer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLVariable;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(storageclass);
        mutInstruction.Add(name);
        mutInstruction.Add(initializer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLFunctionParameter<TBuffer>(this TBuffer buffer, IdResultType resultType, LiteralString name) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(name);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLFunctionParameter;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(name);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLIOVariable<TBuffer>(this TBuffer buffer, IdResultType resultType, StorageClass storageclass, ExecutionModel executionmodel, LiteralString name, LiteralString semantic, IdRef? initializer) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(storageclass) + buffer.GetWordLength(executionmodel) + buffer.GetWordLength(name) + buffer.GetWordLength(semantic) + buffer.GetWordLength(initializer);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLIOVariable;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(storageclass);
        mutInstruction.Add(executionmodel);
        mutInstruction.Add(name);
        mutInstruction.Add(semantic);
        mutInstruction.Add(initializer);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddOpSDSLFunction<TBuffer>(this TBuffer buffer, IdResultType resultType, FunctionControlMask functioncontrol, IdRef functionType, LiteralString functionName) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        var wordLength = 1 + buffer.GetWordLength(resultType) + buffer.GetWordLength(resultId) + buffer.GetWordLength(functioncontrol) + buffer.GetWordLength(functionType) + buffer.GetWordLength(functionName);
        var mutInstruction = new MutRefInstruction(stackalloc int[wordLength]);
        mutInstruction.OpCode = SDSLOp.OpSDSLFunction;
        mutInstruction.Add(resultType);
        mutInstruction.Add(resultId);
        mutInstruction.Add(functioncontrol);
        mutInstruction.Add(functionType);
        mutInstruction.Add(functionName);
        return buffer.Add(mutInstruction);
    }
    public static Instruction AddGLSLRound<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 1, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 1, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLRoundEven<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 2, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 2, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLTrunc<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 3, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 3, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFAbs<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 4, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 4, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSAbs<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 5, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 5, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFSign<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 6, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 6, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSSign<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 7, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 7, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFloor<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 8, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 8, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLCeil<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 9, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 9, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 10, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 10, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLRadians<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef degrees, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{degrees};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 11, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 11, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLDegrees<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef radians, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{radians};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 12, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 12, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 13, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 13, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLCos<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 14, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 14, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLTan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 15, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 15, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAsin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 16, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 16, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAcos<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 17, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 17, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAtan<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef y_over_x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{y_over_x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 18, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 18, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSinh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 19, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 19, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLCosh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 20, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 20, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLTanh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 21, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 21, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAsinh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 22, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 22, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAcosh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 23, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 23, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAtanh<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 24, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 24, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLAtan2<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef y, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{y, x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 25, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 25, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPow<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 26, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 26, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLExp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 27, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 27, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLLog<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 28, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 28, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLExp2<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 29, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 29, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLLog2<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 30, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 30, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSqrt<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 31, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 31, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLInverseSqrt<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 32, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 32, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLDeterminant<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 33, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 33, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLMatrixInverse<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 34, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 34, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLModf<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef i, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, i};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 35, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 35, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLModfStruct<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 36, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 36, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 37, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 37, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 38, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 38, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 39, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 39, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 40, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 40, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 41, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 41, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 42, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 42, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFClamp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef minVal, IdRef maxVal, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, minVal, maxVal};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 43, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 43, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUClamp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef minVal, IdRef maxVal, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, minVal, maxVal};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 44, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 44, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSClamp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef minVal, IdRef maxVal, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, minVal, maxVal};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 45, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 45, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFMix<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, IdRef a, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y, a};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 46, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 46, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLIMix<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, IdRef a, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y, a};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 47, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 47, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLStep<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef edge, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{edge, x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 48, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 48, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLSmoothStep<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef edge0, IdRef edge1, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{edge0, edge1, x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 49, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 49, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFma<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef a, IdRef b, IdRef c, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{a, b, c};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 50, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 50, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFrexp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef exp, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, exp};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 51, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 51, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFrexpStruct<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 52, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 52, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLLdexp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef exp, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, exp};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 53, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 53, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackSnorm4x8<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 54, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 54, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackUnorm4x8<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 55, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 55, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackSnorm2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 56, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 56, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackUnorm2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 57, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 57, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackHalf2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 58, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 58, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLPackDouble2x32<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 59, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 59, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackSnorm2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{p};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 60, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 60, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackUnorm2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{p};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 61, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 61, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackHalf2x16<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 62, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 62, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackSnorm4x8<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{p};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 63, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 63, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackUnorm4x8<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{p};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 64, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 64, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLUnpackDouble2x32<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef v, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{v};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 65, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 65, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLLength<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 66, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 66, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLDistance<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef p0, IdRef p1, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{p0, p1};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 67, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 67, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLCross<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 68, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 68, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLNormalize<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 69, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 69, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFaceForward<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef n, IdRef i, IdRef nref, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{n, i, nref};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 70, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 70, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLReflect<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef i, IdRef n, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{i, n};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 71, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 71, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLRefract<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef i, IdRef n, IdRef eta, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{i, n, eta};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 72, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 72, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFindILsb<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{value};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 73, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 73, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFindSMsb<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{value};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 74, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 74, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLFindUMsb<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef value, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{value};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 75, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 75, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLInterpolateAtCentroid<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef interpolant, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{interpolant};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 76, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 76, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLInterpolateAtSample<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef interpolant, IdRef sample, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{interpolant, sample};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 77, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 77, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLInterpolateAtOffset<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef interpolant, IdRef offset, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{interpolant, offset};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 78, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 78, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLNMin<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 79, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 79, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLNMax<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef y, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, y};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 80, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 80, resultId, resultType , refs);
        else return Instruction.Empty;
    }
    public static Instruction AddGLSLNClamp<TBuffer>(this TBuffer buffer, IdResultType resultType, IdRef x, IdRef minVal, IdRef maxVal, int set) where TBuffer : IMutSpirvBuffer
    {
        var resultId = buffer.GetNextId();
        Span<IdRef> refs = stackalloc IdRef[]{x, minVal, maxVal};
        if(buffer is MultiBuffer mb)
            return mb.AddOpExtInst(set, 81, resultId, resultType , refs);
        else if (buffer is WordBuffer wb)
            return wb.AddOpExtInst(set, 81, resultId, resultType , refs);
        else return Instruction.Empty;
    }
}
