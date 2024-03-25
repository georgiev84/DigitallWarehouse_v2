namespace Warehouse.Application.Common.Interfaces.Authentication;
public interface ITokenBlackListService
{
    Task<bool> IsTokenBlacklisted(string tokenId);
    Task BlacklistToken(string tokenId);
}
