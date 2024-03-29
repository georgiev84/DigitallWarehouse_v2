using Warehouse.Security.Interfaces;

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
        //return Task.FromResult(_blacklistedTokens.Contains(tokenId));
    }

    public async Task BlacklistToken(string tokenId)
    {
        //_blacklistedTokens.Add(tokenId);
        await _repository.BlacklistToken(tokenId);
        //return Task.CompletedTask;
    }
}