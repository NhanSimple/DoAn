using System;
namespace XChess.Model.RealTimeModel
{
    public class GameTimer
    {
        public long MatchId { get; set; }
        public TimeSpan WhiteTimeLeft { get; set; }
        public TimeSpan BlackTimeLeft { get; set; }

    }
}