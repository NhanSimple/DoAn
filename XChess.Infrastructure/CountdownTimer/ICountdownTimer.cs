using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.CountdownTimer
{
    public interface ICountdownTimer
    {
        bool IsRunning { get; }
        TimeSpan Remaining { get; }

        event Action<TimeSpan> Tick;
        event Action TimeExpired;

        void Start();
        void Pause();
        void Stop();
        void Reset(TimeSpan newTime);
    }
}
