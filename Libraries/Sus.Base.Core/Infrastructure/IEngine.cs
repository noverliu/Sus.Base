using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Infrastructure.DependencyManagement;
using Sus.Base.Core.Configuration;
using Autofac;

namespace Sus.Base.Core.Infrastructure
{
    public interface IEngine
    {
        /// <summary>
        /// Container manager
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize(IServiceCollection services, AppConfig config);

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name = "T" > T </ typeparam >
        /// < returns ></ returns >
        T Resolve<T>() where T : class;

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);
        void CreateScope();
        void RunStartupTask(IServiceProvider service);
    }
}
