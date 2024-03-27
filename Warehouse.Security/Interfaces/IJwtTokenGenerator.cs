using Warehouse.Domain.Entities.Users;
using Warehouse.Security.Models;

namespace Warehouse.Security.Interfaces;

public interface IJwtTokenGenerator<TUser> where TUser : User
{
    string GenerateToken(TUser user);

    string GenerateRefreshToken();
    string GenerateTokenWithGoogle(GoogleUserInfo user, DateTime? expirationTime = null);
}