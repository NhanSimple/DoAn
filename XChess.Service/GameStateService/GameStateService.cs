using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Service.Common;
using XChess.Store.GameStateStore;
namespace XChess.Service.GameStateService
{
    public class GameStateService :GenericStoreService<GameState>, IGameStateService
    {
        private readonly IGameStateStore _GameStateStore;
        public GameStateService(IGameStateStore gameStateStore) : base(gameStateStore)
        {
            _GameStateStore = gameStateStore;
        }
        public bool TryGet(string id, out GameState entity)
        {
            // Dùng method TryGet của store
            return _GameStateStore.TryGet(id, out entity);
        }

    }
}
