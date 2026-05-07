using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodX.API.Data;

namespace WoodX.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] string? search)
    {
        var query = db.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category == category);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(q) ||
                p.Description.ToLower().Contains(q));
        }

        return Ok(await query.ToListAsync());
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured() =>
        Ok(await db.Products.Where(p => p.Featured).ToListAsync());

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories() =>
        Ok(await db.Products.Select(p => p.Category).Distinct().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await db.Products.FindAsync(id);
        return product is null
            ? NotFound(new { message = "Ürün bulunamadı" })
            : Ok(product);
    }
}
