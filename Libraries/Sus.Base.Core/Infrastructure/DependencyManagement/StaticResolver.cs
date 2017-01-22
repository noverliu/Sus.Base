using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Features.OwnedInstances;
using Autofac;

namespace Sus.Base.Core.Infrastructure.DependencyManagement
{
    public static class StaticResolver
    {
        private static IServiceProvider _service;
        public static void Config(IServiceProvider service)
        {
            _service = service;
        }
        public static T Resolve<T>()
        {
            return _service.GetService<T>();
        }
        public static object Resolve(Type type)
        {
            return _service.GetService(type);
        }
    }
}
