using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;

namespace XChess.Store.GameRoomStore
{
    public class GameRoomStore : GenericStore<GameRoom>, IGameRoomStore
    {
        public GameRoomStore(IRealtimeContext context) : base(context)
        {
        }

        public bool TryFindWaitingRoom(out GameRoom room)
        {
            room = GetAll().FirstOrDefault(r => r.IsWaiting == true);
            return room != null;
        }

        public IEnumerable<GameRoom> GetWaitingRooms()
        {
            return GetAll().Where(r => r.IsWaiting == true);
        }

        public IEnumerable<GameRoom> GetRoomsIsFull()
        {
            return GetAll().Where(r => r.IsFull == true);
        }

        public bool TryGetRoomByUserId(long userId, out GameRoom room)
        {
            room = GetAll().Where(r => r.HostId == userId || r.GuestId == userId).FirstOrDefault();
            return room != null;
        }
    }
}
