namespace Warehouse.Api.Models.Requests.Login;

public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}