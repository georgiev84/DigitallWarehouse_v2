namespace UserManagementService.Application.Models.Dto.Login;

public class LogoutModel
{
    public string Token { get; set; }
    public string? RefreshToken { get; set; }
}