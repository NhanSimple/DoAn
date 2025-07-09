using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Model.Entities;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using XChess.Repository.MatchPlayerRepository;
namespace XChess.Repository.ChessMatchRepository
{
    public  class ChessMatchRepository:GenericRepository<ChessMatch>, IChessMatchRepository
    {
 
        public ChessMatchRepository(DbContext context) : base(context) 
        {
  
        }
        public bool TryGetOngoingMatchByUserId(long userId, out ChessMatch match)
        {
            var result = _entities.Set<ChessMatch>().FirstOrDefault(m =>
                m.FinishedAt == null &&
                _entities.Set<MatchPlayer>().Any(p => p.MatchId == m.Id && p.UserId == userId));

            if (result != null)
            {
                match = result;
                return true;
            }

            match = null;
            return false;
        }
        public IEnumerable<ChessMatch> GetMatchHistoryByUserId(long userId, int skip = 0, int take = 20)
        {
            return GetAll()
                .Where(m =>
                    m.FinishedAt != null &&
                    (_entities.Set<MatchPlayer>().Any(mp => mp.MatchId == m.Id && mp.UserId == userId)))
                .OrderByDescending(m => m.FinishedAt)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public bool IsUserInMatch(long matchId, long userId)
        {
            return _entities.Set<MatchPlayer>().Any(p => p.MatchId == matchId && p.UserId == userId);
        }
    }
}   
