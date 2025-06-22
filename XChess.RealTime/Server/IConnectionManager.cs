using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public  interface IConnectionManager
    {
        string AddSocket(WebSocket socket);
        Task RemoveSocketAsync(string id);
        WebSocket GetSocketById(string id);
        IEnumerable<WebSocket> GetAll();
    }
}
