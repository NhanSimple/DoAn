using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XChess.Engine;

namespace XChess.Modules
{
    public class EngineModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockfishEngine>()
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();



        }
    }
}