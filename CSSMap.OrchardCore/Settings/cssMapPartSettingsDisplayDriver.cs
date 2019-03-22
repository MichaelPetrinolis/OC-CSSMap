using System;
using System.Threading.Tasks;
using CSSMap.OrchardCore.Models;
using CSSMap.OrchardCore.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace cssMap.OrchardCore.Settings
{
    public class cssMapPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(cssMapPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<cssMapPartSettingsViewModel>("cssMapPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.Settings.ToObject<cssMapPartSettings>();

                model.Map = settings.Map;
                model.Markup = settings.Markup;
                model.Sizes = settings.Sizes == null ? null : string.Join(",", settings?.Sizes);

                model.cssMapPartSettings = settings;
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
                m => m.Sizes);

            context.Builder.WithSetting(nameof(cssMapPartSettings.Map), model.Map);
            context.Builder.WithSetting(nameof(cssMapPartSettings.Markup), model.Markup);
            context.Builder.WithSetting(nameof(cssMapPartSettings.Sizes), model.Sizes);

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}