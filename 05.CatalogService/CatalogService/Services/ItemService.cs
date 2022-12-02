using CatalogService.Controllers;
using CatalogService.Interfases;
using CatalogService.Models.ItemModels;
using CatalogService.Repositories;

namespace CatalogService.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository itemRepository;
    private readonly ICategoryRepository categoryRepository;

    public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository)
    {
        this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<IEnumerable<Item>> GetItemsAsync(int skipCount, int takeCount)
    {
        var items = await itemRepository.GetAsync();
        return items
            .Skip(skipCount)
            .Take(takeCount)
            .Select(i => ConvertToModel(i).Result);
    }

    public async Task<Item> GetAsync(int id)
    {
        var itemEntity = await itemRepository.GetByIdAsync(id);
        return await ConvertToModel(itemEntity);
    }

    public async Task<Item> AddAsync(AddItemModel newItem)
    {
        var item = await itemRepository.AddAsync(newItem);
        return await ConvertToModel(item);
    }

    public async Task<Item> UpdateAsync(ItemEntity item)
    {
        var updatedItem = await itemRepository.UpdateAsync(item);
        return await ConvertToModel(updatedItem);
    }

    public async Task DeleteAsync(int id)
    {
        await itemRepository.DeleteAsync(id);
    }

    private async Task<Item> ConvertToModel(ItemEntity item) =>
        new Item
        {
            Id = item.Id,
            Name = item.Name,
            Category = await categoryRepository.GetByIdAsync(item.CategoryId),
        };
}
