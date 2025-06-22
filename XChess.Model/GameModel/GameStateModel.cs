using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using XChess.Model.Enums;
using XChess.Model.GameModel;
namespace XChess.Model.GameModel
{
    public class GameStateModel
    {
        public string RoomId { get; set; }
        public PlayerConnection WhitePlayer { get; set; }
        public PlayerConnection BlackPlayer { get; set; }
        public string CurrentFEN { get; set; }
        public PlayerColor CurrentTurn { get; set; } = PlayerColor.White;
        public DateTime LastMoveTime { get; set; }
        public TimeSpan WhiteTimeRemaining { get; set; }
        public TimeSpan BlackTimeRemaining { get; set; }
        public GameStatus Status { get; set; } = GameStatus.Ongoing;

        public List<string> MoveHistory { get; set; }

    }
}

