using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using XChess.Model.Enums;
using XChess.Model.RealTimeModel;
namespace XChess.Model.RealTimeModel
{
    public class GameState
    {
        public long MatchId { get; set; }

        public string CurrentTurn { get; set; }
        public string LastMoveFrom { get; set; }
        public string LastMoveTo { get; set; }
        public string LastMoveBy { get; set; }
        public string LastPromotion { get; set; }

        public bool IsCapture { get; set; }
        public string CapturedSquare { get; set; }
        public bool IsEnPassant { get; set; }
        public string EnPassantCapturedSquare { get; set; }
        public bool IsCastling { get; set; }

        public bool IsCheck { get; set; }
        public string CheckToColor { get; set; }

        public bool IsCheckmate { get; set; }
        public bool IsDraw { get; set; }
        public bool IsGameOver { get; set; }
        public string Winner { get; set; }
        public string EndReason { get; set; }

        public DateTime LastMoveAt { get; set; }

        public long? WhiteUserId { get; set; }
        public long? BlackUserId { get; set; }
    }
}


