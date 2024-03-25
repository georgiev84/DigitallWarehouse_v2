using System.Security.Claims;
using Warehouse.Domain.Entities.Users;

namespace Warehouse.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    string RevokeToken(User user);
}
