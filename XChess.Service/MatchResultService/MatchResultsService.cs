using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;
using XChess.Repository.MatchResultRepository;
using XChess.Service.Common;
using XChess.Service.MatchResultService.Dto;

namespace XChess.Service.MatchResultService
{
    public class MatchResultService :EntityService<MatchResult>, IMatchResultService
    {
        private readonly IMatchResultRepository _ResultRepository;
        private readonly IUnitOfWork _UnitOfWork;

        public MatchResultService(IUnitOfWork unitOfWork,IMatchResultRepository resultRepository)
            :base(unitOfWork, resultRepository)
        {
            _ResultRepository = resultRepository;
            _UnitOfWork = unitOfWork;
        }

        public bool TryGetResult(long matchId, long userId, out MatchResultDto result)
        {
            MatchResult entity;
            var found = _ResultRepository.TryGetResult(matchId, userId, out entity);

            if (!found)
            {
                result = null;
                return false;
            }

            result = new MatchResultDto
            {
                MatchId = entity.MatchId,
                UserId = entity.UserId,
                GameResult = entity.GameResult,
                Note = entity.Note
            };

            return true;
        }

        public IEnumerable<MatchResultDto> GetResults(long matchId)
        {
            var results = _ResultRepository.GetResultsByMatchId(matchId);
            return results.Select(r => new MatchResultDto
            {
                MatchId = r.MatchId,
                UserId = r.UserId,
                GameResult = r.GameResult,
                Note = r.Note
            }).ToList();
        }

        public bool HasResult(long matchId)
        {
            return _ResultRepository.GetResultsByMatchId(matchId).Any();
        }
    }
}
