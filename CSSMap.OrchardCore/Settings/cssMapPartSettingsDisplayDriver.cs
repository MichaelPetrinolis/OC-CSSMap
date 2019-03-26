using System;
using System.Linq;
using System.Threading.Tasks;
using CSSMap.OrchardCore.Models;
using CSSMap.OrchardCore.Settings;
using CSSMap.OrchardCore.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace cssMap.OrchardCore.Settings
{
    public class cssMapPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        private readonly IStringLocalizer<cssMapPartSettingsDisplayDriver> T;

        public cssMapPartSettingsDisplayDriver(IStringLocalizer<cssMapPartSettingsDisplayDriver> localizer)
        {
            T = localizer;
        }

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(cssMapPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<cssMapPartSettingsViewModel>("cssMapPartSettings_Edit", model =>
            {
                model.cssMapPartSettings = contentTypePartDefinition.GetSettings<cssMapPartSettings>();
                model.Map = model.cssMapPartSettings.Map;
                model.Markup = model.cssMapPartSettings.Markup;
                model.Sizes = model.cssMapPartSettings.Sizes == null ? null : string.Join(",", model.cssMapPartSettings.Sizes);
                model.StylesheetName = model.cssMapPartSettings.StylesheetName;
                model.StylesheetUrl = model.cssMapPartSettings.StylesheetUrl;

            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(cssMapPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new cssMapPartSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix,
                m => m.Map,
                m => m.Markup,
                m => m.Sizes,
                m => m.StylesheetName,
                m => m.StylesheetUrl);


            int[] sizes = new int[0];

            if (!String.IsNullOrEmpty(model.Sizes))
            {
                sizes = model.Sizes.Split(',').Select(c => Convert.ToInt32(c)).Where(i => i > 0).ToArray();
                model.Sizes = string.Join(",", sizes);
            }

            if (string.IsNullOrWhiteSpace(model.Map))
            {
                context.Updater.ModelState.AddModelError(nameof(model.Map), T["You must set an Id for the map"]);
            }

            if (string.IsNullOrEmpty(model.Sizes))
            {
                context.Updater.ModelState.AddModelError(nameof(model.Sizes), T["You must provide a list of sizes the map supports."]);
            }

            if (string.IsNullOrEmpty(string.Concat(model.StylesheetName, model.StylesheetUrl)))
            {
                context.Updater.ModelState.AddModelError(nameof(model.Sizes), T["You must provide a resource name or a url to load the stylesheet of the map. If both defined, the resource name is used"]);
            }

            if (context.Updater.ModelState.ValidationState == ModelValidationState.Valid)
            {
                model.cssMapPartSettings = new cssMapPartSettings()
                {
                    Map = model.Map,
                    Markup = model.Markup,
                    Sizes = sizes,
                    StylesheetName = model.StylesheetName,
                    StylesheetUrl = string.IsNullOrWhiteSpace(model.StylesheetUrl) ? null : new Microsoft.AspNetCore.Http.PathString(model.StylesheetUrl)
                };
                context.Builder.WithSettings(model.cssMapPartSettings);
            }
            return Edit(contentTypePartDefinition, context.Updater);
        }

    }
}