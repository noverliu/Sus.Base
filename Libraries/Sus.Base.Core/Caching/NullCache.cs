using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Caching
{
    public class NullCache : ICacheManager
    {
        public void Clear()
        {
            
        }

        public void Dispose()
        {
            
        }

        public T Get<T>(string key)
        {
            return default(T);
        }

        public bool IsSet(string key)
        {
            return false;
        }

        public bool Lock(string key)
        {
            return true;
        }

        public void Remove(string key)
        {
            
        }

        public void RemoveByPattern(string pattern)
        {
            
        }

        public void Set(string key, object data, int cacheTime)
        {
            
        }
    }
}
