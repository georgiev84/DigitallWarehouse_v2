using Warehouse.Domain.Entities.Users;

namespace Warehouse.Security.Interfaces;

public interface IJwtTokenGenerator<TUser> where TUser : User
{
    string GenerateToken(TUser user);

    string GenerateRefreshToken();
}