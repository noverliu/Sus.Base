using Sus.Base.Core;
using Sus.Base.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public class SusDataProviderManager:BaseDataProviderManager
    {
        public SusDataProviderManager(DataSettings settings):base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new SusException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                //case "sqlce":
                //    return new SqlCeDataProvider();
                default:
                    throw new SusException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }
    }
}
