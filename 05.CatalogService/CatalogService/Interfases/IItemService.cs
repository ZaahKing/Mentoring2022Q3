using CatalogService.Models.ItemModels;

namespace CatalogService.Interfases;

public interface IItemService
{
    Task<IEnumerable<Item>> GetItemsAsync(int skipCount, int takeCount);
    Task<Item> GetAsync(int id);
    Task<Item> AddAsync(AddItemModel newItem);
    Task<Item> UpdateAsync(ItemEntity item);
    Task DeleteAsync(int id);
}
