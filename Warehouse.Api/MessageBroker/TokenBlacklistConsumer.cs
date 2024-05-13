using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Warehouse.Persistence.Abstractions;
//using Warehouse.Api.Models.Broker;

namespace Warehouse.Api.MessageBroker;

public class TokenBlacklistConsumer : IConsumer<TokenBlacklistEvent>
{
    private readonly IDistributedCache _distributedCache;

    public TokenBlacklistConsumer(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Consume(ConsumeContext<TokenBlacklistEvent> context)
    {
        var tokenBlacklistedMessage = context.Message;

        byte[] cachedValue = Encoding.UTF8.GetBytes("blacklisted-token");
        await _distributedCache.SetAsync(tokenBlacklistedMessage.BlacklistedToken, cachedValue, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }
}
