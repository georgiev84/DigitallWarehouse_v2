using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Services;
using Warehouse.Infrastructure.Authentication;
using Warehouse.Infrastructure.Identity;
using Warehouse.Infrastructure.Services;
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
        services.AddAuth(configuration);
        services.Configure<MockyClientConfiguration>(configuration.GetSection("MockyClient"));
        services.AddHttpClient<MockApiCLient>();
        services.AddScoped<IMockApiClient, MockApiCLient>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);

        services.AddSingleton(Options.Create(JwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                };

            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName, "admin"));
        });

        return services;
    }
}