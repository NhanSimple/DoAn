using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;

namespace XChess.Repository.MatchResultRepository
{
    public interface IMatchResultRepository : IGenericRepository<MatchResult>
    {
        bool TryGetResult(long matchId, long userId, out MatchResult result);

        IEnumerable<MatchResult> GetResultsByMatchId(long matchId);

        bool HasResult(long matchId);
    }
}
