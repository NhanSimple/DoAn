using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.GameModel
{
    public class PlayerConnection
    {
        public string PlayerId { get; set; }
        public WebSocket Socket { get; set; }
    }
}
