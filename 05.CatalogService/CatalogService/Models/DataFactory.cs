using CatalogService.Models.CategoryModels;
using CatalogService.Models.ItemModels;

namespace CatalogService.Models;

internal static class DataFactory
{
    public static IList<Category> GetInitialsCategories() =>
        new List<Category>
        {
            new Category { Id = 1, Name = "Processors", Desacription = "About processors" },
            new Category { Id = 2, Name = "RAM", Desacription = "About RAM" },
            new Category { Id = 3, Name = "Graphic cards", Desacription = "About graphic cards" },
            new Category { Id = 4, Name = "Monitors", Desacription = "About monitors" },
            new Category { Id = 5, Name = "Cases", Desacription = "About cases" },
            new Category { Id = 6, Name = "Keybords", Desacription = "About keyboards" },
            new Category { Id = 7, Name = "Laptops", Desacription = "About laptops" },
        };

    public static IList<ItemEntity> GetInitialsItems() =>
        new List<ItemEntity>
        {
            new ItemEntity { Id = 1, Name = "Processor 1", CategoryId = 1 },
            new ItemEntity { Id = 2, Name = "Processor 2", CategoryId = 1 },
            new ItemEntity { Id = 3, Name = "Processor 3", CategoryId = 1 },
            new ItemEntity { Id = 4, Name = "Processor 4", CategoryId = 1 },
            new ItemEntity { Id = 5, Name = "Processor 5", CategoryId = 1 },
            new ItemEntity { Id = 6, Name = "Processor 6", CategoryId = 1 },
            new ItemEntity { Id = 7, Name = "Processor 7", CategoryId = 1 },
            new ItemEntity { Id = 8, Name = "Processor 8", CategoryId = 1 },
            new ItemEntity { Id = 9, Name = "Processor 9", CategoryId = 1 },
            new ItemEntity { Id = 10, Name = "GoodRAM", CategoryId = 2 },
            new ItemEntity { Id = 11, Name = "Hynix", CategoryId = 2 },
            new ItemEntity { Id = 12, Name = "RAM", CategoryId = 2 },
            new ItemEntity { Id = 13, Name = "GeForce 3060", CategoryId = 3 },
            new ItemEntity { Id = 14, Name = "GeForce 4060", CategoryId = 3 },
            new ItemEntity { Id = 15, Name = "Radeon 7950XTX", CategoryId = 3 },
            new ItemEntity { Id = 16, Name = "Samsung", CategoryId = 4 },
            new ItemEntity { Id = 17, Name = "LG", CategoryId = 4 },
            new ItemEntity { Id = 18, Name = "Philips", CategoryId = 4 },
            new ItemEntity { Id = 19, Name = "Case model 1", CategoryId = 5 },
            new ItemEntity { Id = 20, Name = "Case model 2", CategoryId = 5 },
            new ItemEntity { Id = 21, Name = "Case model 3", CategoryId = 5 },
            new ItemEntity { Id = 22, Name = "Case model 4", CategoryId = 5 },
            new ItemEntity { Id = 23, Name = "Case model 5", CategoryId = 5 },
            new ItemEntity { Id = 24, Name = "Keybord 1", CategoryId = 6 },
            new ItemEntity { Id = 25, Name = "Keybord 2", CategoryId = 6 },
            new ItemEntity { Id = 26, Name = "Keybord 3", CategoryId = 6 },
            new ItemEntity { Id = 27, Name = "Keybord 4", CategoryId = 6 },
            new ItemEntity { Id = 28, Name = "HP", CategoryId = 7 },
            new ItemEntity { Id = 29, Name = "Acer", CategoryId = 7 },
            new ItemEntity { Id = 30, Name = "Lenovo", CategoryId = 7 },
            new ItemEntity { Id = 31, Name = "MacAir", CategoryId = 7 },
            new ItemEntity { Id = 32, Name = "MacBook", CategoryId = 7 },
        };
}
