using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.Realtime;
using XChess.Store.Common;
using XChess.Model.RealTimeModel;

namespace XChess.Store.GameTimerStore
{
    public  class GameTimerStore: GenericStore<GameTimer>, IGameTimerStore
    {
        public GameTimerStore(IRealtimeContext context) : base(context)
        {

        }

        /// <summary>
        /// Khởi tạo bộ đếm thời gian mới.
        /// </summary>
        public void CreateTimer(long matchId, TimeSpan totalTime)
        {
            var id = matchId.ToString();
            var timer = new GameTimer
            {
                MatchId = matchId,
                WhiteTimeLeft = totalTime,
                BlackTimeLeft = totalTime
            };

            TryAdd(id, timer);
        }

        public (TimeSpan? white, TimeSpan? black) GetTimeLeft(long matchId)
        {
            var id = matchId.ToString();
            var timer = GetById(id);
            if (timer == null) return (null, null);

            return (timer.WhiteTimeLeft, timer.BlackTimeLeft);
        }

        public void RemoveTimer(long matchId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTime(long matchId, bool isWhite, TimeSpan timeLeft)
        {
            var id = matchId.ToString();
            var timer = GetById(id);
            if (timer != null)
            {
                if (isWhite) timer.WhiteTimeLeft = timeLeft;
                else timer.BlackTimeLeft = timeLeft;
                TryAdd(id, timer); // Overwrite
            }
        }



    }
}
