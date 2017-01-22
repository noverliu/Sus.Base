using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Caching
{
    public static class Extensions
    {
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            try
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }
            }
            catch (Exception ex)
            {                
                return acquire();
            }

            var result = acquire();

            try
            {
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
            catch (Exception ex)
            {                
                return result;
            }
        }
    }
}
