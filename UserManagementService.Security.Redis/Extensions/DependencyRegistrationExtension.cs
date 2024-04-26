using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Security.Redis.Repositories;

namespace UserManagementService.Security.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddSingleton<ITokenBlackListRepository, TokenBlackListRepository>();
        services.AddStackExchangeRedisCache(options =>
        {
            string connection = configuration.GetConnectionString("Redis");
            options.Configuration = connection;
        });
        return services;
    }
}