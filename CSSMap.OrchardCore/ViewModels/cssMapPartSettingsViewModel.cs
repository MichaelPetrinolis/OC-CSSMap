using CSSMap.OrchardCore.Models;

namespace CSSMap.OrchardCore.ViewModels
{
    public class cssMapPartSettingsViewModel
    {
        public string Sizes { get; set; }
        public string Markup { get; set; }
        public string Map {get; set; }
        public cssMapPartSettings cssMapPartSettings { get; set; }
    }
}
