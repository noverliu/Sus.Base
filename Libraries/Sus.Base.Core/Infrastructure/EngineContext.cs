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
        public static IServiceCollection _services;
        private static IServiceProvider _service;
        #region Methods

        public static IEngine Initialize(bool forceRecreate)
        {
            lock (typeof(EngineContext))
            {
                if (Singleton<IEngine>.Instance == null || forceRecreate)
                {
                    Singleton<IEngine>.Instance = new AppEngine();
                    _service = _services.BuildServiceProvider();
                    var config = _service.GetService<IOptions<AppConfig>>().Value;
                    Singleton<IEngine>.Instance.Initialize(_services, config);
                }
                return Singleton<IEngine>.Instance;
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
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
