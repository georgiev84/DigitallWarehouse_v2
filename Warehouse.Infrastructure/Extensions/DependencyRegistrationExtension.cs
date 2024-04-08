using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Persistence.EF.Client;
using Warehouse.Persistence.EF.Configuration;

namespace Warehouse.Persistence.EF.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        services.Configure<MockyClientConfiguration>(configuration.GetSection("MockyClient"));
        services.AddHttpClient<MockApiCLient>();
        services.AddScoped<IMockApiClient, MockApiCLient>();
        ;
        return services;
    }
}