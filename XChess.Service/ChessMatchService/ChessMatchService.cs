using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Model.Enums;
using XChess.Repository.ChessMatchRepository;
using XChess.Repository.Common;
using XChess.Service.Common;
using XChess.Service.ChessMatchService.Dto;
using XChess.Repository.MatchPlayerRepository;
using XChess.Repository.MatchResultRepository;

namespace XChess.Service.ChessMatchService
{
    public class ChessMatchService : EntityService<ChessMatch>, IChessMatchService
    {
        private readonly IChessMatchRepository _ChessMatchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchPlayerRepository _matchPlayerRepository;
        private readonly IMatchResultRepository _matchResultRepository;
        public ChessMatchService(IUnitOfWork unitOfWork,
            IChessMatchRepository chessMatchRepository,
            IMatchPlayerRepository matchPlayerRepository,
            IMatchResultRepository matchResultRepository
            ) : base(unitOfWork, chessMatchRepository)
        {
            _ChessMatchRepository = chessMatchRepository;
            _unitOfWork = unitOfWork;
            _matchPlayerRepository = matchPlayerRepository;
            _matchResultRepository = matchResultRepository;

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
    }
}
