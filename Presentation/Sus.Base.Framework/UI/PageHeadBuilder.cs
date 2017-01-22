using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Sus.Base.Framework.UI
{
    public class PageHeadBuilder : IPageHeadBuilder
    {
        private readonly List<string> _bodyClass;
        private readonly Dictionary<ResourceLocation, List<string>> _cssParts;
        private readonly Dictionary<ResourceLocation, List<ScriptReferenceMeta>> _scriptParts;
        private readonly List<string> _titleParts;
        public PageHeadBuilder()
        {
            this._titleParts = new List<string>();
            this._cssParts = new Dictionary<ResourceLocation, List<string>>();
            this._scriptParts = new Dictionary<ResourceLocation, List<ScriptReferenceMeta>>();
            this._bodyClass = new List<string>();
        }
        
        public void AddCssFileParts(ResourceLocation location, string part)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;

            _cssParts[location].Add(part);
        }

        public void AddScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            _scriptParts[location].Add(new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }

        public void AddTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _titleParts.Add(part);
        }

        public void AppendCssFileParts(ResourceLocation location, string part)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;

            _cssParts[location].Insert(0, part);
        }

        public void AppendScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            _scriptParts[location].Insert(0, new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }

        public void AppendTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _titleParts.Insert(0, part);
        }

        public string GenerateCssFiles(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = default(bool?))
        {
            if (!_cssParts.ContainsKey(location) || _cssParts[location] == null)
                return "";

            //use only distinct rows
            var distinctParts = _cssParts[location].Distinct().ToList();
            if (distinctParts.Count == 0)
                return "";
            var result = new StringBuilder();
            foreach (var path in distinctParts)
            {
                result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlHelper.Content(path));
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        public string GenerateScripts(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = default(bool?))
        {
            if (!_scriptParts.ContainsKey(location) || _scriptParts[location] == null)
                return "";

            if (_scriptParts.Count == 0)
                return "";
            var result = new StringBuilder();
            foreach (var path in _scriptParts[location].Select(x => x.Part).Distinct())
            {
                result.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        public string GenerateTitle(bool addDefaultTitle)
        {
            var specificTitle = string.Join("-", _titleParts.AsEnumerable().Reverse().ToArray());
            return specificTitle;
        }
        public string GenerateBodyClass()
        {
            return string.Join(" ", _bodyClass.AsEnumerable().ToArray());
        }

        public void SetBodyClass(string css)
        {
            _bodyClass.Clear();
            AddBodyClass(css);
        }

        public void AddBodyClass(string css)
        {
            if (string.IsNullOrEmpty(css))
                return;
            _bodyClass.Add(css);
        }

        public void AddBodyClass(List<string> css)
        {
            _bodyClass.AddRange(css);
        }
    }
    class ScriptReferenceMeta
    {
        public bool ExcludeFromBundle { get; set; }

        public string Part { get; set; }
    }

}
