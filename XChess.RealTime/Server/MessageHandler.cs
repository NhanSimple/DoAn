using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IGameSessionManager _sessionManager;

        public MessageHandler(IGameSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public async Task HandleMessageAsync(string message, WebSocket socket)
        {
            // Deserialize, validate, route message to session/game logic
            await Task.CompletedTask;
        }
    }
}
