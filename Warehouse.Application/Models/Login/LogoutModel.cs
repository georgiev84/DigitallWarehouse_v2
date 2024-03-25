namespace Warehouse.Application.Models.Login;

public class LogoutModel
{
    public string Token { get; set; }
    public string? RefreshToken { get; set; }
}