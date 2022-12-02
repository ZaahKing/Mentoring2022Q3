using CatalogService.Models.ItemModels;

namespace CatalogService.Interfases;

public interface IItemRepository
{
    Task<IEnumerable<ItemEntity>> GetAsync();
    Task<ItemEntity> GetByIdAsync(int id);
    Task<ItemEntity> AddAsync(AddItemModel newItem);
    Task<ItemEntity> UpdateAsync(ItemEntity item);
    Task DeleteAsync(int id);
}
