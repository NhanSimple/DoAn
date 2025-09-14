using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;
using XChess.Store.GameRoomStore;

namespace XChess.Store.GameStateStore
{
    public class GameStateStore : GenericStore<GameState>, IGameStateStore
    {
        public GameStateStore(IRealtimeContext context) : base(context)
        {

        }
    }
}
