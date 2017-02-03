using Sus.Base.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Sus.Base.Core.Configuration;
using Sus.Base.Core.Infrastructure;
using Sus.Base.Services.Security;
using Sus.Base.Services.User;

namespace Sus.Base.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IServiceProvider sp, AppConfig config)
        {
           
        }
    }
}
