using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Enums;

namespace XChess.Service.MatchResultService.Dto
{
    public class MatchResultDto
    {
        public long MatchId { get; set; }
        public long UserId { get; set; }
        public GameResult GameResult { get; set; }
        public string Note { get; set; }
    }
}
