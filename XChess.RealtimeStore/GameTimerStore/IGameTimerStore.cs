using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Model;
using XChess.Store.Common;

namespace XChess.Store.GameTimerStore
{
    public interface IGameTimerStore:IGenericStore<GameTimer>
    {
        (TimeSpan? white, TimeSpan? black) GetTimeLeft(long matchId);
        void UpdateTime(long matchId, bool isWhite, TimeSpan timeLeft);
        void CreateTimer(long matchId, TimeSpan totalTime);
        void RemoveTimer(long matchId);
    }
}
