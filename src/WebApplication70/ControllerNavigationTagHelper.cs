using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace WebApplication70
{
    [OutputElementHint("ul")]
    public class ControllerNavigationTagHelper : TagHelper
    {
        public ControllerNavigationTagHelper(IUrlHelper urlHelper)
        {
            UrlHelper = urlHelper;
        }

        private IUrlHelper UrlHelper { get; }

        public Type ControllerType { get; set; }

        public string Exclude { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";

            var actionNames = ControllerType.GetTypeInfo().DeclaredMethods
                .Where(methodInfo => methodInfo.IsPublic)
                .Select(methodInfo => methodInfo.Name);

            var controllerName = ControllerType.Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            }

            foreach (var name in actionNames)
            {
                if (!string.Equals(name, Exclude, StringComparison.OrdinalIgnoreCase))
                {
                    var displayName =
                        string.Equals(name, "Index", StringComparison.OrdinalIgnoreCase) ? controllerName : name;
                    output.PostContent.Append($"<li><a href='{UrlHelper.Action(name, controllerName)}'>{displayName}</a></li>");
                }
            }
        }
    }
}