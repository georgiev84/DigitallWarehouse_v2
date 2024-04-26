namespace UserManagementService.Application.Common.Interfaces.Security;

public interface ITokenBlacklistService
{
    Task<bool> IsTokenBlacklisted(string tokenId);

    Task BlacklistToken(string tokenId);
}