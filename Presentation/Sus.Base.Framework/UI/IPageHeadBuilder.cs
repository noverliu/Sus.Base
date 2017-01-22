using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Sus.Base.Framework.UI
{
    public interface IPageHeadBuilder
    {

        void AddTitleParts(string part);
        void AppendTitleParts(string part);
        string GenerateTitle(bool addDefaultTitle);

        void AddScriptParts(ResourceLocation location, string part, bool excludeFromBundle);
        void AppendScriptParts(ResourceLocation location, string part, bool excludeFromBundle);
        string GenerateScripts(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null);

        void AddCssFileParts(ResourceLocation location, string part);
        void AppendCssFileParts(ResourceLocation location, string part);
        string GenerateCssFiles(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null);
        void SetBodyClass(string css);
        void AddBodyClass(string css);
        void AddBodyClass(List<string> css);
        string GenerateBodyClass();
    }
}
