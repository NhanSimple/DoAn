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
        public string CurrentFEN { get; set; }
        public PlayerColor CurrentTurn { get; set; }
        //public GameTimerState TimerState { get; set; }
    }
}

