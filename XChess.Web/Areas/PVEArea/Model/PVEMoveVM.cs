using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XChess.Areas.PVEArea.Model
{
    public class PVEMoveVM
    {
        public long MatchId { get; set; }      // Id của trận đấu
        public string Fen { get; set; }        // FEN hiện tại của bàn cờ
        public string Move { get; set; }       // Nước đi theo chuẩn UCI (ví dụ: "e2e4")
        public string PlayerColor { get; set; } // "w" hoặc "b" - màu của người chơi hiện tại
    }
}