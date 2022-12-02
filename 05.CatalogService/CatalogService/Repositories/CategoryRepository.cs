using System.Collections.ObjectModel;
using CatalogService.Interfases;
using CatalogService.Models;
using CatalogService.Models.CategoryModels;
using CatalogService.Models.ItemModels;

namespace CatalogService.Repositories;

internal class CategoryRepository : ICategoryRepository
{
    private static readonly IList<Category> categories = DataFactory.GetInitialsCategories();

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        return Task<IEnumerable<Category>>.Run(() => new ReadOnlyCollection<Category>(categories).AsEnumerable());
    }

    public Task<Category> GetByIdAsync(int id)
    {
        return Task.Run(() => categories.SingleOrDefault(x => x.Id == id));
    }

    public Task<Category> AddAsync(string name, string desacription)
    {
        return Task.Run(() =>
        {
            int maxId = categories.Max(c => c.Id);
            maxId += 1;
            categories.Add(new Category { Id = maxId, Name = name, Desacription = desacription });
            return categories.Single(c => c.Id == maxId);
        });
    }

    public Task<Category> UpdateAsync(Category category)
    {
        return Task.Run(() =>
        {
            var targetCategory = categories.Single(c => c.Id == category.Id);
            targetCategory.Name = category.Name;
            targetCategory.Desacription = category.Desacription;
            return targetCategory;
        });
    }

    public Task DeleteAsync(int id)
    {
        return Task.Run(() =>
        {
            var targetCategory = categories.Single(c => c.Id == id);
            categories.Remove(targetCategory);
        });
    }
}
