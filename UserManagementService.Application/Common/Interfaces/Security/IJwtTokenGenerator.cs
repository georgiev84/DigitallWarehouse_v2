using UserManagementService.Application.Models.Dto;
using UserManagementService.Domain.Entities.Users;

namespace UserManagementService.Application.Common.Interfaces.Security;

public interface IJwtTokenGenerator<TUser> where TUser : User
{
    string GenerateToken(TUser user);

    string GenerateRefreshToken();

    string GenerateTokenWithGoogle(GoogleUserInfo user, DateTime? expirationTime = null);
}