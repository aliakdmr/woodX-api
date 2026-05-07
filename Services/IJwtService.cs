using WoodX.API.Models;

namespace WoodX.API.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
