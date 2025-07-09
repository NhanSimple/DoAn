using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Model.Entities;
namespace XChess.Repository.MoveRepository
{
    public interface IMoveRepository:IGenericRepository<Move>
    {
        IEnumerable<Move> GetMovesByMatchId(long matchId);

        Move GetLastMove(long matchId);

        bool TryGetLastMove(long matchId, out Move move);

        int CountMoves(long matchId);
    }
}
