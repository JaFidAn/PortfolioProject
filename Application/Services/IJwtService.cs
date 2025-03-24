using Domain.Entities;

namespace Application.Services;

public interface IJwtService
{
    Task<string> GenerateToken(AppUser user);
}
