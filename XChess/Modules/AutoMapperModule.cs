using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XChess.Model.Entities;

namespace XChess.Modules
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
                cfg.AllowNullDestinationValues = true;
                cfg.ValidateInlineMaps = false;
                //cfg.IgnoreUnmapped();
                //cfg.CreateMap<DM_DulieuDanhmuc, SelectListItem>()
                //       .ForMember(x => x.Text, y => y.MapFrom(x => x.Name.ToString()))
                //       .ForMember(x => x.Value, y => y.MapFrom(x => x.Id.ToString()));
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}