using OrchardCore.ResourceManagement;

namespace CSSMap.OrchardCore
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(IResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest
                .DefineScript("cssmap")
                .SetDependencies("jQuery")
                .SetUrl("~/CSSMap.OrchardCore/Scripts/jquery.cssmap.min.js")
                .SetCdn("https://cssmapsplugin.com/5/jquery.cssmap.min.js")
                .SetVersion("5.5.4");

            manifest
                .DefineStyle("cssmap-africa")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-africa.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-africa.css")
                .SetVersion("5.5.3");

            manifest
                .DefineStyle("cssmap-australia")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-australia.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-australia.css")
                .SetVersion("5.5.3");

            manifest
                .DefineStyle("cssmap-continents")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-continents.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-continents.css")
                .SetVersion("5.5.4");

            manifest
                .DefineStyle("cssmap-europe")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-europe.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-europe.css")
                .SetVersion("5.5.4");

            manifest
                .DefineStyle("cssmap-poland")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-poland.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-poland.css")
                .SetVersion("5.5.3");

            manifest
                .DefineStyle("cssmap-south-america")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-south-america.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-south-america.css")
                .SetVersion("5.5.3");
        }
    }
}
