using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.MatchPlayerService.Dto
{
    public class MatchPlayerDto
    {
        public long MatchId { get; set; }
        public long UserId { get; set; }
        public PlayerColor PlayerColor { get; set; }
    }
}
