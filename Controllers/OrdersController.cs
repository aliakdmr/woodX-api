using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodX.API.Data;
using WoodX.API.DTOs;
using WoodX.API.Models;

namespace WoodX.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController(AppDbContext db) : ControllerBase
{
    private int GetUserId()
    {
        var val = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? User.FindFirst("sub")?.Value;
        return int.TryParse(val, out var id) ? id : 0;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var userId = GetUserId();
        if (userId == 0) return Unauthorized();

        var order = new Order
        {
            UserId = userId,
            Status = "pending",
            PaymentMethod = dto.PaymentMethod,
            Total = dto.Total,
            ShippingAddress = new ShippingAddress
            {
                FullName = dto.ShippingAddress.FullName,
                Email = dto.ShippingAddress.Email,
                Address = dto.ShippingAddress.Address,
                City = dto.ShippingAddress.City,
                PostalCode = dto.ShippingAddress.PostalCode,
                Country = dto.ShippingAddress.Country,
                Phone = dto.ShippingAddress.Phone ?? "",
            },
            Items = dto.Items.Select(i => new OrderItem
            {
                ProductId = i.Id,
                ProductName = i.Name,
                ProductImage = i.Image,
                Price = i.Price,
                Quantity = i.Quantity,
            }).ToList(),
        };

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return Ok(order);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = GetUserId();
        var orders = await db.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var order = await db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

        return order is null
            ? NotFound(new { message = "Sipariş bulunamadı" })
            : Ok(order);
    }

    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = GetUserId();
        var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

        if (order is null) return NotFound(new { message = "Sipariş bulunamadı" });
        if (order.Status is "delivered" or "cancelled")
            return BadRequest(new { message = "Bu sipariş iptal edilemez" });

        order.Status = "cancelled";
        await db.SaveChangesAsync();
        return Ok(order);
    }
}
