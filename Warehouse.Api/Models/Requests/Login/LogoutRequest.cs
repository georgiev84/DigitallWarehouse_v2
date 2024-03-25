namespace Warehouse.Api.Models.Requests.Login;

public class LogoutRequest
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
