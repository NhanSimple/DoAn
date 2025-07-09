using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;

namespace XChess.Model.Entities
{
    public class MatchPlayer:AuditableEntity<long>
    {
        [NotMapped]
        public override long Id { get; set; }
        public long MatchId { get; set; }
        public long UserId { get; set; }
        public PlayerColor PlayerColor { get; set; }

        public virtual User User { get; set; }
        public virtual ChessMatch Match { get; set; }
    }
}
