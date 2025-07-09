using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;
using XChess.Model.Enums;

namespace XChess.Model.Entities
{
    public class MatchResult : AuditableEntity<long>
    {
        [NotMapped]
        public override long Id { get; set; }
        public long MatchId { get; set; }
        public long UserId { get; set; }
        public GameResult GameResult { get; set; }
        public string Note { get; set; }
    }
}
