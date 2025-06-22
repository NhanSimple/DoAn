using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public class GameSessionManager: IGameSessionManager
    {
        private readonly ConcurrentDictionary<string, List<string>> _sessions = new ConcurrentDictionary<string, List<string>>();

        public void CreateSession(string sessionId)
        {
            _sessions.TryAdd(sessionId, new List<string>());
        }

        public void JoinSession(string sessionId, string playerId)
        {
            if (_sessions.TryGetValue(sessionId, out var list))
            {
                list.Add(playerId);
            }
        }
    }
}
