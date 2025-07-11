﻿using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XChess.Modules;
using XChess.RealTime.Server;

using XChess.Service.GameTimerService;
namespace XChess
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<WebSocketModule>();
            //builder.RegisterControllers
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new StoreModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EngineModule());
            builder.RegisterModule(new AutoMapperModule());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            var webSocketServer = container.Resolve<IWebSocketServer>();
            webSocketServer.Start();



        }
    }
}
