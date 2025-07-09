using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Model.Entities;

namespace XChess.Repository.ChessMatchRepository
{
    public interface IChessMatchRepository:IGenericRepository<ChessMatch>
    {
        bool TryGetOngoingMatchByUserId(long userId, out ChessMatch match);

        IEnumerable<ChessMatch> GetMatchHistoryByUserId(long userId, int skip = 0, int take = 20);

        bool IsUserInMatch(long matchId, long userId);

    }
}
