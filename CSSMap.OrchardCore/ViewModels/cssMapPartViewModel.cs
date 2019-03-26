using Microsoft.AspNetCore.Mvc.ModelBinding;
using CSSMap.OrchardCore.Models;
using System.ComponentModel.DataAnnotations;
using CSSMap.OrchardCore.Settings;

namespace CSSMap.OrchardCore.ViewModels
{
    public class cssMapPartViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required")]
        public string Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The size must be greater that 0")]
        public int Size { get; set; }
        public string Options { get; set; }
        public string Markup { get; set; }
        public string AdditionalMarkup { get; set; }
        public string Map { get; set; }
        [BindNever]
        public cssMapPart cssMapPart { get; set; }
        [BindNever]
        public cssMapPartSettings Settings { get; set; }
    }
}
