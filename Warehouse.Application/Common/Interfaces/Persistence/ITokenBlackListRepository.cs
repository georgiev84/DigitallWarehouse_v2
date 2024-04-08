namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface ITokenBlackListRepository
{
    Task<bool> IsTokenBlacklisted(string tokenId);

    Task BlacklistToken(string tokenId);
}