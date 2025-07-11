﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XChess.RealTime.Server
{
    public interface IWebSocketConnectionHandler
    {
        Task HandleAsync(HttpListenerContext context, CancellationToken token);
    }
}
