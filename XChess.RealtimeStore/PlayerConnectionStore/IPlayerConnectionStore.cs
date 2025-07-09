using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Store.Common;
namespace XChess.Store.PlayerConnectionStore
{
    public interface IPlayerConnectionStore:IGenericStore<PlayerConnection>
    {

        bool TryGetByUserId(long userId, out PlayerConnection connection);
        bool RemoveByUserId(long userId);

    }
}
