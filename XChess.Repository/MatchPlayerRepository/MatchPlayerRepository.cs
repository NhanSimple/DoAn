using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;

namespace XChess.Repository.MatchPlayerRepository
{
    public class MatchPlayerRepository : GenericRepository<MatchPlayer>, IMatchPlayerRepository
    {
        public MatchPlayerRepository(DbContext context) : base(context) { }
        public bool TryGetPlayerColor(long matchId, long userId, out PlayerColor color)
        {
            var player = GetAll().FirstOrDefault(p => p.MatchId == matchId && p.UserId == userId);

            if (player != null)
            {
                color = player.PlayerColor;
                return true;
            }

            color = default;
            return false;
        }

        public bool Exists(long matchId, long userId)
        {
            return Any(p => p.MatchId == matchId && p.UserId == userId);
        }

        public bool TryGetPlayersByMatchId(long matchId, out List<MatchPlayer> players)
        {
            players = _entities.Set<MatchPlayer>()
                               .Where(p => p.MatchId == matchId)
                               .ToList();

            return players.Count > 0;
        }
    }
}
