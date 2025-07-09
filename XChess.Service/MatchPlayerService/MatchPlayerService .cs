using System.Collections.Generic;
using System.Linq;
using XChess.Model.Entities;
using XChess.Repository.Common;
using XChess.Repository.MatchPlayerRepository;
using XChess.Service.Common;
using XChess.Service.MatchPlayerService.Dto;

namespace XChess.Service.MatchPlayerService
{
    public class MatchPlayerService : EntityService<MatchPlayer>, IMatchPlayerService
    {
        private readonly IMatchPlayerRepository _MatchPlayerRepository;
        private readonly IUnitOfWork _UnitOfWork;
        public MatchPlayerService(IUnitOfWork unitOfWork,
            IMatchPlayerRepository matchPlayerRepository)
            : base(unitOfWork, matchPlayerRepository)
        {
            _MatchPlayerRepository = matchPlayerRepository;
            _UnitOfWork = unitOfWork;
        }

        public IEnumerable<MatchPlayerDto> GetPlayersInMatch(long matchId)
        {
            List<MatchPlayer> players;
            var found = _MatchPlayerRepository.TryGetPlayersByMatchId(matchId, out players);

            if (!found)
                return new List<MatchPlayerDto>();

            return players.Select(p => new MatchPlayerDto
            {
                MatchId = p.MatchId,
                UserId = p.UserId,
                PlayerColor = p.PlayerColor
            }).ToList();
        }
        public bool TryGetPlayersInMatch(long matchId, out List<MatchPlayerDto> players)
        {
            List<MatchPlayer> entities;
            var found = _MatchPlayerRepository.TryGetPlayersByMatchId(matchId, out entities);

            players = entities.Select(p => new MatchPlayerDto
            {
                MatchId = p.MatchId,
                UserId = p.UserId,
                PlayerColor = p.PlayerColor
            }).ToList();

            return found;
        }

        public bool TryGetPlayerColor(long matchId, long userId, out PlayerColor color)
        {
            return _MatchPlayerRepository.TryGetPlayerColor(matchId, userId, out color);
        }

        public bool IsUserInMatch(long matchId, long userId)
        {
            return _MatchPlayerRepository.Exists(matchId, userId);
        }

    }
}
