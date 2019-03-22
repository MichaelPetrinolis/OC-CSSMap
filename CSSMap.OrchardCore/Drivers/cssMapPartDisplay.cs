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
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSSMap.OrchardCore.Drivers
{
    public class cssMapPartDisplay : ContentPartDisplayDriver<cssMapPart>
    {
        public static char[] InvalidCharactersForPath = ":?#[]@!$&'()*+,.;=<>\\|%".ToCharArray();
        public const int MaxPathLength = 1024;

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly YesSql.ISession _session;
        private readonly IStringLocalizer<cssMapPartDisplay> T;

        public cssMapPartDisplay(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            YesSql.ISession session,
            IStringLocalizer<cssMapPartDisplay> localizer
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _session = session;
            T = localizer;
        }

        public override IDisplayResult Edit(cssMapPart part)
        {
            return Initialize<cssMapPartViewModel>("cssMapPart_Edit", async model =>
            {
                model.AdditionalMarkup = part.AdditionalMarkup;
                model.cssMapPart = part;
                model.Id = part.Id;
                model.Markup = part.Markup;
                model.Options = part.Options;
                model.Size = part.Size;

                model.Settings = GetSettings(part);
            });
        }

        private cssMapPartSettings GetSettings(cssMapPart autoroutePart)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(autoroutePart.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(cssMapPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<cssMapPartSettings>();
        }

        public override async Task<IDisplayResult> UpdateAsync(cssMapPart model, IUpdateModel updater)
        {
            //var viewModel = new cssMapPartViewModel();

            //await updater.TryUpdateModelAsync(viewModel, Prefix, t => t.Path, t => t.UpdatePath);

            //var settings = GetSettings(model);

            //if (settings.AllowCustomPath)
            //{
            //    model.Path = viewModel.Path;
            //}

            //if (settings.AllowUpdatePath && viewModel.UpdatePath)
            //{
            //    // Make it empty to force a regeneration
            //    model.Path = "";
            //}

            //var httpContext = _httpContextAccessor.HttpContext;

            //if (httpContext != null && await _authorizationService.AuthorizeAsync(httpContext.User, Permissions.SetHomepage))
            //{
            //    await updater.TryUpdateModelAsync(model, Prefix, t => t.SetHomepage);
            //}

            await ValidateAsync(model, updater);

            return Edit(model);
        }

        private async Task ValidateAsync(cssMapPart autoroute, IUpdateModel updater)
        {
            //if (autoroute.Path?.IndexOfAny(InvalidCharactersForPath) > -1 || autoroute.Path?.IndexOf(' ') > -1)
            //{
            //    var invalidCharactersForMessage = string.Join(", ", InvalidCharactersForPath.Select(c => $"\"{c}\""));
            //    updater.ModelState.AddModelError(Prefix, nameof(autoroute.Path), T["Please do not use any of the following characters in your permalink: {0}. No spaces are allowed (please use dashes or underscores instead).", invalidCharactersForMessage]);
            //}

            //if (autoroute.Path?.Length > MaxPathLength)
            //{
            //    updater.ModelState.AddModelError(Prefix, nameof(autoroute.Path), T["Your permalink is too long. The permalink can only be up to {0} characters.", MaxPathLength]);
            //}

            //if (autoroute.Path != null && (await _session.QueryIndex<AutoroutePartIndex>(o => o.Path == autoroute.Path && o.ContentItemId != autoroute.ContentItem.ContentItemId).CountAsync()) > 0)
            //{
            //    updater.ModelState.AddModelError(Prefix, nameof(autoroute.Path), T["Your permalink is already in use."]);
            //}
        }
    }
}
