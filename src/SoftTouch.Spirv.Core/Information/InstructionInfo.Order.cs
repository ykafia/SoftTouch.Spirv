using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Spv.Specification;

namespace SoftTouch.Spirv.Core;



public partial class InstructionInfo
{
    public Dictionary<(Op, StorageClass?), int> OrderGroup = new();

    void InitOrder()
    {
        OrderGroup[(Op.OpCapability, null)] = 0;
        OrderGroup[(Op.OpExtension, null)] = 1;
        OrderGroup[(Op.OpExtInstImport, null)] = 2;
        OrderGroup[(Op.OpMemoryModel, null)] = 3;
        OrderGroup[(Op.OpEntryPoint, null)] = 4;
        
        foreach(var e in Enum.GetValues<Op>().Where(x => x.ToString().StartsWith("OpExecutionMode")))
            OrderGroup[(e,null)] = 5;
        
        foreach(var e in new Op[]{Op.OpString, Op.OpSource, Op.OpSourceExtension, Op.OpSourceContinued})
            OrderGroup[(e,null)] = 6;

        OrderGroup[(Op.OpName, null)] = 7;
        OrderGroup[(Op.OpMemberName, null)] = 7;

        OrderGroup[(Op.OpModuleProcessed, null)] = 8;

        foreach(var e in Enum.GetValues<Op>().Where(x => x.ToString().StartsWith("OpDecorate")))
            OrderGroup[(e,null)] = 9;
        
        foreach(var e in Enum.GetValues<Op>().Where(x => x.ToString().StartsWith("OpType") || x.ToString().StartsWith("OpConstant") || x.ToString().StartsWith("OpSpec")))
            OrderGroup[(e,null)] = 10;
        
        foreach(var e in Enum.GetValues<StorageClass>().Where(x => x != StorageClass.Function))
            OrderGroup[(Op.OpVariable,e)] = 10;
        
        OrderGroup[(Op.OpUndef, null)] = 10;
        OrderGroup[(Op.OpLine, null)] = 11;
        OrderGroup[(Op.OpNoLine, null)] = 11;
        OrderGroup[(Op.OpExtInst, null)] = 12;

        foreach(var e in Enum.GetValues<Op>().Except(OrderGroup.Keys.Select(x => x.Item1)))
            OrderGroup[(e,null)] = 13;
    }

}
