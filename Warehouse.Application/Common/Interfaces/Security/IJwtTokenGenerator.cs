using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities.Users;

namespace Warehouse.Application.Common.Interfaces.Security;

public interface IJwtTokenGenerator<TUser> where TUser : User
{
    string GenerateToken(TUser user);

    string GenerateRefreshToken();

    string GenerateTokenWithGoogle(GoogleUserInfo user, DateTime? expirationTime = null);
}