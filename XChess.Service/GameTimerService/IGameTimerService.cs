using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.GameModel;

namespace XChess.Service.GameTimerService
{
    public interface IGameTimerService
    {
        void CreateGame(string matchId, TimeSpan initialTime);
        void SwitchTurn(string matchId);
        void Pause(string matchId);
        void Resume(string matchId);
        TimeSpan GetTimeLeft(string matchId, PlayerColor color);
        GameTimerState GetState(string matchId);

    }
}
