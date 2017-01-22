using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Framework.Mvc.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(IRouteBuilder routes);

        int Priority { get; }
    }
}
