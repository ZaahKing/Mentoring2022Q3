using CatalogService.Models.CategoryModels;

namespace CatalogService.Interfases;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int categoryId);
    Task<Category> AddAsync(string name, string desacription);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(int id);
}
