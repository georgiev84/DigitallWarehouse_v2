namespace Warehouse.Security.Interfaces;
public interface ITokenBlackListRepository
{
    Task<bool> IsTokenBlacklisted(string tokenId);
    Task BlacklistToken(string tokenId);
}
