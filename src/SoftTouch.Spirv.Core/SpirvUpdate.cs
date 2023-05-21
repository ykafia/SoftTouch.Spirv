using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.Spirv.Core;

public struct SpirvUpdate
{
    public UpdateKind Kind { get; set; }
    public int DataIndex { get; set; }
    public int TargetIndex { get; set; }

    SpirvUpdate(int data, int target = -1)
    {
        Kind = target > -1 ? UpdateKind.Replace : UpdateKind.Remove;
        DataIndex = data;
        TargetIndex = target;
    }

    public static SpirvUpdate Replace(int dataIndex, int targetIndex)
    {
        return new(dataIndex,targetIndex);
    } 
    public static SpirvUpdate Remove(int dataIndex)
    {
        return new(dataIndex);
    } 
}
