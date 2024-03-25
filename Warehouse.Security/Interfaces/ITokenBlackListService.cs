namespace Warehouse.Security.Interfaces;

public interface ITokenBlackListService
{
    Task<bool> IsTokenBlacklisted(string tokenId);

    Task BlacklistToken(string tokenId);
}