using XChess.Model.RealTimeModel;
using XChess.Store.Common;
using XChess.Service.Common;
namespace XChess.Service.GameRoomService.cs
{
    public  interface IGameRoomService:IGenericStoreService<GameRoom>
    {
        GameRoom JoinOrAddRoom(PlayerConnection conn);
        bool TryGetByPlayerId(string playerId, out GameRoom room);
    }
}