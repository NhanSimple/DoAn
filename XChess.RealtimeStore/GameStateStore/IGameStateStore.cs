using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;

namespace XChess.Store.GameStateStore
{
    public interface IGameStateStore : IGenericStore<GameState>
    {

    }
}
