using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using XChess.Model.Enums;
using XChess.Model.RealTimeModel;
using XChess.RealTime.Server;
using XChess.Service.ChessMatchService;
using XChess.Service.GameRoomService.cs;
using XChess.Service.MatchPlayerService;
namespace XChess.RealTime.Server
{
    public class WebSocketMessageHandler : IWebSocketMessageHandler
    {
        private readonly IGameRoomService _gameRoomService;
        private readonly IChessMatchService _chessMatchService;
        public WebSocketMessageHandler(IGameRoomService gameRoomService,
            IChessMatchService chessMatchService)
        {
            _gameRoomService = gameRoomService;
            _chessMatchService = chessMatchService;
        }
        public async Task HandleMessageAsync(WebSocket socket, string message, CancellationToken token)
        {
            Debug.WriteLine("Tin nhận: " + message);

            switch (message.ToLowerInvariant())
            {
                case "create":
                    Debug.WriteLine("Đã tạo timer cho match test_match");
                    var rng = new Random();
                    var userId = (long)rng.Next(100000, 999999);
                    var conn = new PlayerConnection
                    {
                        UserId = userId, // Giả lập nếu chưa có auth
                        Socket = socket
                    };

                    var room = _gameRoomService.JoinOrAddRoom(conn);
                    var info = $"Đã tham gia phòng: {room.Id} - P1: {room.HostId}, P2: {room.GuestId}";

                    Debug.WriteLine(info);
                    await socket.SendTextAsync(info, token);
                    if (room.IsFull)
                    {
                        Debug.WriteLine("Tạo trận đấu giữa 2 người chơi...");

                        // Lấy Player1 và Player2
                        var player1 = room.HostId.Value;
                        var player2 = room.GuestId.Value;

                        var initialTime = TimeSpan.FromMinutes(5); // ví dụ 5 phút mỗi bên
                        var gameMode = GameType.PvP; // hoặc chế độ khác nếu có lựa chọn
                  
                        // Gọi MatchService để tạo trận
                        var match = _chessMatchService.CreateMatch(player1, player2, gameMode, initialTime);

                        Debug.WriteLine($"Đã tạo Match ID = {match.Id}");

                        // Gửi thông tin match cho cả hai client
                        var matchInfo = $"Đã tạo Match ID: {match.Id} cho người chơi {player1} vs {player2}";
                        Debug.WriteLine("➡ match.StartedAt = " + match.StartedAt.ToString("u"));
                        await socket.SendTextAsync(matchInfo, token);
                    }
                    break;


                case "switch":
                    Debug.WriteLine("Không tìm thấy game test_match");
                    break;

                case "ping":
                    Debug.WriteLine("Ping nhận");
                    break;

                default:
                    Debug.WriteLine($"⚠️ Message không rõ: {message}");
                    break;
            }
            await WebSocketExtensions.SendTextAsync(socket, "✅ Server đã nhận: " + message, token);
            // Có thể phản hồi lại client nếu cần
            // await socket.SendAsync(...);
        }

    }
}
