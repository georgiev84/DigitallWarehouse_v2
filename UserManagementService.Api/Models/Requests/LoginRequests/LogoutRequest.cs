namespace UserManagementService.Api.Models.Requests.LoginRequests;
public class LogoutRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}