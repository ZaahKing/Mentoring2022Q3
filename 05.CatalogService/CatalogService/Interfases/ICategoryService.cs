using CatalogService.Models.CategoryModels;

namespace CatalogService.Interfases;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAsync();
    Task<Category> AddAsync(AddCategoryModel newCategory);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(int id);
}
