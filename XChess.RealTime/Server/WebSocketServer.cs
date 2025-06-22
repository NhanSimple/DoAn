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
namespace XChess.RealTime
{
    public class WebSocketServer : IWebSocketServer
    {
        private readonly IGameTimerService _gameTimerService;

        private readonly IMessageHandler _messageHandler;
        private readonly IConnectionManager _connectionManager;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public WebSocketServer(IMessageHandler messageHandler,
            IGameTimerService gameTimerService,
            IConnectionManager connectionManager)
        {
            _gameTimerService = gameTimerService;
            _messageHandler = messageHandler;
            _connectionManager = connectionManager;
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
                        _ = HandleWebSocketAsync(context, cancellationToken);
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


        private async Task HandleWebSocketAsync(HttpListenerContext context, CancellationToken token)
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

                    // Nếu client đóng kết nối
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client đóng", token);
                        Debug.WriteLine("🛑 Client đóng kết nối");
                        return;
                    }

                    // Tạm thời chỉ log tin nhận
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Debug.WriteLine("📩 Tin nhận: " + message);

                    // === TẠM THỜI: xử lý tin nhắn đơn giản ===
                    if (message == "create")
                    {
                        _gameTimerService.CreateGame("test_match", TimeSpan.FromMinutes(5));
                        Debug.WriteLine("✅ Đã tạo timer cho match test_match");
                    }
                    else if (message == "switch")
                    {
                        _gameTimerService.SwitchTurn("test_match");
                        var state = _gameTimerService.GetState("test_match");

                        if (state != null)
                        {
                            Debug.WriteLine($"🕒 Trắng: {state.WhiteTimeLeft.TotalSeconds:0.0}s, " +
                                            $"Đen: {state.BlackTimeLeft.TotalSeconds:0.0}s, " +
                                            $"Lượt: {state.CurrentTurn}");
                        }
                        else
                        {
                            Debug.WriteLine("❌ Không tìm thấy game test_match");
                        }
                    }
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
