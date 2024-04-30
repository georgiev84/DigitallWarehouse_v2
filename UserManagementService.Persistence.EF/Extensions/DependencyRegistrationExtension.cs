using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Persistence.EF.Persistence;
using UserManagementService.Persistence.EF.Persistence.Contexts;
using UserManagementService.Persistence.EF.Persistence.Repositories;

namespace UserManagementService.Persistence.EF.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddPersistenceEF(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<UsersDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("UsersDbConnection"),
            options => options.UseCompatibilityLevel(150)));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}