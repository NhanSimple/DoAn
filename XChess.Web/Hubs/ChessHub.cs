using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace XChess.Hubs
{
    public class ChessHub: Hub
    {
        public void SendMove(string roomId, string from, string to, string fen)
        {
            Clients.Group(roomId).receiveMove(from, to, fen);
        }
        public override Task OnConnected()
        {
            var roomId = Context.QueryString["roomId"];
            if (!string.IsNullOrEmpty(roomId))
            {
                Groups.Add(Context.ConnectionId, roomId);
            }

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var roomId = Context.QueryString["roomId"];
            if (!string.IsNullOrEmpty(roomId))
            {
                Groups.Remove(Context.ConnectionId, roomId);
            }

            return base.OnDisconnected(stopCalled);
        }

        public void Resign(string roomId, string player)
        {
            Clients.Group(roomId).opponentResigned(player);
        }

        public void OfferDraw(string roomId)
        {
            Clients.Group(roomId).drawOffered();
        }

        public void AcceptDraw(string roomId)
        {
            Clients.Group(roomId).drawAccepted();
        }
    }
}