using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public interface IWebSocketMessageHandler
    {
        Task HandleMessageAsync(WebSocket socket, string message, CancellationToken token);
    }
}
