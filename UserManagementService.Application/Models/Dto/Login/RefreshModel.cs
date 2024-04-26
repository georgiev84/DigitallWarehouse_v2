namespace UserManagementService.Application.Models.Dto.Login;

public class RefreshModel
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}