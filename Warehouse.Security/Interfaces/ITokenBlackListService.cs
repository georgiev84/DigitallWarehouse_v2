namespace Warehouse.Security.Interfaces;

public interface ITokenBlacklistService
{
    Task<bool> IsTokenBlacklisted(string tokenId);

    Task BlacklistToken(string tokenId);
}