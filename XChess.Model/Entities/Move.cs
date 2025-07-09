using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;

namespace XChess.Model.Entities
{
    public class Move:AuditableEntity<long>
    {
        public long MatchId { get; set; }
        public long? UserId { get; set; }
        public int MoveNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Piece { get; set; }
        public DateTime MoveTime { get; set; }
        public string FenAfter { get; set; }
        public virtual User User { get; set; }
        public virtual ChessMatch Match { get; set; }
    }
}
