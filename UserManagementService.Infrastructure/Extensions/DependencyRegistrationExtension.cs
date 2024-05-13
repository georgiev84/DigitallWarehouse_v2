using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.Application.Common.Interfaces.MessageBroker;
using UserManagementService.Infrastructure.RabbitMq;

namespace UserManagementService.Infrastructure.Extensions;
public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        return services;
    }
}