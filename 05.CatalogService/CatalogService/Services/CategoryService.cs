using CatalogService.Interfases;
using CatalogService.Models.CategoryModels;

namespace CatalogService.Services;

internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository catalogRepository;
    private readonly IItemRepository itemRepository;

    public CategoryService(ICategoryRepository catalogRepository, IItemRepository itemRepository)
    {
        this.catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<IEnumerable<Category>> GetAsync()
    {
        return await catalogRepository.GetAllAsync();
    }

    public async Task<Category> AddAsync(AddCategoryModel newCategory)
    {
        return await catalogRepository.AddAsync(newCategory.Name, newCategory.Desacription);
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        return await catalogRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var items = await itemRepository.GetAsync();
        foreach (var item in items.Where(i => i.CategoryId == id))
        {
            await itemRepository.DeleteAsync(item.Id);
        }

        await catalogRepository.DeleteAsync(id);
    }
}
