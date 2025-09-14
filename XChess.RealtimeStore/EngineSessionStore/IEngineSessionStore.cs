using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;

namespace XChess.Store.EngineSessionStore
{
    public interface IEngineSessionStore : IGenericStore<EngineSession>
    {
        EngineSession Get(long matchId);
        bool Exists(long matchId);
        bool TryGet(long matchId, out EngineSession session);
        void Add(EngineSession session);
        void RemoveAndStop(long matchId);
    }
}
