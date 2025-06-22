using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
namespace XChess.Modules
{
    public class RepositoryModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("XChess.Repository"))
                                   .Where(t => t.Name.EndsWith("Repository"))
                                     .AsImplementedInterfaces()
                                      .InstancePerLifetimeScope();
        }
    }
}