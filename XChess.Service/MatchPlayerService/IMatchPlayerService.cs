using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Service.Common;
using XChess.Service.MatchPlayerService.Dto;

namespace XChess.Service.MatchPlayerService
{
    public interface IMatchPlayerService:IEntityService<MatchPlayer>
    {
        IEnumerable<MatchPlayerDto> GetPlayersInMatch(long matchId);
        bool TryGetPlayersInMatch(long matchId, out List<MatchPlayerDto> players);

        bool TryGetPlayerColor(long matchId, long userId, out PlayerColor color);
       

        bool IsUserInMatch(long matchId, long userId);
    }
}
