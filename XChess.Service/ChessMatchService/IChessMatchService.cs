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
    public interface IChessMatchService : IEntityService<ChessMatch>
    {
        ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime);

        bool TryGetOngoingMatch(long userId, out ChessMatchDto match);

        IEnumerable<ChessMatchDto> GetMatchHistory(long userId);

        long StartWithAI(int level, PlayerColor playerColor, long userId);

        void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note);

        /// <summary>
        /// Tính nước đi tốt nhất của AI theo trạng thái FEN hiện tại (async)
        /// </summary>
        Task<string> ComputeAIMoveAsync(long matchId, string fen);

        /// <summary>
        /// Áp dụng nước đi vào trạng thái trận đấu, trả về true nếu thành công (async)
        /// </summary>
        Task<bool> ApplyMoveAsync(long matchId, string fen, string move);
    }
}
