using Sus.Base.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Sus.Base.Core.Infrastructure;
using Sus.Base.Data;
using Sus.Base.Core.Data;
using Sus.Base.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sus.Base.Core.Configuration;
using Sus.Base.Core.Caching;
using Sus.Base.Framework.Mvc.Routes;
using Sus.Base.Framework.UI;
using Microsoft.Extensions.DependencyInjection;
using Sus.Base.Services.Security;
using Sus.Base.Services.User;

namespace Sus.Base.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder,IServiceProvider sp,AppConfig config)
        {            
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            var dataProvider = (new SusDataProviderManager(dataProviderSettings)).LoadDataProvider();
            //services.AddSingleton(typeof(DataSettings), dataProviderSettings);
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            //services.AddSingleton(typeof(BaseDataProviderManager), new SusDataProviderManager(dataProviderSettings));
            builder.Register(x => new SusDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            //services.AddSingleton(typeof(IDataProvider), s => s.GetService<BaseDataProviderManager>().LoadDataProvider());
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(c =>
            {
                var contextBuilder = c.Resolve<IDataProvider>();
                return new SusDbContext(contextBuilder.BuildOptions(dataProviderSettings.DataConnectionString));
            }).As<IDbContext>().InstancePerLifetimeScope();
            //services.AddScoped(typeof(SusDbContext), s =>
            //{
            //    return new SusDbContext(s.GetService<IDataProvider>().BuildOptions(dataProviderSettings.DataConnectionString));
            //});

            //services.AddScoped(typeof(IRepository<>),typeof(SusRepository<>));
            builder.RegisterGeneric(typeof(SusRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            switch (config.CacheType)
            {
                case "Redis":
                    //services.AddScoped<ICacheManager, RedisCacheManager>();
                    builder.RegisterType<RedisCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").InstancePerLifetimeScope();
                    break;
                case "Memory":
                    //services.AddSingleton<ICacheManager, MemoryCacheManager>();
                    builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
                    break;
                default:
                    //services.AddScoped<ICacheManager, NullCache>();
                    builder.RegisterType<NullCache>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").InstancePerLifetimeScope();
                    break;
            }
            //builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();
            //services.AddSingleton<IRoutePublisher, RoutePublisher>();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            //services.AddScoped<IPageHeadBuilder, PageHeadBuilder>();
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<UserIdentifyService>().As<IUserIdentifyService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
