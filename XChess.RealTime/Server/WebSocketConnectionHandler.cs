using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public class WebSocketConnectionHandler:IWebSocketConnectionHandler
    {
        private readonly IWebSocketMessageHandler _messageHandler;
        public WebSocketConnectionHandler(IWebSocketMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task HandleAsync(HttpListenerContext context, CancellationToken token)
        {
            var wsContext = await context.AcceptWebSocketAsync(null);
            var socket = wsContext.WebSocket;

            Debug.WriteLine("✅ Client WebSocket đã kết nối");

            var buffer = new byte[1024];

            while (socket.State == WebSocketState.Open && !token.IsCancellationRequested)
            {
                try
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), token);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client đóng", token);
                        Debug.WriteLine("🛑 Client đóng kết nối");
                        return;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await _messageHandler.HandleMessageAsync(socket, message, token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("⚠️ Lỗi socket: " + ex.ToString());
                    break;
                }
            }

            if (socket.State == WebSocketState.Open)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Kết thúc", token);
            }
        }
    }
}
