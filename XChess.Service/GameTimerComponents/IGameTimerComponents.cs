using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.GameModel;

namespace XChess.Service.GameTimer
{
    public interface IGameTimerComponents
    {
        GameTimerState State { get; }
        void SwitchTurn();
        void Pause();
        void Resume();
        TimeSpan GetTimeLeft(PlayerColor color);
    }
}
