using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.Infrastructure.CountdownTimer
{
    public class CountdownTimer:ICountdownTimer
    {
        private TimeSpan _remaining;
        private Timer _timer;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(1);
        public bool IsRunning { get; private set; }
        public TimeSpan Remaining => _remaining;

        public event Action<TimeSpan> Tick;
        public event Action TimeExpired;

        public CountdownTimer(TimeSpan initial)
        {
            _remaining = initial;
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _timer = new Timer(_ =>
            {
                _remaining -= _interval;
                Tick?.Invoke(_remaining);

                if (_remaining <= TimeSpan.Zero)
                {
                    Stop();
                    TimeExpired?.Invoke();
                }

            }, null, TimeSpan.Zero, _interval);
        }

        public void Pause()
        {
            _timer?.Dispose();
            IsRunning = false;
        }

        public void Reset(TimeSpan newTime)
        {
            Pause();
            _remaining = newTime;
        }

        public void Stop()
        {
            _timer?.Dispose();
            IsRunning = false;
        }
    }
}
