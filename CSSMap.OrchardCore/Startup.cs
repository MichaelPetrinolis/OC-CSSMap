using cssMap.OrchardCore.Handlers;
using cssMap.OrchardCore.Settings;
using CSSMap.OrchardCore.Drivers;
using CSSMap.OrchardCore.Models;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace CSSMap.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IContentPartDisplayDriver, cssMapPartDisplayDriver>();
            services.AddSingleton<ContentPart, cssMapPart>();
            services.AddScoped<IContentPartHandler, cssMapPartHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, cssMapPartSettingsDisplayDriver>();

            services.AddScoped<IResourceManifestProvider, ResourceManifest>();
            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}