using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Service.Common;
using XChess.Service.MatchResultService.Dto;
namespace XChess.Service.MatchResultService
{
    public interface IMatchResultService:IEntityService<MatchResult>
    {
        bool TryGetResult(long matchId, long userId, out MatchResultDto result);

        IEnumerable<MatchResultDto> GetResults(long matchId);

        bool HasResult(long matchId);
    }
}
