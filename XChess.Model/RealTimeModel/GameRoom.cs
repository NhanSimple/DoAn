using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;

namespace XChess.Model.RealTimeModel
{
    public  class GameRoom
    {
        public string Id { get; set; }
        public long? HostId { get; set; }
        public long? GuestId { get; set; }
        public bool GuestReady { get; set; }
        public bool IsWaiting => GuestId == null;
        public bool IsFull => GuestId != null;
        public bool BothReady => GuestReady;
    }
}

