using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Sus.Base.Core.Caching
{
    public class PerRequestCacheManager : ICacheManager
    {
        private readonly HttpContext _context;
        public PerRequestCacheManager(HttpContext context)
        {
            this._context = context;
        }
        protected virtual IDictionary<object,object> GetItems()
        {
            if (_context != null)
                return _context.Items;

            return null;
        }
        public void Clear()
        {
            var items = GetItems();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                keysToRemove.Add(enumerator.Current.Key.ToString());
            }

            foreach (string key in keysToRemove)
            {
                items.Remove(key);
            }
        }

        public void Dispose()
        {
            
        }

        public T Get<T>(string key)
        {
            var items = GetItems();
            if (items == null)
                return default(T);

            return (T)items[key];
        }

        public bool IsSet(string key)
        {
            var items = GetItems();
            if (items == null)
                return false;

            return (items[key] != null);
        }

        public bool Lock(string key)
        {
            return true;
        }

        public void Remove(string key)
        {
            var items = GetItems();
            if (items == null)
                return;

            items.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Current.Key.ToString()))
                {
                    keysToRemove.Add(enumerator.Current.Key.ToString());
                }
            }

            foreach (string key in keysToRemove)
            {
                items.Remove(key);
            }
        }

        public void Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return;

            if (data != null)
            {
                if (items.ContainsKey(key))
                    items[key] = data;
                else
                    items.Add(key, data);
            }
        }
    }
}
