using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanchime.Common.Snowflakes.Internal;

internal class NormalSnowWorker(SnowflakeOptions options) : DriftSnowWorker(options)
{
    public override long NextId()
    {
        lock (SyncLocker)
        {
            long currentTimeTick = GetCurrentTimeTick();

            if (LastTimeTick == currentTimeTick)
            {
                if (CurrentSeqNumber++ > MaxSeqNumber)
                {
                    CurrentSeqNumber = MinSeqNumber;
                    currentTimeTick = GetNextTimeTick();
                }
            }
            else
            {
                CurrentSeqNumber = MinSeqNumber;
            }

            if (currentTimeTick < LastTimeTick)
            {
                throw new Exception(string.Format("Time error for {0} milliseconds", LastTimeTick - currentTimeTick));
            }

            LastTimeTick = currentTimeTick;
            return (currentTimeTick << TimestampShift) + ((long)WorkerId << SeqBitLength) + CurrentSeqNumber;
        }
    }
}
