using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Model.Enums;
using XChess.Model.RealTimeModel;
using XChess.Repository.ChessMatchRepository;
using XChess.Repository.Common;
using XChess.Repository.MatchPlayerRepository;
using XChess.Repository.MatchResultRepository;
using XChess.Service.Common;
using XChess.Service.EngineSessionService;
using XChess.Service.GameStateService;
using XChess.Service.ChessMatchService.Dto;

namespace XChess.Service.ChessMatchService
{
    public interface IMatchPlayerService : IEntityService<MatchPlayer>
    {
        ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime);

        bool TryGetOngoingMatch(long userId, out ChessMatchDto match);

        IEnumerable<ChessMatchDto> GetMatchHistory(long userId);

        long StartWithAI(int level, PlayerColor playerColor, long userId);

        void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note);

        Task<string> ComputeAIMoveAsync(long matchId, string fen);

        Task<string> ApplyMoveAsync(long matchId, string fen, string move);
    }


    public class MatchPlayerService : EntityService<MatchPlayer>, IMatchPlayerService
    {
        private readonly IChessMatchRepository _chessMatchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchPlayerRepository _matchPlayerRepository;
        private readonly IMatchResultRepository _matchResultRepository;
        private readonly IEngineSessionService _engineSessionService;
        private readonly IGameStateService _gameStateService;

        public MatchPlayerService(
            IUnitOfWork unitOfWork,
            IChessMatchRepository chessMatchRepository,
            IMatchPlayerRepository matchPlayerRepository,
            IMatchResultRepository matchResultRepository,
            IEngineSessionService engineSessionService,
            IGameStateService gameStateService
        ) : base(unitOfWork, matchPlayerRepository)
        {
            _unitOfWork = unitOfWork;
            _chessMatchRepository = chessMatchRepository;
            _matchPlayerRepository = matchPlayerRepository;
            _matchResultRepository = matchResultRepository;
            _engineSessionService = engineSessionService;
            _gameStateService = gameStateService;
        }

        public ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime)
        {
            var match = new ChessMatch
            {
                StartedAt = DateTime.UtcNow,
                GameMode = gameType,
                InitialTime = initialTime
            };

            _chessMatchRepository.Add(match);
            _unitOfWork.Commit();

            _matchPlayerRepository.Add(new MatchPlayer
            {
                MatchId = match.Id,
                UserId = whitePlayerId,
                PlayerColor = PlayerColor.White
            });

            _matchPlayerRepository.Add(new MatchPlayer
            {
                MatchId = match.Id,
                UserId = blackPlayerId,
                PlayerColor = PlayerColor.Black
            });

            _unitOfWork.Commit();

            return MapMatchToDto(match);
        }

        public bool TryGetOngoingMatch(long userId, out ChessMatchDto matchDto)
        {
            if (_chessMatchRepository.TryGetOngoingMatchByUserId(userId, out var match))
            {
                matchDto = MapMatchToDto(match);
                return true;
            }

            matchDto = null;
            return false;
        }

        public IEnumerable<ChessMatchDto> GetMatchHistory(long userId)
        {
            return _chessMatchRepository.GetMatchHistoryByUserId(userId)
                .Select(MapMatchToDto)
                .ToList();
        }

        public long StartWithAI(int level, PlayerColor playerColor, long userId)
        {
            var match = new ChessMatch
            {
                GameMode = GameType.PvE,
                StartedAt = DateTime.UtcNow,
            };

            _chessMatchRepository.Add(match);
            _unitOfWork.Commit();

            var state = new GameState
            {
                MatchId = match.Id,
                LastMoveFrom = null,
                CurrentTurn = "white",
                LastMoveTo = null,
                LastMoveBy = null,
                LastPromotion = null,

                IsCapture = false,
                CapturedSquare = null,
                IsEnPassant = false,
                EnPassantCapturedSquare = null,
                IsCastling = false,

                IsCheck = false,
                CheckToColor = null,

                IsCheckmate = false,
                IsDraw = false,
                IsGameOver = false,
                Winner = null,
                EndReason = null,
                LastMoveAt = DateTime.UtcNow,

                WhiteUserId = playerColor == PlayerColor.White ? userId : (long?)null,
                BlackUserId = playerColor == PlayerColor.Black ? userId : (long?)null
            };
            _gameStateService.TryAdd(match.Id.ToString(), state);

            _engineSessionService.Create(match.Id, level);

            return match.Id;
        }

        public void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note)
        {
            var match = _chessMatchRepository.GetById(matchId);
            match.FinishedAt = DateTime.UtcNow;
            _chessMatchRepository.Update(match);

            foreach (var kvp in resultByUserId)
            {
                _matchResultRepository.Add(new MatchResult
                {
                    MatchId = matchId,
                    UserId = kvp.Key,
                    GameResult = kvp.Value,
                    Note = note
                });
            }

            _unitOfWork.Commit();
        }

        public async Task<string> ComputeAIMoveAsync(long matchId, string fen)
        {
            if (_engineSessionService.TryGet(matchId.ToString(), out var session))
            {
                return await session.Engine.GetBestMove(fen);
            }
            return null;
        }

        public async Task<string> ApplyMoveAsync(long matchId, string fen, string move)
        {
            if (!_gameStateService.TryGet(matchId.ToString(), out var state))
                throw new InvalidOperationException($"Không tìm thấy trạng thái trận đấu với matchId {matchId}");

            bool legal = await _engineSessionService.ApplyMoveAsync(matchId, fen, move);
            if (!legal) return null;

            state.LastMoveFrom = move.Substring(0, 2);
            state.LastMoveTo = move.Substring(2, 2);
            state.LastMoveBy = state.CurrentTurn;
            state.CurrentTurn = state.CurrentTurn == "white" ? "black" : "white";
            state.LastMoveAt = DateTime.UtcNow;

            _gameStateService.Update(matchId.ToString(), state);

            // Nếu muốn có fen mới để trả về, bạn phải lấy từ engine hoặc tính riêng
            // Hiện tại return move (bạn có thể đổi lại theo ý bạn)
            return move;
        }

        private ChessMatchDto MapMatchToDto(ChessMatch m)
        {
            return new ChessMatchDto
            {
                Id = m.Id,
                StartedAt = m.StartedAt,
                FinishedAt = m.FinishedAt,
                GameMode = m.GameMode,
                InitialTime = m.InitialTime
            };
        }
    }
}
