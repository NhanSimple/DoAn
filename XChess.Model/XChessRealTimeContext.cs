using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Infrastructure.Realtime;



namespace XChess.Model
{
    public class XChessRealTimeContext:RealTimeContext
    {
        public XChessRealTimeContext() : base("XChessRealtimeContext") { }
        public IRealtimeSet<GameRoom> GameRooms { get; private set; }
        public IRealtimeSet<GameTimer> GameTimers { get; private set; }
        public IRealtimeSet<PlayerConnection> PlayerConnections { get; private set; }
        //public IRealtimeSet<OnlineUser> OnlineUsers { get; private set; }
        //public IRealtimeSet<GameSession> GameSessions { get; private set; }

      
    }
}
