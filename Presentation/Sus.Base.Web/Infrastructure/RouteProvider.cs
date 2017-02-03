using Sus.Base.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Sus.Base.Web.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return 1;
            }
        }

        public void RegisterRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("Login",
                "Login",
                new { controller = "Login", action = "Login" });
        }
    }
}
