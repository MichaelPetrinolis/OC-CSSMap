using CSSMap.OrchardCore.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Cache;
using OrchardCore.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cssMap.OrchardCore.Handlers
{
    public class cssMapPartHandler : ContentPartHandler<cssMapPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly ITagCache _tagCache;

        public cssMapPartHandler(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
        }

        public override Task CreatingAsync(CreateContentContext context, cssMapPart instance)
        {
            var settings = GetSettings(instance);
            instance.Markup = settings.Markup;
            instance.Apply();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the pattern from the AutoroutePartSettings property for its type
        /// </summary>
        private cssMapPartSettings GetSettings(cssMapPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(cssMapPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<cssMapPartSettings>();
        }

    }
}