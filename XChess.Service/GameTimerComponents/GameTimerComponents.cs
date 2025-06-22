using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.GameModel;

namespace XChess.Service.GameTimer
{
    public class GameTimerComponents : IGameTimerComponents
    {
        public GameTimerState State { get; private set; }
        public GameTimerComponents(TimeSpan initialTime)
        {
            State = new GameTimerState
            {
                InitialTime = initialTime,
                WhiteTimeLeft = initialTime,
                BlackTimeLeft = initialTime,
                CurrentTurn = PlayerColor.White,
                LastMoveTime = DateTime.UtcNow
            };
        }

        public void SwitchTurn()
        {
            if (State.IsPaused) return;

            var now = DateTime.UtcNow;
            var elapsed = now - State.LastMoveTime;

            if (State.CurrentTurn == PlayerColor.White)
                State.WhiteTimeLeft -= elapsed;
            else
                State.BlackTimeLeft -= elapsed;

            State.CurrentTurn = State.CurrentTurn == PlayerColor.White
                ? PlayerColor.Black
                : PlayerColor.White;

            State.LastMoveTime = now;
        }

        public void Pause()
        {
            if (State.IsPaused) return;

            var now = DateTime.UtcNow;
            var elapsed = now - State.LastMoveTime;

            if (State.CurrentTurn == PlayerColor.White)
                State.WhiteTimeLeft -= elapsed;
            else
                State.BlackTimeLeft -= elapsed;

            State.IsPaused = true;
        }

        public void Resume()
        {
            if (!State.IsPaused) return;

            State.LastMoveTime = DateTime.UtcNow;
            State.IsPaused = false;
        }

        public TimeSpan GetTimeLeft(PlayerColor color)
        {
            if (State.IsPaused)
            {
                return color == PlayerColor.White ? State.WhiteTimeLeft : State.BlackTimeLeft;
            }

            var elapsed = DateTime.UtcNow - State.LastMoveTime;
            return color == PlayerColor.White && State.CurrentTurn == PlayerColor.White
                ? State.WhiteTimeLeft - elapsed
                : color == PlayerColor.Black && State.CurrentTurn == PlayerColor.Black
                    ? State.BlackTimeLeft - elapsed
                    : color == PlayerColor.White
                        ? State.WhiteTimeLeft
                        : State.BlackTimeLeft;
        }
    }
}
