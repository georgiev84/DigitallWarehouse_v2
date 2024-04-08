namespace Warehouse.Api.Models.Requests.Login;

public class LogoutRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}