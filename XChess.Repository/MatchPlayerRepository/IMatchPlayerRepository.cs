using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;
namespace XChess.Repository.MatchPlayerRepository
{
    public interface IMatchPlayerRepository : IGenericRepository<MatchPlayer>
    {
        bool TryGetPlayerColor(long matchId, long userId, out PlayerColor color);

        bool Exists(long matchId, long userId);
        bool TryGetPlayersByMatchId(long matchId, out List<MatchPlayer> players);
    }
}
