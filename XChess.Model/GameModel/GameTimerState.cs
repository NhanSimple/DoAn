using System;
namespace XChess.Model.GameModel
{
    public class GameTimerState
    {
        public TimeSpan InitialTime { get; set; }
        public TimeSpan WhiteTimeLeft { get; set; }
        public TimeSpan BlackTimeLeft { get; set; }
        public DateTime LastMoveTime { get; set; }
        public PlayerColor CurrentTurn { get; set; }
        public bool IsPaused { get; set; } = false;

    }

}