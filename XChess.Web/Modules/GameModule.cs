using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XChess.Service.GameTimerService;
namespace XChess.Modules
{
    public class GameModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameTimerService>()
               .As<IGameTimerService>()
               .SingleInstance();
        }
    }
}