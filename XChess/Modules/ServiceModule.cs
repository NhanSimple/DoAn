using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace XChess.Modules
{
    public class ServiceModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("XChess.Service"))
                                            .Where(t => t.Name.EndsWith("Service"))
                                            .AsImplementedInterfaces()
                                            .InstancePerLifetimeScope(); 
        }
    }
}