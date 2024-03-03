using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanchime.Common.Snowflakes;

internal class OverCostActionArgs(ushort workerId, long timeTick, int actionType = 0, int overCostCountInOneTerm = 0, int genCountWhenOverCost = 0, int index = 0)
{
    /// <summary>
    /// 事件类型
    /// 1-开始，2-结束，8-漂移
    /// </summary>
    public virtual int ActionType { get; set; } = actionType;
    /// <summary>
    /// 时间戳
    /// </summary>
    public virtual long TimeTick { get; set; } = timeTick;
    /// <summary>
    /// 机器码
    /// </summary>
    public virtual ushort WorkerId { get; set; } = workerId;
    /// <summary>
    /// 漂移计算次数
    /// </summary>
    public virtual int OverCostCountInOneTerm { get; set; } = overCostCountInOneTerm;
    /// <summary>
    /// 漂移期间生产ID个数
    /// </summary>
    public virtual int GenCountInOneTerm { get; set; } = genCountWhenOverCost;
    /// <summary>
    /// 漂移周期
    /// </summary>
    public virtual int TermIndex { get; set; } = index;
}
