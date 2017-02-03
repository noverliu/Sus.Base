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
using Microsoft.AspNetCore.Http;

namespace Sus.Base.Core.Infrastructure
{
    public class AppEngine : IEngine
    {
        #region Fields
        //public AutofacServiceProvider _afServiceProvider;
        //private ContainerManager _containerManager;
        private ContainerManager _container;
        #endregion

        public ContainerManager Container
        {
            get
            {
                return _container;
            }
        }
        protected virtual void RunStartupTasks(IServiceProvider service)
        {
            var typeFinder =service.GetService<ITypeFinder>();
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

            ////startup tasks
            //if (!config.IgnoreStartupTasks)
            //{
            //    RunStartupTasks();
            //}
        }
        public void RunStartupTask(IServiceProvider service)
        {
            RunStartupTasks(service);
        }
        protected virtual void RegisterDependencies(IServiceCollection services,AppConfig config)
        {
            //IServiceCollection inject
            var sp = services.BuildServiceProvider();
            var typeFinder = sp.GetService<ITypeFinder>();
            var env = sp.GetService<IHostingEnvironment>();
            //Autofac container inject
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            //builder.RegisterInstance(env).As<IHostingEnvironment>().SingleInstance();
            builder.RegisterInstance(config).As<AppConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.Populate(services);
            var container = builder.Build();
            this._container = new ContainerManager(container);
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();

            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder,sp,config);
            builder.Update(container);
            //_afServiceProvider = new AutofacServiceProvider(_container.Container);
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
        public void CreateScope()
        {
            _container.BuildScope();
        }
    }
}
