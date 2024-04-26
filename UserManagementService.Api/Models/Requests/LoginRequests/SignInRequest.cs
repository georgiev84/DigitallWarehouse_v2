namespace UserManagementService.Api.Models.Requests.LoginRequests;


public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}