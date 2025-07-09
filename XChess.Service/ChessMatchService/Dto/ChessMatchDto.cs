using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Enums;

namespace XChess.Service.ChessMatchService.Dto
{
    public class ChessMatchDto
    {
        public long Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public GameType GameMode { get; set; }
        public TimeSpan InitialTime { get; set; }
    }
}
