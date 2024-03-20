using Warehouse.Domain.Entities.Users;

namespace Warehouse.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
