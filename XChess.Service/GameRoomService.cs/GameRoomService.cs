using System;
using System.Collections.Concurrent;
using XChess.Model.RealTimeModel;
using XChess.Repository.Common;
using XChess.Service.Common;
using XChess.Service.GameTimerService;
using XChess.Store;
using XChess.Store.GameRoomStore;
namespace XChess.Service.GameRoomService.cs
{
    public class GameRoomService : GenericStoreService<GameRoom>, IGameRoomService
    {
      private readonly  IGameRoomStore _GameRoomStore;
       
        public GameRoomService(IGameRoomStore gameRoomStore) 
            : base(gameRoomStore)
        {
            _GameRoomStore= gameRoomStore;
        }

        public bool Exists(string id)
        {
            throw new System.NotImplementedException();
        }

        public GameRoom JoinOrAddRoom(PlayerConnection conn)
        {
            if (_GameRoomStore == null)
            {
                throw new InvalidOperationException("lỗi");
            }
            if (_GameRoomStore.TryGetRoomByUserId(conn.UserId, out var existingRoom))
            {
                return existingRoom;
            }

            var check = _GameRoomStore.TryFindWaitingRoom(out GameRoom room);
            if (check)
            {
                room.GuestId = conn.UserId;
            }
            else
            {
                room = new GameRoom
                {
                    Id = Guid.NewGuid().ToString(),
                    GuestId = conn.UserId,

                };
                _GameRoomStore.TryAdd(room.Id, room);
                room.GuestId = conn.UserId;
            }
            return room;
        }

        public bool TryGetByPlayerId(string playerId, out GameRoom room)
        {
            throw new System.NotImplementedException();
        }
    }
}