using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Common.Interfaces.Security;

namespace Warehouse.Security.Services;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly HashSet<string> _blacklistedTokens = new HashSet<string>();
    private readonly ITokenBlackListRepository _repository;

    public TokenBlacklistService(ITokenBlackListRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsTokenBlacklisted(string tokenId)
    {
        var blacklistedToken = await _repository.IsTokenBlacklisted(tokenId);
        return blacklistedToken;
    }

    public async Task BlacklistToken(string tokenId)
    {
        await _repository.BlacklistToken(tokenId);
    }
}