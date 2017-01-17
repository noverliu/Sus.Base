using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Configuration
{
    public class AppConfig
    {
        public bool IgnoreStartupTasks { get; set; }
        public string assemblySkipLoadingPattern { get; set; }
        public string assemblyRestrictToLoadingPattern { get; set; }
    }
}
