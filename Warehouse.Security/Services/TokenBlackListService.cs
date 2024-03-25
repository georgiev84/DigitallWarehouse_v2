using Warehouse.Security.Interfaces;

namespace Warehouse.Security.Services;

public class TokenBlacklistService : ITokenBlackListService
{
    private readonly HashSet<string> _blacklistedTokens = new HashSet<string>();

    public Task<bool> IsTokenBlacklisted(string tokenId)
    {
        return Task.FromResult(_blacklistedTokens.Contains(tokenId));
    }

    public Task BlacklistToken(string tokenId)
    {
        _blacklistedTokens.Add(tokenId);
        return Task.CompletedTask;
    }
}