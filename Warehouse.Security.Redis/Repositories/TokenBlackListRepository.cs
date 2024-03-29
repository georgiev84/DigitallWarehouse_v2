using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Warehouse.Application.Common.Interfaces.Persistence;

namespace Warehouse.Security.Redis.Repositories;
public class TokenBlackListRepository : ITokenBlackListRepository
{
    private readonly IDistributedCache _distributedCache;

    public TokenBlackListRepository(IDistributedCache cache)
    {
        _distributedCache = cache;
    }
    public async Task<bool> IsTokenBlacklisted(string tokenId)
    {
        byte[]? cachedValue = await _distributedCache.GetAsync(tokenId);

        return cachedValue != null;
    }

    public async Task BlacklistToken(string tokenId)
    {
        byte[] cachedValue = Encoding.UTF8.GetBytes("blacklisted");
        await _distributedCache.SetAsync(tokenId, cachedValue, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) 
        });
    }
}
