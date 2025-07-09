using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.GameTimerService
{
    public class GameTimerDto
    {
        public string MatchId { get; set; }
        public TimeSpan WhiteTimeLeft { get; set; }
        public TimeSpan BlackTimeLeft { get; set; }
    }
}
