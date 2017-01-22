using Sus.Base.Core;
using Sus.Base.Core.Data;
using Sus.Base.Core.Infrastructure;
using Sus.Base.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public int Order
        {
            get
            {
                return -1000;
            }
        }

        public void Execute()
        {
            var settings = StaticResolver.Resolve<DataSettings>();
            if (settings != null && settings.IsValid())
            {
                var provider = StaticResolver.Resolve<IDataProvider>();
                if (provider == null)
                    throw new SusException("No IDataProvider found");
                provider.SetDatabaseInitializer();
            }
        }
    }
}
