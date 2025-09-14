using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;

namespace XChess.Store.EngineSessionStore
{
    public class EngineSessionStore : GenericStore<EngineSession>, IEngineSessionStore
    {
        public EngineSessionStore(IRealtimeContext context) : base(context)
        {
        }

        public EngineSession Get(long matchId)
        {
            var id = matchId.ToString();
            if (TryGet(id, out var session))
                return session;
            return null;
        }

        public bool Exists(long matchId)
        {
            var id = matchId.ToString();
            return Contains(id);
        }

        public bool TryGet(long matchId, out EngineSession session)
        {
            var id = matchId.ToString();
            return TryGet(id, out session);
        }

        public void Add(EngineSession session)
        {
            var id = session.MatchId.ToString();
            if (!TryAdd(id, session))
                throw new InvalidOperationException($"Session for match {id} already exists.");
        }

        public void RemoveAndStop(long matchId)
        {
            string id = matchId.ToString();

            if (TryGet(id, out var session))
            {
                session.Engine?.Stop();
                session.Engine?.Dispose();
            }
            TryRemove(id);

        }
    }
}
