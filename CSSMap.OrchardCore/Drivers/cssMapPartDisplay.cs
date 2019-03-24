using CSSMap.OrchardCore.Models;
using CSSMap.OrchardCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.Models;

namespace CSSMap.OrchardCore.Drivers
{
    public class cssMapPartDisplay : ContentPartDisplayDriver<cssMapPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<cssMapPartDisplay> T;

        public cssMapPartDisplay(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<cssMapPartDisplay> localizer
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            T = localizer;
        }

        public override IDisplayResult Display(cssMapPart part, BuildPartDisplayContext context)
        {
            return Initialize<cssMapPartViewModel>("cssMapPart", m =>
            {
                m.cssMapPart = part;
                m.Settings = GetSettings(part);
                m.AdditionalMarkup = part.AdditionalMarkup;
                m.Id = part.Id;
                m.Markup = part.Markup;
                m.Options = part.Options;
                m.Size = part.Size;
            })
            .Location("Detail", "Content:5");

        }

        public override IDisplayResult Edit(cssMapPart part)
        {
            return Initialize<cssMapPartViewModel>("cssMapPart_Edit", model =>
            {
                model.Settings = GetSettings(part);

                model.AdditionalMarkup = part.AdditionalMarkup;
                model.cssMapPart = part;
                model.Id = part.Id;
                model.Markup = string.IsNullOrWhiteSpace(part.Markup) ? model.Settings.Markup : part.Markup;
                model.Options = part.Options;
                model.Size = part.Size;

            });
        }

        private cssMapPartSettings GetSettings(cssMapPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(cssMapPart), StringComparison.Ordinal));
            Newtonsoft.Json.Linq.JToken settings;
            if (contentTypePartDefinition.Settings.TryGetValue(nameof(cssMapPartSettings), out settings))
                return settings.ToObject<cssMapPartSettings>();
            else
                return new cssMapPartSettings();
        }

        public override async Task<IDisplayResult> UpdateAsync(cssMapPart model, IUpdateModel updater)
        {
            var viewModel = new cssMapPartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix, t => t.Id, t => t.Markup, t => t.Options, t => t.Size);

            model.Id = viewModel.Id;
            model.Markup = viewModel.Markup;
            model.Options = viewModel.Options;
            model.Size = viewModel.Size;

            await ValidateAsync(model, updater);

            return Edit(model);
        }

        private Task ValidateAsync(cssMapPart model, IUpdateModel updater)
        {
            var settings = GetSettings(model);

            //if (string.IsNullOrWhiteSpace(model.Id))
            //{
            //    updater.ModelState.AddModelError(Prefix, nameof(model.Id), T["You must set an Id for the map"]);
            //}

            if (settings.Sizes != null)
            {
                if (!settings.Sizes.Contains(model.Size))
                {
                    updater.ModelState.AddModelError(Prefix, nameof(model.Id), T["There is no size {0} px defined.", model.Size]);
                }
            }

            return Task.CompletedTask;
        }
    }
}
