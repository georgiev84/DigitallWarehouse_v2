namespace UserManagementService.Application.Models.Dto.Login;

public class LoginGoogleModel
{
    public string Token { get; set; }
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
}