using CatalogService.Models.CategoryModels;

namespace CatalogService.Models.ItemModels;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
}
