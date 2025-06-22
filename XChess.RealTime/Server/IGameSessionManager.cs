using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public interface IGameSessionManager
    {
        void CreateSession(string sessionId);
        void JoinSession(string sessionId, string playerId);
    }
}
