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
                .SetVersion("5.5.4")
                ;

            manifest
                .DefineStyle("cssmap-continents")
                .SetUrl("~/CSSMap.OrchardCore/Styles/cssmap-continents.min.css", "~/CSSMap.OrchardCore/Styles/cssmap-continents.css")
                .SetVersion("5.5.4")
                ;
        }
    }
}
