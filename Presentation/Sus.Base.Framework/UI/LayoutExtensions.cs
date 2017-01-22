using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Sus.Base.Core.Infrastructure;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Sus.Base.Core.Infrastructure.DependencyManagement;

namespace Sus.Base.Framework.UI
{
    public static class LayoutExtensions
    {
        public static void AddTitleParts(this IHtmlHelper html, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddTitleParts(part);
        }
        public static void AppendTitleParts(this IHtmlHelper html, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendTitleParts(part);
        }
        public static HtmlString AppTitle(this IHtmlHelper html, bool addDefaultTitle, string part = "")
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            html.AppendTitleParts(part);
            return new HtmlString(html.Encode(pageHeadBuilder.GenerateTitle(addDefaultTitle)));
        }
        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddScriptParts(this IHtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AddScriptParts(html, ResourceLocation.PageScript, part, excludeFromBundle);
        }
        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddScriptParts(this IHtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddScriptParts(location, part, excludeFromBundle);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendScriptParts(this IHtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AppendScriptParts(html, ResourceLocation.PageScript, part, excludeFromBundle);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendScriptParts(this IHtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendScriptParts(location, part, excludeFromBundle);
        }
        /// <summary>
        /// Generate all script parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="urlHelper">URL Helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="bundleFiles">A value indicating whether to bundle script elements</param>
        /// <returns>Generated string</returns>
        public static HtmlString Scripts(this IHtmlHelper html, IUrlHelper urlHelper,
            ResourceLocation location, bool? bundleFiles = null)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            return new HtmlString(pageHeadBuilder.GenerateScripts(urlHelper, location, bundleFiles));
        }
        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        public static void AddCssFileParts(this IHtmlHelper html, string part)
        {
            AddCssFileParts(html, ResourceLocation.PageStyle, part);
        }
        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        public static void AddCssFileParts(this IHtmlHelper html, ResourceLocation location, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddCssFileParts(location, part);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        public static void AppendCssFileParts(this IHtmlHelper html, string part)
        {
            AppendCssFileParts(html, ResourceLocation.PageStyle, part);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        public static void AppendCssFileParts(this IHtmlHelper html, ResourceLocation location, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendCssFileParts(location, part);
        }
        /// <summary>
        /// Generate all CSS parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="urlHelper">URL Helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="bundleFiles">A value indicating whether to bundle script elements</param>
        /// <returns>Generated string</returns>
        public static HtmlString CssFiles(this IHtmlHelper html, IUrlHelper urlHelper,
            ResourceLocation location, bool? bundleFiles = null)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            return new HtmlString(pageHeadBuilder.GenerateCssFiles(urlHelper, location, bundleFiles));
        }
        public static void SetBodyClass(this IHtmlHelper html, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.SetBodyClass(part);
        }
        public static void AddBodyClass(this IHtmlHelper html, string part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddBodyClass(part);
        }
        public static void AddBodyClass(this IHtmlHelper html, List<string> part)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddBodyClass(part);
        }
        public static string BodyClass(this IHtmlHelper html)
        {
            var pageHeadBuilder = StaticResolver.Resolve<IPageHeadBuilder>();
            return pageHeadBuilder.GenerateBodyClass();
        }
    }
}
