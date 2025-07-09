using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Model.Enums;
using XChess.Store.Common;
namespace XChess.Store.GameRoomStore
{
    public  interface IGameRoomStore:IGenericStore<GameRoom>
    {
        bool TryFindWaitingRoom(out GameRoom room);
        bool TryGetRoomByUserId(long userId, out GameRoom room);
        IEnumerable<GameRoom> GetWaitingRooms();
        IEnumerable<GameRoom> GetRoomsIsFull();
    }
}
    