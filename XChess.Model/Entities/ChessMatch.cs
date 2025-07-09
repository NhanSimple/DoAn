using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;
using XChess.Model.Enums;

namespace XChess.Model.Entities
{
    public class ChessMatch : AuditableEntity<long>
    {
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public GameType GameMode { get; set; }
        public TimeSpan InitialTime { get; set; }

        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
        public virtual ICollection<MatchResult> MatchResults { get; set; }
        public virtual ICollection<Move> Moves { get; set; }

    }
}
