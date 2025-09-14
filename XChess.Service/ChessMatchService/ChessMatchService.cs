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
using XChess.Service.ChessMatchService;
using XChess.Service.ChessMatchService.Dto;
using XChess.Service.Common;
using XChess.Service.EngineSessionService;
using XChess.Service.GameStateService;

namespace XChess.Service.ChessMatchService{
    public class ChessMatchService : EntityService<ChessMatch>, IChessMatchService
    {
        private readonly IChessMatchRepository _ChessMatchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchPlayerRepository _matchPlayerRepository;
        private readonly IMatchResultRepository _matchResultRepository;
        private readonly IEngineSessionService _EngineSessionService;
        private readonly IGameStateService _GameStateService;

        public ChessMatchService(IUnitOfWork unitOfWork,
            IChessMatchRepository chessMatchRepository,
            IMatchPlayerRepository matchPlayerRepository,
            IMatchResultRepository matchResultRepository,
            IEngineSessionService engineSessionService,
            IGameStateService gameStateService
            ) : base(unitOfWork, chessMatchRepository)
        {
            _ChessMatchRepository = chessMatchRepository;
            _unitOfWork = unitOfWork;
            _matchPlayerRepository = matchPlayerRepository;
            _matchResultRepository = matchResultRepository;
            _EngineSessionService = engineSessionService;
            _GameStateService = gameStateService;
        }

        public ChessMatchDto CreateMatch(long whitePlayerId, long blackPlayerId, GameType gameType, TimeSpan initialTime)
        {
            var match = new ChessMatch
            {
                StartedAt = DateTime.UtcNow,
                GameMode = gameType,
                InitialTime = initialTime
            };

            _ChessMatchRepository.Add(match);
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
            if (_ChessMatchRepository.TryGetOngoingMatchByUserId(userId, out ChessMatch chesMatch))
            {
                matchDto = MapMatchToDto(chesMatch);
                return true;
            }

            matchDto = null;
            return false;
        }

        public IEnumerable<ChessMatchDto> GetMatchHistory(long userId)
        {
            return _ChessMatchRepository.GetMatchHistoryByUserId(userId)
                             .Select(MapMatchToDto)
                             .ToList();
        }

        public void EndMatch(long matchId, Dictionary<long, GameResult> resultByUserId, string note)
        {
            var match = _ChessMatchRepository.GetById(matchId);
            match.FinishedAt = DateTime.UtcNow;
            Update(match);

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

        public long StartWithAI(int level, PlayerColor playerColor, long userId)
        {
            var match = new ChessMatch
            {
                GameMode = GameType.PvE,
                StartedAt = DateTime.UtcNow,
            };
            _ChessMatchRepository.Add(match);
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
            _GameStateService.TryAdd(match.Id.ToString(), state);

            _EngineSessionService.Create(match.Id, level); // Tạo session engine với level

            return match.Id;
        }

        public string ComputeAIMove(long matchId, string fen)
        {
            // Lấy nước đi tốt nhất từ engine session tương ứng với matchId
            var bestMove = _EngineSessionService.GetBestMoveAsync(matchId, fen);
            return bestMove.ToString(); // trả về dạng "e2e4"
        }

        /// <summary>
        /// Áp dụng nước đi người chơi vào trạng thái và trả về FEN mới
        /// </summary>
        public async Task<bool> ApplyMoveAsync(long matchId, string fen, string move)
        {
            if (!_GameStateService.TryGet(matchId.ToString(), out var state))
                throw new InvalidOperationException($"Không tìm thấy trạng thái trận đấu với matchId {matchId}");

            bool isLegal = await _EngineSessionService.ApplyMoveAsync(matchId, fen, move);
            if (!isLegal) return false;

            // Cập nhật trạng thái game sau khi move hợp lệ
            state.LastMoveFrom = move.Substring(0, 2);
            state.LastMoveTo = move.Substring(2, 2);
            state.LastMoveBy = state.CurrentTurn;
            state.CurrentTurn = state.CurrentTurn == "white" ? "black" : "white";
            state.LastMoveAt = DateTime.UtcNow;

            _GameStateService.Update(matchId.ToString(), state);

            return true;
        }


        public Task<string> ComputeAIMoveAsync(long matchId, string fen)
        {
            throw new NotImplementedException();
        }
    }

}