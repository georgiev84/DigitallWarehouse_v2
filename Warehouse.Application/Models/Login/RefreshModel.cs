namespace Warehouse.Application.Models.Login;
public class RefreshModel
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
