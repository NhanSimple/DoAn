using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Entities
{
    public class Move
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public string FenBefore { get; set; }  
        public string FenAfter { get; set; }     
        public string MoveUci { get; set; }      // e.g., "e2e4", "g1f3"
        public DateTime PlayedAt { get; set; }   // Thời điểm thực hiện nước đi
        public string PlayerId { get; set; }
    }
}
