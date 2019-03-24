﻿using CSSMap.OrchardCore.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace CSSMap.OrchardCore
{
    public class Migrations : DataMigration
    {
        private readonly IRecipeMigrator _recipeMigrator;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IRecipeMigrator recipeMigrator, IContentDefinitionManager contentDefinitionManager)
        {
            _recipeMigrator = recipeMigrator;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(cssMapPart), builder => builder
                .Attachable()
                .WithDescription("Provides a cssmap part to create map widgets."));

            await _recipeMigrator.ExecuteAsync("migration.recipe.json", this);
            return await Task.FromResult(1);
        }

        //public async Task<int> UpdateFrom1Async()
        //{
        //    _contentDefinitionManager.AlterPartDefinition(nameof(cssMapPart), builder => builder
        //        .Attachable()
        //        .WithDescription("Provides a cssmap part to create map widgets."));
        //    return await Task.FromResult(2);
        //}
    }
}