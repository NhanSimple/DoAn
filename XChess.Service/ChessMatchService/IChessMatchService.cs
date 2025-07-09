using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Model.Enums;
using XChess.Service.Common;
using XChess.Service.ChessMatchService.Dto;
namespace XChess.Service.ChessMatchService
{
    public interface IChessMatchService:IEntityService<ChessMatch>
    {
        ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime);

        bool TryGetOngoingMatch(long userId, out ChessMatchDto match);

        IEnumerable<ChessMatchDto> GetMatchHistory(long userId);

        void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note);
    }
}
