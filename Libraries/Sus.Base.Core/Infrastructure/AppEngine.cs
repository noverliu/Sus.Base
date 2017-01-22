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
        
        //private ContainerManager _containerManager;
        private IContainer _container;
        #endregion

        public IContainer Container
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
            
            //dependencies
            var sp = services.BuildServiceProvider();
            var typeFinder = sp.GetService<ITypeFinder>();

            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(services, typeFinder,sp,config);

        }

        //public object Resolve(Type type)
        //{
        //    return _container.RequestServices.GetService(type);
        //}

        //public T Resolve<T>() where T : class
        //{
        //    return SusHttpContext.Current.RequestServices.GetService<T>();
        //}

    }
}
