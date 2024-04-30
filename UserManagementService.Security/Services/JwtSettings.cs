﻿namespace UserManagementService.Security.Services;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string? Secret { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public int ExpiryMinutes { get; init; }
}