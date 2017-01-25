using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Sus.Base.Core.Infrastructure.DependencyManagement;

namespace Sus.Base.Core
{
    public class CommonHelper
    {
        public static IHostingEnvironment _env { get; set; }
        public static string MapPath(string path)
        {
            //_env = StaticResolver.Resolve<IHostingEnvironment>();
            string baseDirectory = _env.ContentRootPath;
            string webDir = _env.WebRootPath;
            //string basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
            path = path.Replace("~/", "").TrimStart('/').Replace("/", "");
            return Path.Combine(baseDirectory, path);
        }


    }
}
