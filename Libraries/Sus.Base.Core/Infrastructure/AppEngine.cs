using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sus.Base.Core.Configuration;
using Sus.Base.Core.Infrastructure.DependencyManagement;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Sus.Base.Core.Infrastructure
{
    public class AppEngine : IEngine
    {
        #region Fields
        
        private ContainerManager _containerManager;
        #endregion

        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }
        public void Initialize(IServiceCollection services, AppConfig config)
        {
            services.AddSingleton<ITypeFinder,WebAppTypeFinder>(); 
            RegisterDependencies(services,config);

            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                RunStartupTasks();
            }
        }
        protected virtual void RegisterDependencies(IServiceCollection service,AppConfig config)
        {
            ContainerBuilder builder = new ContainerBuilder(),
                            tmpbuilder = new ContainerBuilder();
            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.

            //dependencies
            var sp = service.BuildServiceProvider();
            var typeFinder = sp.GetService<ITypeFinder>();
            var env = service.BuildServiceProvider().GetService<IHostingEnvironment>();

            tmpbuilder.RegisterInstance(env).As<IHostingEnvironment>().SingleInstance();
            tmpbuilder.RegisterInstance(service).As<IServiceCollection>().SingleInstance();
            tmpbuilder.RegisterInstance(config).As<AppConfig>().SingleInstance();
            tmpbuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
            tmpbuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            builder.RegisterInstance(env).As<IHostingEnvironment>().SingleInstance();
            builder.RegisterInstance(service).As<IServiceCollection>().SingleInstance();
            builder.RegisterInstance(config).As<AppConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            var container = tmpbuilder.Build();
            this._containerManager = new ContainerManager(container);

            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder,sp);
            builder.Populate(service);
            container = builder.Build();
            this._containerManager = new ContainerManager(container);
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
