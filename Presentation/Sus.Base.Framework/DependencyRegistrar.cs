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

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder,IServiceProvider sp)
        {
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new SusDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x=>x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(c =>
            {
                var contextBuilder = c.Resolve<IDataProvider>();
                return new SusDbContext(contextBuilder.BuildOptions(dataProviderSettings.DataConnectionString));
            }).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SusRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
