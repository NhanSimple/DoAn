using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;

namespace XChess.Repository.MatchResultRepository
{
    public class MatchResultRepository : GenericRepository<MatchResult>, IMatchResultRepository
    {
        public MatchResultRepository(DbContext context) : base(context) { }

        public bool TryGetResult(long matchId, long userId, out MatchResult result)
        {
            var r = _entities.Set<MatchResult>().FirstOrDefault(x => x.MatchId == matchId && x.UserId == userId);
            result = r;
            return result != null;
        }

        public IEnumerable<MatchResult> GetResultsByMatchId(long matchId)
        {
            return _entities.Set<MatchResult>().Where(r => r.MatchId == matchId).ToList();
        }

        public bool HasResult(long matchId)
        {
            return _entities.Set<MatchResult>().Any(r => r.MatchId == matchId);
        }
    }
}
