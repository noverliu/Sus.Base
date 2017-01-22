using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sus.Base.Core.Configuration;
using Sus.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Infrastructure
{
    public class EngineContext
    {
        private static IServiceCollection _services;
        private static IServiceProvider _service;
        #region Methods

        public static IEngine Initialize(bool forceRecreate,IServiceCollection services)
        {
            lock (typeof(EngineContext))
            {
                
                if (Singleton<IEngine>.Instance == null || forceRecreate)
                {
                    _services = services;
                    Singleton<IEngine>.Instance = new AppEngine();
                    _service = services.BuildServiceProvider();
                    var config = _service.GetService<IOptions<AppConfig>>().Value;
                    var httpcontext = _service.GetService<HttpContext>();
                    Singleton<IEngine>.Instance.Initialize(services, config);
                }
                return Singleton<IEngine>.Instance;
            }
        }
        public static void RunStartUpTask(IServiceProvider service)
        {
            var config = service.GetService<IOptions<AppConfig>>().Value;
            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                Current.RunStartupTask(service);
            }
        }
        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton Nop engine used to access Nop services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false,_services);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
