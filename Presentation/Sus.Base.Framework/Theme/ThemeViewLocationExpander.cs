using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace Sus.Base.Framework.Theme
{
    public class ThemeViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var theme = "custom";
            var newViewLocation = new[]
            {
                $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                $"/Themes/{theme}/Shared/{{0}}.cshtml",
            };
            viewLocations = newViewLocation.Concat(viewLocations);
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //context.Values[THEME_KEY] = context.ActionContext.HttpContext.GetTenant<AppTenant>()?.Theme;
        }
    }
}
