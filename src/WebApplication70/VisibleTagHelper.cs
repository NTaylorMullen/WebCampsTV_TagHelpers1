using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace WebApplication70
{
    [TargetElement(Attributes = "asp-visible")]
    public class VisibleTagHelper : TagHelper
    {
        public override int Order
        {
            get
            {
                return int.MaxValue;
            }
        }

        public bool AspVisible { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!AspVisible)
            {
                output.SuppressOutput();
            }
        }
    }
}