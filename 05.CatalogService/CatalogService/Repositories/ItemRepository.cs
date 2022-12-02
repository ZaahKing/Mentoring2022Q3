using System.Collections.ObjectModel;
using CatalogService.Interfases;
using CatalogService.Models;
using CatalogService.Models.ItemModels;

namespace CatalogService.Repositories;

public class ItemRepository : IItemRepository
{
    private static readonly IList<ItemEntity> items = DataFactory.GetInitialsItems();

    public Task<IEnumerable<ItemEntity>> GetAsync()
    {
        return Task.Run(() => new ReadOnlyCollection<ItemEntity>(items).AsEnumerable());
    }

    public Task<ItemEntity> GetByIdAsync(int id)
    {
        return Task.Run(() => items.Single(x => x.Id == id));
    }

    public Task<ItemEntity> AddAsync(AddItemModel newItem)
    {
        return Task.Run(() =>
        {
            int maxId = items.Max(i => i.Id);
            maxId += 1;
            items.Add(new ItemEntity { Id = maxId, Name = newItem.Name, CategoryId = newItem.CategoryId });
            return items.Single(i => i.Id == maxId);
        });
    }

    public Task<ItemEntity> UpdateAsync(ItemEntity item)
    {
        return Task.Run(() =>
        {
            var targetItem = items.Single(i => i.Id == item.Id);
            targetItem.Name = item.Name;
            targetItem.CategoryId = item.CategoryId;
            return targetItem;
        });
    }

    public Task DeleteAsync(int id)
    {
        return Task.Run(() =>
        {
            var targetItem = items.Single(c => c.Id == id);
            items.Remove(targetItem);
        });
    }
}
