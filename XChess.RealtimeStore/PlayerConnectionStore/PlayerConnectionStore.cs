using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;
namespace XChess.Store.PlayerConnectionStore
{
    public class PlayerConnectionStore : GenericStore<PlayerConnection>, IPlayerConnectionStore
    {
        public PlayerConnectionStore(IRealtimeContext context) : base(context)
        {
        }

        public bool TryGetByUserId(long userId, out PlayerConnection connection)
        {
            connection = GetAll().FirstOrDefault(x => x.UserId == userId);
            return connection != null;            
        }

        public bool RemoveByUserId(long userId)
        {
            PlayerConnection conn;
            if (!TryGetByUserId(userId, out conn))
                return false;

            return TryRemove(GetConnectionKey(conn));
        }
        public static string GetConnectionKey(PlayerConnection conn)
        {
            // Ưu tiên dùng socket hash nếu có, fallback dùng UserId
            return conn.Socket?.GetHashCode().ToString() ?? conn.UserId.ToString();
        }
    }
}
