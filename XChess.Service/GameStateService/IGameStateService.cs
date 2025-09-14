using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Service.Common;
using XChess.Model.RealTimeModel;
namespace XChess.Service.GameStateService
{
    public interface IGameStateService: IGenericStoreService<GameState>
    {
        bool TryGet(string id, out GameState entity);
    }
}
