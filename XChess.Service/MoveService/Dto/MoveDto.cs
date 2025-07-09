using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.MoveService.Dto
{
    public class MoveDto
    {
        public long MatchId { get; set; }
        public long? UserId { get; set; }
        public int MoveNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Piece { get; set; }
        public DateTime MoveTime { get; set; }
        public string FenAfter { get; set; }
    }
}
