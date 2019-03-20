using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement;
using System;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace CSSMap.OrchardCore
{
    public class Migrations : DataMigration
    {
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(IRecipeMigrator recipeMigrator)
        {
            _recipeMigrator = recipeMigrator;
        }

        public async Task<int> CreateAsync()
        {
            await _recipeMigrator.ExecuteAsync("migration.recipe.json", this);
            return 1;
        }
    }
}