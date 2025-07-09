using Autofac;
using System.Linq;
using System.Reflection;

namespace XChess.Modules
{
    public class StoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("XChess.Store"))
                .Where(t => t.Name.EndsWith("Store"))
                .AsImplementedInterfaces()
                 .SingleInstance();
        }
    }
}