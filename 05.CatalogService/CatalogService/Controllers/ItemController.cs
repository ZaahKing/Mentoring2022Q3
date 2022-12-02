using CatalogService.Interfases;
using CatalogService.Models.CategoryModels;
using CatalogService.Models.ItemModels;
using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemService itemService;

    public ItemController(IItemService itemService)
    {
        this.itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }

    [HttpGet]
    public async Task<IEnumerable<Item>> Get([FromQuery] int page, [FromQuery] int count)
    {
        var skipCount = (page - 1) * count;
        return await itemService.GetItemsAsync(skipCount, count);
    }

    [HttpGet("{id}")]
    public async Task<Item> GetItem([FromRoute]int id)
    {
        return await itemService.GetAsync(id);
    }

    [HttpPost]
    public async Task<Item> Post([FromBody] AddItemModel item)
    {
        return await itemService.AddAsync(item);
    }

    [HttpPut("{id}")]
    public async Task<Item> Put(int id, [FromBody] UpdateItemModel request)
    {
        return await itemService.UpdateAsync(new ItemEntity { Id = id, Name = request.Name, CategoryId = request.CategoryId });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await itemService.DeleteAsync(id);
        return NoContent();
    }
}
