using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserManagementService.Application.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}