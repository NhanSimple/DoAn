using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime
{
    public class PlayerConnection
    {
        public string ConnectionId { get; set; }
        public WebSocket Socket { get; set; }
        public string PlayerId { get; set; }
        public string SessionId { get; set; }
    }
}
