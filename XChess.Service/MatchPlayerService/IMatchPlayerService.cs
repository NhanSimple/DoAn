using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Model.Enums;
using XChess.Service.ChessMatchService.Dto;
using XChess.Service.Common;
using XChess.Service.MatchPlayerService.Dto;

namespace XChess.Service.MatchPlayerService
{
    public interface IMatchPlayerService:IEntityService<MatchPlayer>
    {
        ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime);

        bool TryGetOngoingMatch(long userId, out ChessMatchDto match);

        IEnumerable<ChessMatchDto> GetMatchHistory(long userId);

        long StartWithAI(int level, PlayerColor playerColor, long userId);

        void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note);

        Task<string> ComputeAIMoveAsync(long matchId, string fen);

        Task<string> ApplyMoveAsync(long matchId, string fen, string move);
    }
}
