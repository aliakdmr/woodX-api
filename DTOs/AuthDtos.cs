namespace WoodX.API.DTOs;

public record LoginDto(string Email, string Password);
public record RegisterDto(string Name, string Email, string Password);
public record UserDto(int Id, string Name, string Email, string Role);
public record AuthResponseDto(UserDto User, string Token);
