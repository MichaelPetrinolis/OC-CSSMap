using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSSMap.OrchardCore.Settings
{
    public class cssMapPartSettings
    {
        public int[] Sizes { get; set; }
        public string Markup { get; set; }
        public string Map { get; set; }
        public string StylesheetName { get; set; }
        public PathString? StylesheetUrl { get; set; }
    }
}
