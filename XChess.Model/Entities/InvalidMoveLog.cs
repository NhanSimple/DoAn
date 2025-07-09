using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;

namespace XChess.Model.Entities
{
    public class InvalidMoveLog : AuditableEntity<long>
    {
        public long? UserId { get; set; }           // Có thể null nếu là AI
        public long MatchId { get; set; }
        public string From { get; set; }            // Ô bắt đầu
        public string To { get; set; }              // Ô kết thúc
        public string Piece { get; set; }           // Quân cờ định đi
        public string Reason { get; set; }          // Lý do không hợp lệ (nếu có)
        public string CurrentFEN { get; set; }      // FEN tại thời điểm gửi nước đi
        public DateTime AttemptTime { get; set; }   // Thời điểm gửi
    }
}
