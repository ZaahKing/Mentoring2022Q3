using CatalogService.Interfases;
using CatalogService.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryService.GetAsync();
        }

        [HttpPost]
        public async Task<Category> Post([FromBody] AddCategoryModel request)
        {
            return await categoryService.AddAsync(request);
        }

        [HttpPut("{id}")]
        public async Task<Category> Put(int id, [FromBody] UpdateCategoryModel request)
        {
            return await categoryService.UpdateAsync(new Category { Id = id, Name = request.Name, Desacription = request.Desacription});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
