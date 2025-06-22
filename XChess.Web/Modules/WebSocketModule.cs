using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XChess.RealTime;
using XChess.RealTime.Server;


namespace XChess.Modules
{
    public class WebSocketModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionManager>()
                .As<IConnectionManager>()
                .SingleInstance();

            builder.RegisterType<WebSocketServer>()
                .As<IWebSocketServer>()
                .SingleInstance();

            builder.RegisterType<MessageHandler>()
                   .As<IMessageHandler>()
                   .SingleInstance();

            builder.RegisterType<GameSessionManager>()
                   .As<IGameSessionManager>()
                   .SingleInstance();
        }
    }
}