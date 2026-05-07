using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodX.API.Data;
using WoodX.API.DTOs;
using WoodX.API.Models;
using WoodX.API.Services;

namespace WoodX.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, IJwtService jwt) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "E-posta veya şifre hatalı" });

        return Ok(BuildResponse(user));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await db.Users.AnyAsync(u => u.Email == dto.Email))
            return Conflict(new { message = "Bu e-posta zaten kayıtlı" });

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "customer",
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Ok(BuildResponse(user));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = GetUserId();
        var user = await db.Users.FindAsync(userId);
        if (user is null) return NotFound();

        return Ok(new UserDto(user.Id, user.Name, user.Email, user.Role));
    }

    private int GetUserId()
    {
        var val = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? User.FindFirst("sub")?.Value;
        return int.TryParse(val, out var id) ? id : 0;
    }

    private AuthResponseDto BuildResponse(User user) =>
        new(new UserDto(user.Id, user.Name, user.Email, user.Role), jwt.GenerateToken(user));
}
