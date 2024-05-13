using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Warehouse.Api.Filters;

public class TokenAuthorizeFilter : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();

        string token = GetTokenFromRequestHeader(context);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
        string tokenId = jwtToken.Id;

        byte[]? cachedValue = cache.Get(tokenId);
        if (cachedValue is not null)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
    private string GetTokenFromRequestHeader(AuthorizationFilterContext context)
    {
        string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            return null;
        }

        // Check if the Authorization header starts with "Bearer "
        if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Extract the token from the Authorization header
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        // If the Authorization header doesn't start with "Bearer ", return null
        return null;
    }


    private async Task<string> GetTokenFromRequestBody(AuthorizationFilterContext context)
    {
        using (var reader = new StreamReader(context.HttpContext.Request.Body))
        {
            string requestBody = await reader.ReadToEndAsync();

            var bodyObject = JsonSerializer.Deserialize<JsonElement>(requestBody);

            if (bodyObject.TryGetProperty("token", out JsonElement tokenIdElement))
            {
                return tokenIdElement.GetString();
            }

            return null;
        }
    }
}
