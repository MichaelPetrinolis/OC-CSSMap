using CSSMap.OrchardCore.Models;
using System.ComponentModel.DataAnnotations;

namespace CSSMap.OrchardCore.Settings
{
    public class cssMapPartSettingsViewModel
    {
        public string Sizes { get; set; }
        public string Markup { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Map class is required")]
        public string Map {get; set; }
        public string StylesheetName { get; set; }
        [Url(ErrorMessage = "Invalid url")]
        public string StylesheetUrl { get; set; }
        public cssMapPartSettings cssMapPartSettings { get; set; }
    }
}
