using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XChess.Infrastructure.Realtime;
using XChess.Infrastructure.PasswordHasher;
using XChess.Infrastructure.EmailSender;
using XChess.Model;
namespace XChess.Modules
{
    public class InfrastructureModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new StoreModule());
            //builder.RegisterModule<StoreModule>(); không chuyền được tham số 
            builder.RegisterType<XChessRealTimeContext>()
                   .As<IRealtimeContext>()
                   .SingleInstance();
            builder.RegisterType<BcryptPasswordHasher>()
                .As<IPasswordHasher>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GmailEmailSender>().As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
    }
}