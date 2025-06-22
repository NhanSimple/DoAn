using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public interface IWebSocketServer
    {
        void Start();
        void Stop();
    }
}
