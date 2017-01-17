using Sus.Base.Core;
using Sus.Base.Core.Data;
using Sus.Base.Core.Infrastructure;
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
            var settings = EngineContext.Current.Resolve<DataSettings>();
            if (settings != null && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<IDataProvider>();
                if (provider == null)
                    throw new SusException("No IDataProvider found");
                provider.SetDatabaseInitializer();
            }
        }
    }
}
