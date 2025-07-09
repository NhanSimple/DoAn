using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.RealTimeModel
{
    public class PlayerConnection
    {

        public long UserId { get; set; }
        public WebSocket Socket { get; set; }
    }
}
