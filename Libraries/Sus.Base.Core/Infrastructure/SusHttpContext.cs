using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Infrastructure
{
    public static class SusHttpContext
    {
        public static IServiceProvider ServiceProvider;
        public static IHttpContextAccessor _httpContextAccessor;
        static SusHttpContext()
        { }

        public static void Config(IHttpContextAccessor httpcontextAccessor)
        {
            _httpContextAccessor = httpcontextAccessor;
        }
        public static HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
                //object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                //HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                //return context;
            }
        }
    }
}
