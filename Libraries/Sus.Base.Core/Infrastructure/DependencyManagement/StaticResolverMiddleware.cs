using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Infrastructure.DependencyManagement
{
    public class StaticResolverMiddleware
    {
        private readonly RequestDelegate _next;
        public StaticResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            EngineContext.Current.CreateScope();
            await _next.Invoke(context);
        }
    }
}
