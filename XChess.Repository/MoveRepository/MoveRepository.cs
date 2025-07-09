using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Model.Entities;
using System.Data.Entity;

namespace XChess.Repository.MoveRepository
{
    public class MoveRepository:GenericRepository<Move>, IMoveRepository
    {
        public MoveRepository(DbContext context) : base(context) { }

        public IEnumerable<Move> GetMovesByMatchId(long matchId)
        {
            return _entities.Set<Move>()
                .Where(m => m.MatchId == matchId)
                .OrderBy(m => m.MoveNumber)
                .ToList();
        }

        public Move GetLastMove(long matchId)
        {
            return _entities.Set<Move>()
                .Where(m => m.MatchId == matchId)
                .OrderByDescending(m => m.MoveNumber)
                .First();
        }

        public bool TryGetLastMove(long matchId, out Move move)
        {
            var found = _entities.Set<Move>()
                .Where(m => m.MatchId == matchId)
                .OrderByDescending(m => m.MoveNumber)
                .FirstOrDefault();

            if (found != null)
            {
                move = found;
                return true;
            }

            move = null;
            return false;
        }

        public int CountMoves(long matchId)
        {
            return _entities.Set<Move>()
                .Count(m => m.MatchId == matchId);
        }
    }
}
