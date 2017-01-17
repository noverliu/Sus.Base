using Newtonsoft.Json;
using StackExchange.Redis;
using Sus.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Base.Core.Caching
{
    public class RedisCacheManager:ICacheManager
    {
        protected IDatabase _cache;

        private ConnectionMultiplexer _connection;

        public RedisCacheManager(string constr,int dbid = 1)
        {
            _connection = ConnectionMultiplexer.Connect(constr);
            _cache = _connection.GetDatabase(dbid);
        }
       
        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public string GetKeyForRedis(string key)
        {
            return  key;
        }
        public T Get<T>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            var rValue = _cache.StringGet(key);
            if (!rValue.HasValue)
                return default(T);
            var result = Deserialize<T>(rValue);

            this.Set(key, result, 0);
            return result;
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            _cache.StringSet(key, entryBytes, expiresIn);
        }

        public bool IsSet(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExists(GetKeyForRedis(key));
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            _cache.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            foreach(var ep in _connection.GetEndPoints())
            {
                var server = _connection.GetServer(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*", database: _cache.Database);
                foreach(var key in keys)
                {
                    _cache.KeyDelete(key);
                }
            }
            
        }

        public void Clear()
        {
            foreach (var ep in _connection.GetEndPoints())
            {
                var server = _connection.GetServer(ep);
                var keys = server.Keys(_cache.Database);
                foreach (var key in keys)
                {
                    _cache.KeyDelete(key);
                }
            }
        }

        public bool Lock(string key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
