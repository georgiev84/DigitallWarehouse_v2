﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Services;
using Warehouse.Domain.Entities.Users;
using Warehouse.Infrastructure.Identity;

namespace Warehouse.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        DateTime defaultExpirationTime = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

        return GenerateToken(user, defaultExpirationTime);
    }

    public string GenerateToken(User user, DateTime? expirationTime = null)
    {
        if (expirationTime == null)
        {
            expirationTime = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);
        }

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(IdentityData.AdminUserClaimName, user.Role)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expirationTime.Value,
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string RevokeToken(User user)
    {
        DateTime expirationTime = _dateTimeProvider.UtcNow.AddMinutes(-10);
        return GenerateToken(user, expirationTime);
    }
}
