using CSSMap.OrchardCore.Models;
using CSSMap.OrchardCore.Settings;
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

        public cssMapPartHandler(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
        }

        public override Task InitializingAsync(InitializingContentContext context, cssMapPart part)
        {
            var settings = GetcssMapPartSettings(part);
            part.Markup = settings.Markup;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the pattern from the AutoroutePartSettings property for its type
        /// </summary>
        private cssMapPartSettings GetcssMapPartSettings(cssMapPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(cssMapPart), StringComparison.Ordinal));
            return contentTypePartDefinition.GetSettings<cssMapPartSettings>();
        }

    }
}