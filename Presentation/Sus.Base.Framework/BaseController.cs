using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Framework
{
    [Authorize]
    public class BaseController:Controller
    {
        string _host = "";
        public BaseController()
        {
            _host = RouteData.Values["Host"]?.ToString();
            Console.WriteLine(_host);
        }
    }
}
