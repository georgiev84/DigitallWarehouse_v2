namespace Warehouse.Api.Models.Responses.LoginResponses;

public class LoginResponse
{
    public string Email { get; set; }
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
