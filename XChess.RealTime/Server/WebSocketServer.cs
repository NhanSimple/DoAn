using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XChess.RealTime.Server;
using System.Diagnostics;
using XChess.Service.GameTimerService;
using XChess.Model;
using XChess.Service.GameRoomService.cs;
using XChess.Model.RealTimeModel;
namespace XChess.RealTime
{
    public class WebSocketServer : IWebSocketServer
    {

        private readonly  IWebSocketConnectionHandler _webSocketConnectionHandler;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public WebSocketServer(IWebSocketConnectionHandler webSocketConnectionHandler)
        {
            _webSocketConnectionHandler = webSocketConnectionHandler;
        }
        public void Start()
        {
            _ = ListenAsync(_cts.Token);
            Debug.WriteLine("Bắt đầu lắng nghe");
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private async Task ListenAsync(CancellationToken cancellationToken)
        {
            try
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://+:8081/ws/");
                listener.Start();
                Debug.WriteLine("WebSocket server đang chạy trên ws://<ip>:8081/ws/");
                while (!cancellationToken.IsCancellationRequested)
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    if (context.Request.IsWebSocketRequest)
                    {
                        _ = _webSocketConnectionHandler.HandleAsync(context, cancellationToken);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                listener.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("🔥 Lỗi khởi động WebSocket server: " + ex.ToString());
            }
        }
    }
}
