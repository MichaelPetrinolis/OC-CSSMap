using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "CSSMap Widget",
    Author = "Michael Petrinolis",
    Website = "https://orchardproject.net",
    Version = "0.0.1",
    Description = "Integrate CSSMap Plugin to OrchardCore",
    Dependencies = new[] { "OrchardCore.Fields","OrchardCore.Resources","OrchardCore.Widgets" },
    Category = "Content Management"
)]
