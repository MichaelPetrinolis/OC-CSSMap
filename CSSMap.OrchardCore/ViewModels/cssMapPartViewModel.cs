using Microsoft.AspNetCore.Mvc.ModelBinding;
using CSSMap.OrchardCore.Models;

namespace CSSMap.OrchardCore.ViewModels
{
    public class cssMapPartViewModel
    {
        public string Id { get; set; }
        public int Size { get; set; }
        public string Options { get; set; }
        public string Markup { get; set; }
        public string AdditionalMarkup { get; set; }

        [BindNever]
        public cssMapPart cssMapPart { get; set; }

        [BindNever]
        public cssMapPartSettings Settings { get; set; }
    }
}
