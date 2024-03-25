namespace Warehouse.Application.Models.Login;
public class LogoutModel
{
    public required string Token { get; set; }
    public required string? RefreshToken { get; set; }
}
