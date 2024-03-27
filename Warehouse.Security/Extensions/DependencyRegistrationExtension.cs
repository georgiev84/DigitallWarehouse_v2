using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Warehouse.Domain.Entities.Users;
using Warehouse.Security.Identity;
using Warehouse.Security.Interfaces;
using Warehouse.Security.Services;

namespace Warehouse.Security.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
        services.AddSingleton(Options.Create(JwtSettings));
        services.AddSingleton(typeof(IJwtTokenGenerator<User>), typeof(JwtTokenGenerator<User>));

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

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var tokenBlacklist = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();
                        var tokenId = context.SecurityToken.Id;

                        if (await tokenBlacklist.IsTokenBlacklisted(tokenId))
                        {
                            context.Fail("Token has been blacklisted");
                        }
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName, "admin"));
            options.AddPolicy(IdentityData.CustomerPolicyName, p => p.RequireClaim(IdentityData.CustomerUserClaimName, "CustomerRole"));
        });

        return services;
    }
}