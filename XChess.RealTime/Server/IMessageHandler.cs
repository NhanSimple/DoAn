using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string message, WebSocket socket);
    }
}
