using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public static class WebSocketExtensions
    {
        public static Task SendTextAsync(this WebSocket socket, string message, CancellationToken token)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes);
            return socket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
        }
    }
}
