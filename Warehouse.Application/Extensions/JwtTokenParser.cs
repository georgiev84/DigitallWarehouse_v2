using System.IdentityModel.Tokens.Jwt;

namespace Warehouse.Application.Extensions;

public static class JwtTokenParser
{
    public static Guid ParseJwtToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
        string subClaim = jwtToken.Subject;
        Guid subGuid = Guid.Parse(subClaim);

        return subGuid;
    }
}